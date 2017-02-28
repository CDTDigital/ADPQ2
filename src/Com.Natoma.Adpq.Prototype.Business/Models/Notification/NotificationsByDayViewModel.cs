using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Notification
{
    public class NotificationsByDayViewModel
    {
        public DateTime DateSent { get; set; }
        public NotificationSendTypeEnum SendType { get; set; }
        public string SendTypeDisplay { get; set; }
        public int Count { get; set; }
    }
}
