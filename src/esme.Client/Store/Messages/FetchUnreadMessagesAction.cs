using Blazor.Fluxor;
using System;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesAction : IAction
    {
        public FetchUnreadMessagesAction(Guid circleId)
        {
            CircleId = circleId;
        }

        public Guid CircleId { get; }
    }
}
