using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Natoma.Adpq.Prototype.Business.Models.Message
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }
        public DateTime? DateSent { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
