using Blazor.Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class SubmitMessageEffect : Effect<SubmitMessageAction>
    {
        private readonly HttpClient _httpClient;

        public SubmitMessageEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task HandleAsync(SubmitMessageAction action, IDispatcher dispatcher)
        {
            try
            {
                await _httpClient.PostJsonAsync($"api/my/messages?circleId={action.CircleId}", action.NewMessage);
                dispatcher.Dispatch(new SubmitMessageSucceededAction());
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new SubmitMessageFailedAction(errorMessage: x.Message));
            }
        }
    }
}
