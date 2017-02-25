using System;
using System.Collections.Generic;

namespace Com.Natoma.Adpq.Prototype.Web.Scaffold
{
    public partial class UserNotification
    {
        public int UserNotificationId { get; set; }
        public int UserId { get; set; }
        public int NotificationId { get; set; }
        public bool? IsEmailSent { get; set; }
        public bool? IsSmsSent { get; set; }
        public DateTime? NotificationDate { get; set; }
    }
}
