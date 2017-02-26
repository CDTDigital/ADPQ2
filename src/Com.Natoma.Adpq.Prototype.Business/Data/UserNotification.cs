using System;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class UserNotification
    {
        public int UserNotificationId { get; set; }
        public int UserId { get; set; }
        public int NotificationId { get; set; }
        public bool IsEmailSent { get; set; }
        public bool IsSmsSent { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Result { get; set; }

        public virtual Notification Notification { get; set; }
        public virtual User User { get; set; }
    }
}
