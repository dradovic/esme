using System.Collections.Generic;
using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesSucceededAction : IAction
    {
        public FetchUnreadMessagesSucceededAction(List<MessageViewModel> messages)
        {
            Messages = messages;
        }

        public List<MessageViewModel> Messages { get; }
    }
}