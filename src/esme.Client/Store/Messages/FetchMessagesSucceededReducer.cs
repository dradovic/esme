using Blazor.Fluxor;
using System;

namespace esme.Client.Store.Messages
{
    public class FetchMessagesSucceededReducer : Reducer<MessagesState, FetchMessagesSucceededAction>
    {
        public override MessagesState Reduce(MessagesState state, FetchMessagesSucceededAction action)
        {
            return new MessagesState(
                isLoading: false,
                errorMessage: null,
                messages: action.Messages);
        }
    }
}
