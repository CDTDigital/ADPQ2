using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.Notification;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Com.Natoma.Adpq.Prototype.Business.Services
{
    public class NotificationService: INotificationService
    {
        private readonly adpq2adpqContext _context;
        private readonly IGeoCodeService _geoCodeService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public NotificationService(adpq2adpqContext context, IGeoCodeService geoCodeService, IEmailService emailService, ISmsService smsService)
        {
            _context = context;
            _geoCodeService = geoCodeService;
            _emailService = emailService;
            _smsService = smsService;
        }

        public RequestResult Get()
        {
            var result = new RequestResult();
            var userNotifications = _context.UserNotification.Include(x => x.Notification)
                .Select(PopulateUserNotificationViewModel).ToList();
            result.State = RequestStateEnum.Success;
            result.Data = userNotifications;
            return result;
        }

        public RequestResult Get(int userProfileId)
        {
            var result = new RequestResult();
            var userNotifications = _context.UserNotification.Include(x => x.Notification)
                .Where(x => x.UserId == userProfileId).Select(PopulateUserNotificationViewModel).ToList();
            result.State = RequestStateEnum.Success;
            result.Data = userNotifications;
            return result;
        }

        public RequestResult GetNotificationsByDay()
        {
            var result = new List<NotificationsByDayViewModel>();
            var notificationGroups =
                _context.Notification.Include(x => x.UserNotification)
                    .Where(x => x.CreatedOn >= DateTime.Now.AddDays(-30) && x.CreatedOn <= DateTime.Now)
                    .GroupBy(g => g.CreatedOn)
                    .Select(gi => new {Items = gi })
                    .ToList();
            
            foreach (var group in notificationGroups)
            {
                
                var emailNotes = group.Items.Where(x => x.EmailSubject != null).ToList();
                int totalEmails = emailNotes.Sum(item => item.UserNotification.Where(x => x.IsEmailSent).ToList().Count);
                if (emailNotes.Count > 0)
                {
                    var emailItem = new NotificationsByDayViewModel
                    {
                        DateSent = (DateTime)group.Items.First().CreatedOn,
                        Count = totalEmails,
                        SendType = NotificationSendTypeEnum.Email,
                        SendTypeDisplay = NotificationSendTypeEnum.Email.ToString()
                    };
                    emailItem.DateSentDisplay = emailItem.DateSent.ToString("d");
                    result.Add(emailItem);
                }
                var smsNotes = group.Items.Where(x => x.SmsMessage != null).ToList();
                int totalTexts = smsNotes.Sum(item => item.UserNotification.Where(x => x.IsSmsSent).ToList().Count);
                if (smsNotes.Count > 0)
                {
                    var smsItem = new NotificationsByDayViewModel
                    {
                        DateSent = (DateTime)group.Items.First().CreatedOn,
                        DateSentDisplay = string.Format("d", (DateTime)group.Items.First().CreatedOn),
                        Count = totalTexts,
                        SendType = NotificationSendTypeEnum.Text,
                        SendTypeDisplay = NotificationSendTypeEnum.Text.ToString()
                    };
                    smsItem.DateSentDisplay = smsItem.DateSent.ToString("d");
                    result.Add(smsItem);
                } 
            }
            return new RequestResult
            {
                 State = RequestStateEnum.Success,
                 Data = result.OrderBy(x=> x.DateSent)
            };
        }
        
        public async Task<RequestResult> CreateAndSendNotification(NotificationViewModel notificationViewModel)
        {
            var result = new RequestResult();
            
            // prepare the new objects for context updates
            var newNotification = new Notification
            {
                State = notificationViewModel.State,
                Zipcode = notificationViewModel.Zipcode,
                Address1 = notificationViewModel.AddressLine1,
                Address2 = notificationViewModel.AddressLine2,
                City = notificationViewModel.City,
                EmailSubject = notificationViewModel.EmailSubject,
                EmailMessage = notificationViewModel.EmailMessage,
                SmsMessage = notificationViewModel.SmsMessage,
                NotificationType = _context.NotificationType.First(x => x.Name == notificationViewModel.NotificationType.ToString()),
                CreatedOn = DateTime.Now,
                CreatedBy = notificationViewModel.CreatedBy,
                UpdatedBy = notificationViewModel.CreatedBy,
                UpdatedOn = DateTime.Now,
                RadiusMiles = notificationViewModel.RadiusMiles
            };

            List<User> usersToRecieve;
            if (notificationViewModel.NotificationType == NotificationTypeEnum.Regional)
            {
                //  geocode the notification
                // TODO: Doing this here assumes that lat/long will be found.  A refactor would push this check to the client.
                var latLongSet = _geoCodeService.GetGeoLocation(notificationViewModel.AddressLine1, notificationViewModel.AddressLine2,
                    notificationViewModel.City, notificationViewModel.State, notificationViewModel.Zipcode);

                // get a list of users to send notifications to
                usersToRecieve = GetUserToNotify(latLongSet.Latitude, latLongSet.Longitude,
                    notificationViewModel.RadiusMiles);
            }
            else
            {
                // blast, send to everyone
                usersToRecieve = _context.User.Where(x => (x.IsEmailNotification|| x.IsSms) && !x.IsAdmin).ToList();
            }

            await ProcessNotifications(newNotification, usersToRecieve);
            _emailService.Disconnect();

            _context.Notification.Add(newNotification);
            await _context.SaveChangesAsync();

            result.State = RequestStateEnum.Success;

            return result;
        }

        private async Task ProcessNotifications(Notification notification, List<User> usersToRecieve)
        {
            // NOTE: In a real production app, this functionality should be accomplished using a third party email tool to avoid domain spam issues.

            var tasks = new List<Task>();
            // semaphore, allow to run 10 tasks in parallel
            var semaphore = new SemaphoreSlim(10);
            foreach (var user in usersToRecieve)
            {
                // await here until there is a room for this task
                await semaphore.WaitAsync();
                tasks.Add(SendNotification(semaphore, notification, user));
            }
            // await for the rest of tasks to complete
            await Task.WhenAll(tasks);
        }
        
        private async Task SendNotification(SemaphoreSlim semaphore, Notification notification, User user)
        {
            var resultMessage = new StringBuilder();
            var newUserNotification = new UserNotification
            {
                UserId = user.UserId,
                NotificationId = notification.NotificationId,
                IsEmailSent = user.IsEmailNotification,
                IsSmsSent = user.IsSms
            };

            if (user.IsEmailNotification && !string.IsNullOrEmpty(notification.EmailSubject))
            {
                // send an email
                var emailResult = await _emailService.SendEmailAsync(user.Email, notification.EmailSubject, notification.EmailMessage);
                resultMessage.AppendLine(!emailResult ? "Email Failed. " : "Email Success. ");
            }
            if (user.IsSms && !string.IsNullOrEmpty(notification.SmsMessage))
            {
                // send sms message
                var smsResult = await _smsService.SendSms(user.Phone, notification.SmsMessage);
                if (!smsResult)
                {
                    resultMessage.AppendLine("Sms Failed. ");
                }
                else
                {
                    resultMessage.AppendLine("Sms Success. ");
                }
            }
            newUserNotification.Result = resultMessage.ToString();
            notification.UserNotification.Add(newUserNotification);
            semaphore.Release(); // release the semaphore to free up queue
        }

        private List<User> GetUserToNotify(double latitude, double longtitude, int radiusMiles)
        {
            return _geoCodeService.GetUsersInRadius(latitude, longtitude, radiusMiles);
        }

        private UserNotificationViewModel PopulateUserNotificationViewModel(UserNotification notification)
        {
            return new UserNotificationViewModel
            {
                NotificationId = notification.NotificationId,
                UserNotificationId = notification.UserNotificationId,
                NotificationSubject = notification.Notification.EmailSubject,
                NotificationMessage = notification.Notification.EmailMessage,
                NotificationSmsMessage = notification.Notification.SmsMessage,
                IsEmail = notification.IsEmailSent,
                IsSms = notification.IsSmsSent,
                Result = notification.Result,
                CreatedOn = notification.NotificationDate,
                NotificationTypeId = notification.Notification.NotificationTypeId
            };
        }
    }
}

