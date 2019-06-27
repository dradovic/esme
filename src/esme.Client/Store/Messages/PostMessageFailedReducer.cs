using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostMessageFailedReducer : Reducer<MessagesState, PostMessageFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, PostMessageFailedAction action)
        {
            var newState = state.TransitionTo(State.Default);
            newState.ErrorMessage = action.ErrorMessage;
            return newState;
        }
    }
}
