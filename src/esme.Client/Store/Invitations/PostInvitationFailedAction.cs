using Blazor.Fluxor;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationFailedAction : IAction
    {
        public PostInvitationFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}