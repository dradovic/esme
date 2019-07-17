using Blazor.Fluxor;
using esme.Shared.Invitations;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationFailedAction : IAction
    {
        public PostInvitationFailedAction(InvitationEditModel invitation, string errorMessage)
        {
            Invitation = invitation;
            ErrorMessage = errorMessage;
        }

        public InvitationEditModel Invitation { get; }
        public string ErrorMessage { get; }
    }
}