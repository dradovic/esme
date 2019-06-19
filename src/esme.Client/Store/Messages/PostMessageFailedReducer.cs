using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostMessageFailedReducer : Reducer<MessagesState, PostMessageFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, PostMessageFailedAction action)
        {
            return new MessagesState(
                isLoading: false,
                errorMessage: action.ErrorMessage,
                messages: null);
        }
    }
}
