using Blazor.Fluxor;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsFailedReducer : Reducer<InvitationsState, FetchInvitationsFailedAction>
    {
        public override InvitationsState Reduce(InvitationsState state, FetchInvitationsFailedAction action)
        {
            return new InvitationsState(
                isLoading: false,
                errorMessage: action.ErrorMessage,
                invitations: null);
        }
    }
}
