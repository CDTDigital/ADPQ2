using System;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Notification
{
    public class NotificationsByDayViewModel
    {
        public DateTime DateSent { get; set; }
        public string DateSentDisplay { get; set; } 
        public NotificationSendTypeEnum SendType { get; set; }
        public string SendTypeDisplay { get; set; }
        public int Count { get; set; }
    }
}
