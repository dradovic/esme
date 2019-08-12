using esme.Shared.Circles;

namespace esme.Client.Store.Messages
{
    public abstract class PostMessageReducer<TAction>
    {
        public MessagesState Reduce(MessagesState state, object action)
        {
            var newState = state.TransitionTo(State.Default);
            if (newState.Messages != null)
            {
                MessageViewModel newViewModel = CreateViewModel((TAction)action);
                newState.Messages.Add(newViewModel); // append to messages
            }
            return newState;
        }

        public bool ShouldReduceStateForAction(object action)
        {
            return action.GetType() == typeof(TAction);
        }

        protected abstract MessageViewModel CreateViewModel(TAction action);
    }
}
