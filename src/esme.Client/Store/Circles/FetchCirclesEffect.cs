using Blazor.Fluxor;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Store.Circles
{
    public class FetchCirclesEffect : Effect<FetchCirclesAction>
    {
        private readonly HttpClient _httpClient;

        public FetchCirclesEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async override Task HandleAsync(FetchCirclesAction action, IDispatcher dispatcher)
        {
            try
            {
                var circles = await _httpClient.GetJsonAsync<CircleViewModel[]>("api/my/circles");
                dispatcher.Dispatch(new FetchCirclesSucceededAction(circles));
            }
            catch (Exception x)
            {
                dispatcher.Dispatch(new FetchCirclesFailedAction(errorMessage: x.Message));
            }
        }
    }
}
