using System.Collections.Generic;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesSucceededAction
    {
        public FetchUnreadMessagesSucceededAction(List<MessageViewModel> messages)
        {
            Messages = messages;
        }

        public List<MessageViewModel> Messages { get; }
    }
}