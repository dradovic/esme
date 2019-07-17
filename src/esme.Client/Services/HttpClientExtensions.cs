using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System;

namespace esme.Client.Services
{
    public static class HttpClientExtensions
    {
        // FIXME: da, use this method everywhere
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string requestUri, object payload, Action<string> onError)
        {
            var payloadContent = new StringContent(JsonSerializer.ToString(payload), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestUri, payloadContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Parse<T>(responseContent);
            }
            else
            {
                onError(responseContent);
                return default;
            }
        }
    }
}
