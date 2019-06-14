using Blazor.Fluxor;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class FetchMessagesEffect : Effect<FetchMessagesAction>
    {
        private readonly HttpClient _httpClient;

        public FetchMessagesEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async override Task HandleAsync(FetchMessagesAction action, IDispatcher dispatcher)
        {
            try
            {
                var messages = await _httpClient.PostJsonAsync<IEnumerable<MessageViewModel>>($"api/my/messages/actions/read?circleId={action.CircleId}", null);
                dispatcher.Dispatch(new FetchMessagesSucceededAction(messages.ToList()));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new FetchMessagesFailedAction(errorMessage: x.Message));
            }
        }
    }
}
