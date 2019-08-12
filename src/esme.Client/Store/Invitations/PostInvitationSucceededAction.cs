using esme.Shared.Invitations;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationSucceededAction
    {
        public PostInvitationSucceededAction(InvitationViewModel postedInvitation)
        {
            PostedInvitation = postedInvitation;
        }

        public InvitationViewModel PostedInvitation { get; }
    }
}