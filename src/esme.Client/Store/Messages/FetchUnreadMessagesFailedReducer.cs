using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesFailedReducer : Reducer<MessagesState, FetchUnreadMessagesFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchUnreadMessagesFailedAction action)
        {
            // FIXME: da, set status of all unsent messages to "failed sending"
            return state.DeepClone();
        }
    }
}
