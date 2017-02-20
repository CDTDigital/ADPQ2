using System.Collections.Generic;

namespace Com.Natoma.Adpq.Prototype.Business.Data
{
    public partial class EventType
    {
        public EventType()
        {
            Event = new HashSet<Event>();
        }

        public int EventTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
