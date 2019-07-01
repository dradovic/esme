using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;
using AuthenticationState = esme.Client.Services.AuthenticationState; // FIXME: da, eliminate ours and use the provided one

namespace esme.Client.Pages
{
    public abstract class BalloonBase : ComponentBase
    {
        [Parameter]
        protected MessageViewModel Message { get; set; }

        [Inject]
        private AuthenticationState AuthenticationState { get; set; }

        protected bool IsMine
        {
            get
            {
                return Message.SenderName == AuthenticationState.User.UserName || // FIXME: da, rather check eq of Id
                    Message.IsBeingSent;
            }
        }
    }
}
