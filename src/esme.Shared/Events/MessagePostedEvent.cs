using System;

namespace esme.Shared.Events
{
    public class MessagePostedEvent
    {
        public int CircleId { get; set; }
        public Guid MessageId { get; set; }
    }
}
