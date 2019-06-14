using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Messages
{
    public class SubmitMessageSucceededReducer : Reducer<MessagesState, SubmitMessageSucceededAction>
    {
        public override MessagesState Reduce(MessagesState state, SubmitMessageSucceededAction action)
        {
            return state.DeepClone(); // FIXME: da, set status of submitted messages to 'sent'
        }
    }
}
