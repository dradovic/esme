using Blazor.Fluxor;
using esme.Shared.Circles;
using Force.DeepCloner;
using System.Collections.Generic;

namespace esme.Client.Store.Messages
{
    public class SubmitMessageReducer : Reducer<MessagesState, SubmitMessageAction>
    {
        public override MessagesState Reduce(MessagesState state, SubmitMessageAction action)
        {
            var newState = state.DeepClone();
            if (newState.Messages != null)
            {
                newState.Messages.Add(new MessageViewModel
                {
                    Text = action.NewMessage.Text,
                });
            }
            return newState;
        }
    }
}
