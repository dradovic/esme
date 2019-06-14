using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class SubmitMessageFailedReducer : Reducer<MessagesState, SubmitMessageFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, SubmitMessageFailedAction action)
        {
            return new MessagesState(
                isLoading: false,
                errorMessage: action.ErrorMessage,
                messages: null);
        }
    }
}
