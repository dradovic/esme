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
        public static async Task<TResult> PostAsync<TResult>(this HttpClient httpClient, string requestUri, object payload, Action<string> onError)
        {
            var payloadContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"); // FIXME: da, remove ref. to Newtonsoft and use JsonSerializer
            //var payloadContent = new StringContent(JsonSerializer.ToString(payload), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestUri, payloadContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResult>(responseContent);
                //return JsonSerializer.Parse<TResult>(responseContent);
            }
            else
            {
                onError(responseContent);
                return default;
            }
        }

        // FIXME: da, use this method everywhere
        public static async Task<string> PostAsync(this HttpClient httpClient, string requestUri, object payload)
        {
            var payloadContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"); // FIXME: da, remove ref. to Newtonsoft and use JsonSerializer
            //var payloadContent = new StringContent(JsonSerializer.ToString(payload), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestUri, payloadContent);
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null; // success
        }
    }
}
