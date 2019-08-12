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
        private readonly ILogger<ClientHub> _logger;

        public ClientHub(HubConnectionBuilder connectionBuilder, IEventAggregator eventAggregator, ILogger<ClientHub> logger)
        {
            _connectionBuilder = connectionBuilder;
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        private HubConnection Connection { get; set; }

        public async Task Connect()
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
                    .Build(); // can only be called once
            }
            Connection.On<MessagePostedEvent>(nameof(IEventsHub.MessagePosted), OnMessagePosted);
            await Connection.StartAsync();
        }

        public async Task Disconnect()
        {
            if (Connection != null)
            {
                await Connection.StopAsync();
            }
        }

        private async Task OnMessagePosted(MessagePostedEvent messagePostedEvent)
        {
            _logger.LogInformation("OnMessagePosted.MessageId: {0}", messagePostedEvent.MessageId);
            await _eventAggregator.PublishAsync(messagePostedEvent);
        }
    }
}
