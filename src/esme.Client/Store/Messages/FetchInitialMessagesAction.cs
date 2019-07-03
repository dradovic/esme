using Blazor.Fluxor;
using System;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesAction : IAction
    {
        public FetchInitialMessagesAction(Guid circleId)
        {
            CircleId = circleId;
        }

        public Guid CircleId { get; }
    }
}
