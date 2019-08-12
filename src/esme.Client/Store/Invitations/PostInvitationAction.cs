using esme.Shared.Invitations;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationAction
    {
        public PostInvitationAction(InvitationEditModel invitation)
        {
            Invitation = invitation;
        }

        public InvitationEditModel Invitation { get; }
    }
}
