using Blazor.Fluxor;
using esme.Shared.Circles;
using System.Collections.Generic;

namespace esme.Client.Store.Messages
{
    public class FetchMessagesSucceededAction : IAction
    {
        public FetchMessagesSucceededAction(List<MessageViewModel> messages)
        {
            Messages = messages;
        }

        public List<MessageViewModel> Messages { get; }
    }
}