using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class SubmitMessageAction : IAction
    {
        public SubmitMessageAction(int circleId, MessageEditModel newMessage)
        {
            CircleId = circleId;
            NewMessage = newMessage;
        }

        public int CircleId { get; }
        public MessageEditModel NewMessage { get; }
    }
}
