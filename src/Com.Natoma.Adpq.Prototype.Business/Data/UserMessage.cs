using System;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class UserMessage
    {
        public int UserMessageId { get; set; }
        public int? MessageId { get; set; }
        public int? UserProfileid { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Message Message { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
