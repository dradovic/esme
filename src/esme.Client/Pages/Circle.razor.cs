using Blazor.Extensions;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class CircleBase : ComponentBase
    {
        protected List<MessageViewModel> Messages { get; private set; }
        protected MessageEditModel NewMessage { get; private set; } = new MessageEditModel();

        [Parameter]
        protected int Id { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private HubConnectionBuilder HubConnectionBuilder { get; set; }

        protected override async Task OnInitAsync()
        {
            // FIXME: da, disable send button while setup is going on (see BlazorChat sample)?
            await SetupSignalRConnection();
            await LoadMessages();
        }

        private async Task SetupSignalRConnection()
        {
            var connection = HubConnectionBuilder
                .WithUrl("/my/hub", // if the Hub is hosted on the server where the blazor is hosted, you can just use the relative path
                options =>
                {
                    options.LogLevel = SignalRLogLevel.Trace; // client log level
                    options.Transport = HttpTransportType.WebSockets;
                })
                .Build();

            connection.On<int>("MessageAdded", OnMessageAdded);
            await connection.StartAsync();
        }

        private async Task OnMessageAdded(int m)
        {
            await LoadMessages();
            StateHasChanged();
        }

        private async Task LoadMessages()
        {
            var messages = await Http.GetJsonAsync<IEnumerable<MessageViewModel>>($"api/my/messages?circleId={Id}");
            Messages = messages.ToList();
        }

        protected async Task OnSubmit()
        {
            await Http.PostJsonAsync($"api/my/messages?circleId={Id}", NewMessage); // FIXME: da, handle failure
        }
    }
}
