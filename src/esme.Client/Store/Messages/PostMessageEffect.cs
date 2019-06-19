using Blazor.Fluxor;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class PostMessageEffect : Effect<PostMessageAction>
    {
        private readonly HttpClient _httpClient;

        public PostMessageEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task HandleAsync(PostMessageAction action, IDispatcher dispatcher)
        {
            try
            {
                var postedMessage = await _httpClient.PostJsonAsync<MessageViewModel>($"api/my/messages?circleId={action.CircleId}", action.NewMessage);
                dispatcher.Dispatch(new PostMessageSucceededAction(postedMessage));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new PostMessageFailedAction(errorMessage: x.Message));
            }
        }
    }
}
