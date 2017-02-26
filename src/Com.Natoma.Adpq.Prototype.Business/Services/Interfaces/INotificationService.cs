using System.Threading.Tasks;
using Com.Natoma.Adpq.Prototype.Business.Models.Notification;
using Com.Natoma.Adpq.Prototype.Business.Models.Request;

namespace Com.Natoma.Adpq.Prototype.Business.Services.Interfaces
{
    public interface INotificationService
    {
        RequestResult Get();
        RequestResult Get(int userProfileId);
        Task<RequestResult> CreateAndSendNotification(NotificationViewModel notificationViewModel);
    }
}
