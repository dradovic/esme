using Blazor.Fluxor;

namespace esme.Client.Store.Circles
{
    public class IncrementUnreadMessagesAction : IAction
    {
        public IncrementUnreadMessagesAction(int circleId)
        {
            CircleId = circleId;
        }

        public int CircleId { get; }
    }
}
