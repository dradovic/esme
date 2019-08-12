using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesFailedReducer : Reducer<MessagesState, FetchUnreadMessagesFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchUnreadMessagesFailedAction action)
        {
            // FEATURE: da, do something with the error message?
            return state.DeepClone();
        }
    }
}
