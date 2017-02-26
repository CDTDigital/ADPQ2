using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Notification
{
    public class UserNotificationViewModel
    {
        public int UserNotificationId { get; set; }
        public int NotificationId { get; set; }
        public string NotificationSubject { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsEmail { get; set; }
        public bool IsSms { get; set; }
        public string Result { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
