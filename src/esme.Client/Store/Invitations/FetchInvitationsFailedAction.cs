using Blazor.Fluxor;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsFailedAction : IAction
    {
        public FetchInvitationsFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
