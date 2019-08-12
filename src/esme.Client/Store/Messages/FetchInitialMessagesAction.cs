using System;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesAction
    {
        public FetchInitialMessagesAction(Guid circleId)
        {
            CircleId = circleId;
        }

        public Guid CircleId { get; }
    }
}
