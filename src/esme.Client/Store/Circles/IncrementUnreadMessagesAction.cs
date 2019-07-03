using Blazor.Fluxor;
using System;

namespace esme.Client.Store.Circles
{
    public class IncrementUnreadMessagesAction : IAction
    {
        public IncrementUnreadMessagesAction(Guid circleId)
        {
            CircleId = circleId;
        }

        public Guid CircleId { get; }
    }
}
