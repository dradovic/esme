using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostMessageSucceededReducer : Reducer<MessagesState, PostMessageSucceededAction>
    {
        public override MessagesState Reduce(MessagesState state, PostMessageSucceededAction action)
        {
            var newState = state.TransitionTo(State.Default);
            newState.Merge(action.PostedMessage);
            return newState;
        }
    }
}
