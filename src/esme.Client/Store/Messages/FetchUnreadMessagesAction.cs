using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesAction : IAction
    {
        public FetchUnreadMessagesAction(int circleId)
        {
            CircleId = circleId;
        }

        public int CircleId { get; }
    }
}
