using esme.Shared.Invitations;

namespace esme.Client.Store.Invitations
{
    public class InvitationsState
    {
        public InvitationsState(bool isLoading, string errorMessage, InvitationViewModel[] invitations)
        {
            IsLoading = isLoading;
            ErrorMessage = errorMessage;
            Invitations = invitations;
        }

        public bool IsLoading { get; }
        public string ErrorMessage { get; }
        public InvitationViewModel[] Invitations { get; }
    }
}
