using Blazor.Fluxor;
using esme.Client.Store.Invitations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace esme.Client.Pages
{
    [Authorize]
    public abstract class InvitationsBase : ComponentBase
    {
        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        protected IState<InvitationsState> InvitationsState { get; private set; }

        protected override void OnInit()
        {
            InvitationsState.Subscribe(this);
            Dispatcher.Dispatch(new FetchInvitationsAction());
        }
    }
}
