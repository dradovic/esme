using Blazor.Fluxor;
using esme.Shared.Circles;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class PostMessageReducer : Reducer<MessagesState, PostMessageAction>
    {
        public override MessagesState Reduce(MessagesState state, PostMessageAction action)
        {
            var newState = state.DeepClone();
            if (newState.Messages != null)
            {
                MessageViewModel newViewModel = new MessageViewModel
                {
                    Id = action.NewMessage.Id,
                    Text = action.NewMessage.Text,
                };
                newState.Messages.Add(newViewModel);
            }
            return newState;
        }
    }
}
