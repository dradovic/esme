using Blazor.Fluxor;
using esme.Shared.Invitations;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsSucceededAction : IAction
    {
        public FetchInvitationsSucceededAction(InvitationViewModel[] invitations)
        {
            Invitations = invitations;
        }

        public InvitationViewModel[] Invitations { get; }
    }
}
