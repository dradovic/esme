using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesReducer : Reducer<MessagesState, FetchUnreadMessagesAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchUnreadMessagesAction action)
        {
            // FIXME: da, set status of all unread messages to "loading"
            return state.DeepClone();
        }
    }
}
