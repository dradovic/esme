using Blazor.Fluxor;
using esme.Shared.Invitations;
using System.Collections.Generic;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsFailedReducer : Reducer<InvitationsState, FetchInvitationsFailedAction>
    {
        public override InvitationsState Reduce(InvitationsState state, FetchInvitationsFailedAction action)
        {
            return new InvitationsState(
                isLoading: false,
                errorMessage: action.ErrorMessage,
                invitations: new List<InvitationViewModel>());
        }
    }
}
