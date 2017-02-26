using System;
using System.Collections.Generic;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class Notification
    {
        public Notification()
        {
            UserNotification = new HashSet<UserNotification>();
        }

        public int NotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public int? RadiusMiles { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public string SmsMessage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<UserNotification> UserNotification { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}
