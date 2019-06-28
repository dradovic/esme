using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class PostMessageSucceededAction : IAction
    {
        public PostMessageSucceededAction(MessageViewModel postedMessage)
        {
            PostedMessage = postedMessage;
        }

        public MessageViewModel PostedMessage { get; }
    }
}