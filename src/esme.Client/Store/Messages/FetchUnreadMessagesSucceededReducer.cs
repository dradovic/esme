using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesSucceededReducer : Reducer<MessagesState, FetchUnreadMessagesSucceededAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchUnreadMessagesSucceededAction action)
        {
            var newState = state.DeepClone();
            foreach (var message in action.Messages)
            {
                int index = newState.Messages.FindIndex(m => m.Id == message.Id);
                if (index >= 0)
                {
                    newState.Messages[index] = message;
                }
                else
                {
                    newState.Messages.Add(message);
                }
            }
            return newState;
        }
    }
}