using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchMessagesAction : IAction
    {
        public FetchMessagesAction(int circleId)
        {
            CircleId = circleId;
        }

        public int CircleId { get; }
    }
}
