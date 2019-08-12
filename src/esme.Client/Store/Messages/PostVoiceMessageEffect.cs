using Blazor.Fluxor;
using esme.Shared;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Store.Messages
{
    public class PostVoiceMessageEffect : Effect<PostVoiceMessageAction>
    {
        private readonly HttpClient _httpClient;

        public PostVoiceMessageEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task HandleAsync(PostVoiceMessageAction action, IDispatcher dispatcher)
        {
            try
            {
                action.Message.Recording = await _httpClient.GetByteArrayAsync(action.RecordingUrl); // read the recording from the browser's blob storage
                var postedMessage = await _httpClient.PostJsonAsync<MessageViewModel>(Urls.GetPostVoiceMessageUrl(action.CircleId), action.Message);
                dispatcher.Dispatch(new PostMessageSucceededAction(postedMessage));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new PostVoiceMessageFailedAction(errorMessage: x.Message));
            }
        }
    }
}
