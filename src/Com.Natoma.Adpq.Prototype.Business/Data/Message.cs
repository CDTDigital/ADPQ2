using System;
using System.Collections.Generic;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class Message
    {
        public Message()
        {
            UserMessage = new HashSet<UserMessage>();
        }

        public int MessageId { get; set; }
        public DateTime? DateSent { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public int? EventId { get; set; }

        public virtual ICollection<UserMessage> UserMessage { get; set; }
        public virtual Event Event { get; set; }
    }
}
