using Blazor.Fluxor;
using esme.Client.Services;
using esme.Shared.Circles;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesEffect : Effect<FetchUnreadMessagesAction>
    {
        private readonly MessagesApi _api;

        public FetchUnreadMessagesEffect(MessagesApi api)
        {
            _api = api;
        }

        protected override async Task HandleAsync(FetchUnreadMessagesAction action, IDispatcher dispatcher)
        {
            try
            {
                var messages = await _api.ReadMessages(action.CircleId, ReadMessagesOptions.Unread);
                dispatcher.Dispatch(new FetchUnreadMessagesSucceededAction(messages.ToList()));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new FetchUnreadMessagesFailedAction(errorMessage: x.Message));
            }
        }
    }
}
