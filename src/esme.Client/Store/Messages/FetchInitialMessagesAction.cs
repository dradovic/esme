using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesAction : IAction
    {
        public FetchInitialMessagesAction(int circleId)
        {
            CircleId = circleId;
        }

        public int CircleId { get; }
    }
}
