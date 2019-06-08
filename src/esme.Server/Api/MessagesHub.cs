using esme.Shared.Circles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace esme.Server.Api
{
    public interface IMessagesHub
    {
        Task MessageAdded(int n);
    }

    [Authorize]
    public class MessagesHub : Hub<IMessagesHub>
    {
    }
}
