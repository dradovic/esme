using Blazor.Extensions;
using esme.Shared.Events;
using EventAggregator.Blazor;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public class ClientHub
    {
        private readonly HubConnectionBuilder _connectionBuilder;
        private readonly IEventAggregator _eventAggregator;

        public ClientHub(HubConnectionBuilder connectionBuilder, IEventAggregator eventAggregator)
        {
            _connectionBuilder = connectionBuilder;
            _eventAggregator = eventAggregator;
        }

        private HubConnection Connection { get; set; }

        public async Task SetupConnection()
        {
            if (Connection == null)
            {
                Connection = _connectionBuilder
                    .WithUrl("/my/hub", // if the Hub is hosted on the server where the blazor is hosted, you can just use the relative path
                    options =>
                    {
                        options.LogLevel = SignalRLogLevel.Trace; // client log level
                    options.Transport = HttpTransportType.WebSockets;
                    })
                    .Build();

                Connection.On<MessagePostedEvent>("MessagePosted", OnMessagePosted); // FIXME: da, use nameof?
                await Connection.StartAsync();
            }
        }

        private async Task OnMessagePosted(MessagePostedEvent messagePostedEvent)
        {
            await _eventAggregator.PublishAsync(messagePostedEvent);
        }
    }
}
