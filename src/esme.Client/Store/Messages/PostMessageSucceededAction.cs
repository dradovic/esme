using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class PostMessageSucceededAction
    {
        public PostMessageSucceededAction(MessageViewModel postedMessage)
        {
            PostedMessage = postedMessage;
        }

        public MessageViewModel PostedMessage { get; }
    }
}