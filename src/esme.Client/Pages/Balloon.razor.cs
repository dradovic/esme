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

        protected bool IsMine // FIXME: da, implement with <AuthorizeView> where {user.Identity.Name} is available
        {
            get
            {
                return Message.SenderName == AuthenticationState.User.Identity.Name || // FIXME: da, rather check eq of Id
                    Message.IsBeingSent;
            }
        }
    }
}
