using esme.Shared.Circles;
using System;
using System.Collections.Generic;

namespace esme.Client.Store.Messages
{
    public class MessagesState
    {
        public bool IsLoading { get; }
        public string ErrorMessage { get; }
        public List<MessageViewModel> Messages { get; private set; }

        public MessagesState(bool isLoading, string errorMessage, List<MessageViewModel> messages)
        {
            IsLoading = isLoading;
            ErrorMessage = errorMessage;
            Messages = messages;
        }

        public void Merge(IEnumerable<MessageViewModel> messages)
        {
            foreach (var message in messages)
            {
                Merge(message);
            }
        }

        public void Merge(MessageViewModel message)
        {
            if (Messages == null) Messages = new List<MessageViewModel>();
            int index = Messages.FindIndex(m => m.Id == message.Id);
            if (index >= 0)
            {
                Messages[index] = message;
            }
            else
            {
                Messages.Add(message);
            }
        }
    }
}
