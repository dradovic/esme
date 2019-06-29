using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class PostTextMessageAction : IAction
    {
        public PostTextMessageAction(int circleId, TextMessageEditModel message)
        {
            CircleId = circleId;
            Message = message;
        }

        public int CircleId { get; }
        public TextMessageEditModel Message { get; }
    }
}
