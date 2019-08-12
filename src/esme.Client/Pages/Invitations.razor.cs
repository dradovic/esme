using Blazor.Fluxor;
using esme.Client.Store.Invitations;
using esme.Shared.Invitations;
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

        protected InvitationEditModel NewInvitation { get; private set; } = new InvitationEditModel();

        protected override void OnInit()
        {
            InvitationsState.Subscribe(this);
            Dispatcher.Dispatch(new FetchInvitationsAction());
        }

        protected void OnSubmit()
        {
            // FIXME: da, limit the number of invitations that can be sent
            Dispatcher.Dispatch(new PostInvitationAction(NewInvitation));
            NewInvitation = new InvitationEditModel(); // start over
        }
    }
}
