using System.Collections.Generic;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            Notification = new HashSet<Notification>();
        }

        public int NotificationTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Notification> Notification { get; set; }
    }
}
