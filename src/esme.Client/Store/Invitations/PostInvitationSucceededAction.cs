using Blazor.Fluxor;
using esme.Shared.Invitations;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationSucceededAction : IAction
    {
        public PostInvitationSucceededAction(InvitationViewModel postedInvitation)
        {
            PostedInvitation = postedInvitation;
        }

        public InvitationViewModel PostedInvitation { get; }
    }
}