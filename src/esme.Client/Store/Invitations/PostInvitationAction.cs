using Blazor.Fluxor;
using esme.Shared.Invitations;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationAction : IAction
    {
        public PostInvitationAction(InvitationEditModel invitation)
        {
            Invitation = invitation;
        }

        public InvitationEditModel Invitation { get; }
    }
}
