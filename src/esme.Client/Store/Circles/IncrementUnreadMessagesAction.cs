using System;

namespace esme.Client.Store.Circles
{
    public class IncrementUnreadMessagesAction
    {
        public IncrementUnreadMessagesAction(Guid circleId)
        {
            CircleId = circleId;
        }

        public Guid CircleId { get; }
    }
}
