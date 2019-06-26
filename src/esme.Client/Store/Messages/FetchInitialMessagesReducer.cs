using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesReducer : Reducer<MessagesState, FetchInitialMessagesAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchInitialMessagesAction action)
        {
            return new MessagesState
            {
                State = State.IsLoading
            };
        }
    }
}
