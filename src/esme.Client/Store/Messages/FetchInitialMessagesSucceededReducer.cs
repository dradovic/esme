using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesSucceededReducer : Reducer<MessagesState, FetchInitialMessagesSucceededAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchInitialMessagesSucceededAction action)
        {
            return new MessagesState
            {
                Messages = action.Messages
            };
        }
    }
}
