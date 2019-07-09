using Blazor.Fluxor;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsReducer : Reducer<InvitationsState, FetchInvitationsAction>
    {
        public override InvitationsState Reduce(InvitationsState state, FetchInvitationsAction action)
        {
            return new InvitationsState(
                isLoading: true,
                errorMessage: null,
                invitations: null);
        }
    }
}
