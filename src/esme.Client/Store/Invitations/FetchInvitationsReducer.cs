using Blazor.Fluxor;
using esme.Shared.Invitations;
using System.Collections.Generic;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsReducer : Reducer<InvitationsState, FetchInvitationsAction>
    {
        public override InvitationsState Reduce(InvitationsState state, FetchInvitationsAction action)
        {
            return new InvitationsState(
                isLoading: true,
                errorMessage: null,
                invitations: new List<InvitationViewModel>());
        }
    }
}
