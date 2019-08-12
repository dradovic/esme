using Blazor.Fluxor;
using esme.Client.Services;
using esme.Shared.Circles;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesEffect : Effect<FetchInitialMessagesAction>
    {
        private readonly MessagesApi _api;

        public FetchInitialMessagesEffect(MessagesApi api)
        {
            _api = api;
        }

        protected async override Task HandleAsync(FetchInitialMessagesAction action, IDispatcher dispatcher)
        {
            try
            {
                var messages = await _api.ReadMessages(action.CircleId, ReadMessagesOptions.All);
                dispatcher.Dispatch(new FetchInitialMessagesSucceededAction(messages.ToList()));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new FetchInitialMessagesFailedAction(errorMessage: x.Message));
            }
        }
    }
}
