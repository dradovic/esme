using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchMessagesReducer : Reducer<MessagesState, FetchMessagesAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchMessagesAction action)
        {
            return new MessagesState(
                isLoading: true,
                errorMessage: null,
                messages: null);
        }
    }
}
