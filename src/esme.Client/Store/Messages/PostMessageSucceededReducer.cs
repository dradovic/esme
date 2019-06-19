using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class PostMessageSucceededReducer : Reducer<MessagesState, PostMessageSucceededAction>
    {
        public override MessagesState Reduce(MessagesState state, PostMessageSucceededAction action)
        {
            var newState = state.DeepClone(); // FIXME: da, set status of submitted messages to 'sent'
            newState.Merge(action.PostedMessage);
            return newState;
        }
    }
}
