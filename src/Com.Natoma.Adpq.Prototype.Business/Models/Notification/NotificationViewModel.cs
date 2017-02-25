using System;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Notification
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public DateTime? DateSent { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
