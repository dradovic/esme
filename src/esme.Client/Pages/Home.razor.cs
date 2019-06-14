using Blazor.Extensions;
using Blazor.Fluxor;
using esme.Client.Store.Circles;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class HomeBase : ComponentBase
    {
        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        protected IState<CirclesState> CirclesState { get; private set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        [Inject]
        private HubConnectionBuilder HubConnectionBuilder { get; set; }

        protected override async Task OnInitAsync()
        {
            await SetupSignalRConnection();
            CirclesState.Subscribe(this);
            Dispatcher.Dispatch(new FetchCirclesAction());
        }

        protected void CircleClick(CircleViewModel circle)
        {
            UriHelper.NavigateTo($"/circle/{circle.Id}");
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

        private Task OnMessageAdded(int circleId)
        {
            Dispatcher.Dispatch(new IncrementUnreadMessagesAction(circleId));
            return Task.CompletedTask;
        }
    }
}
