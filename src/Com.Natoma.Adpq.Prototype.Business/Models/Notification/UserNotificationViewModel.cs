﻿using System;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Notification
{
    public class UserNotificationViewModel
    {
        public int UserNotificationId { get; set; }
        public int NotificationId { get; set; }
        public string NotificationSubject { get; set; }
        public string NotificationMessage { get; set; }
        public string NotificationSmsMessage { get; set; }
        public int NotificationTypeId { get; set; }
        public bool IsEmail { get; set; }
        public bool IsSms { get; set; }
        public string Result { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
