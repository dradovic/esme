using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class PostTextMessageAction : IAction
    {
        public PostTextMessageAction(int circleId, TextMessageEditModel newMessage)
        {
            CircleId = circleId;
            NewMessage = newMessage;
        }

        public int CircleId { get; }
        public TextMessageEditModel NewMessage { get; }
    }
}
