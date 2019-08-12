using System;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesAction
    {
        public FetchUnreadMessagesAction(Guid circleId)
        {
            CircleId = circleId;
        }

        public Guid CircleId { get; }
    }
}
