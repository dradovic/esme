using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesReducer : Reducer<MessagesState, FetchUnreadMessagesAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchUnreadMessagesAction action)
        {
            // FEATURE: da, show spinner?
            return state.DeepClone();
        }
    }
}
