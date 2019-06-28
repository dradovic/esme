using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class PostTextMessageSucceededAction : IAction
    {
        public PostTextMessageSucceededAction(MessageViewModel postedMessage)
        {
            PostedMessage = postedMessage;
        }

        public MessageViewModel PostedMessage { get; }
    }
}