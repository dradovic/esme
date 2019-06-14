using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchMessagesFailedReducer : Reducer<MessagesState, FetchMessagesFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchMessagesFailedAction action)
        {
            return new MessagesState(
                isLoading: false,
                errorMessage: action.ErrorMessage,
                messages: null);
        }
    }
}
