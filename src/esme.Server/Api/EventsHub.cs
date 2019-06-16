using esme.Shared.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace esme.Server.Api
{
    [Authorize]
    public class EventsHub : Hub<IEventsHub>
    {
    }
}
