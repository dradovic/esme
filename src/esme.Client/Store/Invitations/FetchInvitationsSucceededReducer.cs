using Blazor.Fluxor;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsSucceededReducer : Reducer<InvitationsState, FetchInvitationsSucceededAction>
    {
        public override InvitationsState Reduce(InvitationsState state, FetchInvitationsSucceededAction action)
        {
            return new InvitationsState(
                isLoading: false,
                errorMessage: null,
                invitations: action.Invitations);
        }
    }
}
