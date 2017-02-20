using System;
using System.Collections.Generic;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class Event
    {
        public Event()
        {
            Message = new HashSet<Message>();
        }

        public int EventId { get; set; }
        public int? EventSourceId { get; set; }
        public int? EventTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceUrl { get; set; }
        public DateTime? OccurrenceDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual ICollection<Message> Message { get; set; }
        public virtual EventType EventType { get; set; }
    }
}
