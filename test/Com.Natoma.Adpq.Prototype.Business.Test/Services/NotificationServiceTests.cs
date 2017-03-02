using System;
using System.Collections.Generic;
using System.Linq;
using Com.Natoma.Adpq.Prototype.Business.Data;
using Com.Natoma.Adpq.Prototype.Business.Models.Notification;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;
using Com.Natoma.Adpq.Prototype.Business.Models.UserProfile;
using Com.Natoma.Adpq.Prototype.Business.Services;
using Com.Natoma.Adpq.Prototype.Business.Services.Interfaces;
using Com.Natoma.Adpq.Prototype.Business.Test.TestUtils;
using Moq;
using Org.BouncyCastle.Asn1.Cmp;
using Twilio.Rest.Api.V2010.Account;
using Xunit;

namespace Com.Natoma.Adpq.Prototype.Business.Test.Services
{
    public class NotificationServiceTests
    {

        [Fact]
        public void NotificationService_Get_Success()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockSmsService = new Mock<ISmsService>();
            PopulateContextGet(context);
            var service = new NotificationService(context, mockGeoCodeService.Object, mockEmailService.Object,
                mockSmsService.Object);

            var result = service.Get();
            Assert.True(result.Data != null);
            var noteList = (List<UserNotificationViewModel>)result.Data;
            Assert.True(noteList.Count == 1);
            Assert.True(noteList.First().NotificationMessage == "Test Message");
            Assert.True(noteList.First().NotificationSmsMessage == "Test SMS Message");
        }

        [Fact]
        public void NotificationService_GetById_Success()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockSmsService = new Mock<ISmsService>();
            PopulateContextGet(context);
            var service = new NotificationService(context, mockGeoCodeService.Object, mockEmailService.Object,
                mockSmsService.Object);

            var result = service.Get(1);
            Assert.True(result.Data != null);
            var noteList = (List<UserNotificationViewModel>)result.Data;
            Assert.True(noteList.Count == 1);
            Assert.True(noteList.First().NotificationMessage == "Test Message");
            Assert.True(noteList.First().NotificationSmsMessage == "Test SMS Message");
        }

        [Fact]
        public async void NotificationService_Send_Success()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            mockGeoCodeService.Setup(x => x.GetGeoLocation(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => new LatLongSet() {Latitude = 38.568713, Longitude = -121.414380});
            mockGeoCodeService.Setup(x => x.GetUsersInRadius(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>()))
                .Returns(() => new List<User>
                {
                    new User
                    {
                        UserId = 2,
                        IsSms = true,
                        IsEmailNotification = false,
                        Latitude = 38.615967,
                        Longitude = -121.360219,
                    },
                    new User
                    {
                        UserId = 3,
                        IsSms = false,
                        IsEmailNotification = true,
                        Latitude = 38.576486,
                        Longitude = -121.493858,
                    }
                });
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => true);
            var mockSmsService = new Mock<ISmsService>();
            mockSmsService.Setup(x => x.SendSms(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => true);
            PopulateContextSend(context);
            var service = new NotificationService(context, mockGeoCodeService.Object, mockEmailService.Object,
                mockSmsService.Object);

            var notificationViewModel = new NotificationViewModel
            {
                EmailSubject = "test",
                EmailMessage = "test",
                SmsMessage = "test",
                NotificationType = NotificationTypeEnum.Regional,
                Latitude = 38.568713,
                Longitude = -121.414380,
                RadiusMiles = 50,
                AddressLine1 = "test",
                AddressLine2 = "test",
                City = "test",
                State = "test",
                Zipcode = "test",
                CreatedBy = 1
            };

            var result = await service.CreateAndSendNotification(notificationViewModel);
            Assert.True(result.State == RequestStateEnum.Success);

        }

        [Fact]
        public void NotificationService_GetByDay_Success()
        {
            var options = DbContextUtils.CreateNewContextOptions();
            var context = new adpq2adpqContext(options);
            var mockGeoCodeService = new Mock<IGeoCodeService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockSmsService = new Mock<ISmsService>();
            PopulateContextGet(context);
            var service = new NotificationService(context, mockGeoCodeService.Object, mockEmailService.Object,
                mockSmsService.Object);

            var result = service.GetNotificationsByDay();
            Assert.True(result.Data != null);
        }

        private void PopulateContextSend(adpq2adpqContext context)
        {
            var user1 = new User
            {
                UserId = 1,
                IsAdmin = false,
                IsEmailNotification = true,
                IsSms = true
            };

            // send test users
            var user2 = new User
            {
                UserId = 2,
                IsSms = true,
                IsEmailNotification = false,
                Latitude = 38.615967,
                Longitude = -121.360219,
            };
            var user3 = new User
            {
                UserId = 3,
                IsSms = false,
                IsEmailNotification = true,
                Latitude = 38.576486,
                Longitude = -121.493858,
            };

            context.User.Add(user1);
            context.User.Add(user2);
            context.User.Add(user3);

            // types
            var blastType = new NotificationType
            {
                NotificationTypeId = 1,
                Name = "Blast"
            };
            var regionalType = new NotificationType
            {
                NotificationTypeId = 2,
                Name = "Regional"
            };

            context.NotificationType.Add(blastType);
            context.NotificationType.Add(regionalType);
            context.SaveChanges();
        }

        private void PopulateContextGet(adpq2adpqContext context)
        {
            // notification test user
            var user1 = new User
            {
                UserId = 1,
                IsAdmin = false,
                IsEmailNotification = true,
                IsSms = true
            };
            var notification1 = new Notification
            {
                NotificationId = 1,
                SmsMessage = "Test SMS Message",
                EmailSubject = "Test Email Subject",
                EmailMessage = "Test Message",
                CreatedOn = DateTime.Now.AddDays(-1)
            };
            var userNotification1 = new UserNotification
            {
                UserId = 1,
                UserNotificationId = 1,
                IsEmailSent = true,
                IsSmsSent = true,
                NotificationId = 1,
                NotificationDate = DateTime.Now,
                Result = "test"
            };
            
            context.User.Add(user1);
            context.Notification.Add(notification1);
            context.UserNotification.Add(userNotification1);
            
            context.SaveChanges();
        }
    }
}
