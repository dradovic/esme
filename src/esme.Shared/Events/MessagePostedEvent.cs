using System;

namespace esme.Shared.Events
{
    public class MessagePostedEvent
    {
        public Guid CircleId { get; set; }
        public Guid MessageId { get; set; }
    }
}
