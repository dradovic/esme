using esme.Shared;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public class MessagesApi // FIXME: da, move all API calls to this class
    {
        private readonly HttpClient _httpClient;

        public MessagesApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MessageViewModel>> ReadMessages(Guid circleId, ReadMessagesOptions options)
        {
            return await _httpClient.PostJsonAsync<IEnumerable<MessageViewModel>>(Urls.GetPostReadMessages(circleId), options);
        }
    }
}
