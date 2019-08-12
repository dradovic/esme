namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsFailedAction
    {
        public FetchInvitationsFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
