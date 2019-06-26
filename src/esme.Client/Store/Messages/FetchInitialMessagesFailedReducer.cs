using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesFailedReducer : Reducer<MessagesState, FetchInitialMessagesFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchInitialMessagesFailedAction action)
        {
            return new MessagesState
            {
                ErrorMessage = action.ErrorMessage,
            };
        }
    }
}
