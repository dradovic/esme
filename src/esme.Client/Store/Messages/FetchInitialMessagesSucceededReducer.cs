using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesSucceededReducer : Reducer<MessagesState, FetchInitialMessagesSucceededAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchInitialMessagesSucceededAction action)
        {
            return new MessagesState(
                isLoading: false,
                errorMessage: null,
                messages: action.Messages);
        }
    }
}
