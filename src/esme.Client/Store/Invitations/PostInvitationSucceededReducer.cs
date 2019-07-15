using Blazor.Fluxor;
using Force.DeepCloner;
using System;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationSucceededReducer : Reducer<InvitationsState, PostInvitationSucceededAction>
    {
        public override InvitationsState Reduce(InvitationsState state, PostInvitationSucceededAction action)
        {
            var newState = state.DeepClone();
            newState.Merge(action.PostedInvitation);
            return newState;
        }
    }
}
