using esme.Shared.Circles;
using System.Collections.Generic;

namespace esme.Client.Store.Messages
{
    public class MessagesState
    {
        public bool IsLoading { get; }
        public string ErrorMessage { get; }
        public List<MessageViewModel> Messages { get; }

        public MessagesState(bool isLoading, string errorMessage, List<MessageViewModel> messages)
        {
            IsLoading = isLoading;
            ErrorMessage = errorMessage;
            Messages = messages;
        }
    }
}
