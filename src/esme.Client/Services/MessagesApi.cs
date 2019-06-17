using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public class MessagesApi
    {
        private readonly HttpClient _httpClient;

        public MessagesApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MessageViewModel>> ReadMessages(int circleId, ReadMessagesOptions options)
        {
            return await _httpClient.PostJsonAsync<IEnumerable<MessageViewModel>>($"api/my/messages/actions/read?circleId={circleId}", options);
        }
    }
}
