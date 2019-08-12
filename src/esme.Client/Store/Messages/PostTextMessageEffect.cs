using Blazor.Fluxor;
using esme.Shared;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class PostTextMessageEffect : Effect<PostTextMessageAction>
    {
        private readonly HttpClient _httpClient;

        public PostTextMessageEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task HandleAsync(PostTextMessageAction action, IDispatcher dispatcher)
        {
            try
            {
                var postedMessage = await _httpClient.PostJsonAsync<MessageViewModel>(Urls.GetPostTextMessageUrl(action.CircleId), action.Message);
                dispatcher.Dispatch(new PostMessageSucceededAction(postedMessage));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new PostTextMessageFailedAction(errorMessage: x.Message));
            }
        }
    }
}
