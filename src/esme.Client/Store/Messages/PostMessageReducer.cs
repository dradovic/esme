using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public abstract class PostMessageReducer<TAction>
    {
        public MessagesState Reduce(MessagesState state, IAction action)
        {
            var newState = state.TransitionTo(State.Default);
            if (newState.Messages != null)
            {
                MessageViewModel newViewModel = CreateViewModel((TAction)action);
                newState.Messages.Add(newViewModel); // append to messages
            }
            return newState;
        }

        public bool ShouldReduceStateForAction(IAction action)
        {
            return action.GetType() == typeof(TAction);
        }

        protected abstract MessageViewModel CreateViewModel(TAction action);
    }
}
