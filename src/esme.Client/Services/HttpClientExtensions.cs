using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System;
using Newtonsoft.Json;

namespace esme.Client.Services
{
    public static class HttpClientExtensions
    {
        // FIXME: da, use this method everywhere
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string requestUri, object payload, Action<string> onError)
        {
            var payloadContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"); // FIXME: da, remove ref. to Newtonsoft and use JsonSerializer
            //var payloadContent = new StringContent(JsonSerializer.ToString(payload), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestUri, payloadContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(responseContent);
                //return JsonSerializer.Parse<T>(responseContent);
            }
            else
            {
                onError(responseContent);
                return default;
            }
        }
    }
}
