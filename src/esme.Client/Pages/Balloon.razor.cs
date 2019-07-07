using esme.Shared.Circles;
using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    public abstract class BalloonBase : ComponentBase
    {
        [Parameter]
        protected MessageViewModel Message { get; set; }

        [Inject]
        private AuthenticationState AuthenticationState { get; set; }

        protected bool IsMine // FIXME: da, compute on back-end and send to client
        {
            get
            {
                return Message.SenderName == AuthenticationState.User.Identity.Name ||
                    Message.IsBeingSent;
            }
        }
    }
}
