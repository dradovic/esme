using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesSucceededReducer : Reducer<MessagesState, FetchUnreadMessagesSucceededAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchUnreadMessagesSucceededAction action)
        {
            var newState = state.DeepClone();
            newState.Merge(action.Messages);
            return newState;
        }
    }
}