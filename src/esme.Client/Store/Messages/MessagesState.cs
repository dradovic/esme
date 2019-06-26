using esme.Shared.Circles;
using Force.DeepCloner;
using System;
using System.Collections.Generic;

namespace esme.Client.Store.Messages
{
    public enum State
    {
        Default,
        IsLoading,
        IsRecording,
        StopRecording,
        RecordingAvailable,
    }

    public class MessagesState
    {
        public State State { get; set; }
        public string ErrorMessage { get; set; }
        public List<MessageViewModel> Messages { get; set; }
        public string RecordingUrl { get; set; }

        public MessagesState TransitionTo(State newState)
        {
            var result = this.DeepClone();
            result.State = newState;
            return result;
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
