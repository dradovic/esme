using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostTextMessageFailedReducer : Reducer<MessagesState, PostTextMessageFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, PostTextMessageFailedAction action)
        {
            var newState = state.TransitionTo(State.Default);
            newState.ErrorMessage = action.ErrorMessage;
            return newState;
        }
    }
}
