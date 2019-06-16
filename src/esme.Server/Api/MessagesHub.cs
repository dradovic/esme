using esme.Shared.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace esme.Server.Api
{
    public interface IMessagesHub
    {
        Task MessagePosted(MessagePostedEvent messagePostedEvent);
    }

    [Authorize]
    public class MessagesHub : Hub<IMessagesHub>
    {
    }
}
