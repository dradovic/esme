using Blazor.Fluxor;
using esme.Shared.Circles;
using System.Collections.Generic;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesSucceededAction : IAction
    {
        public FetchInitialMessagesSucceededAction(List<MessageViewModel> messages)
        {
            Messages = messages;
        }

        public List<MessageViewModel> Messages { get; }
    }
}