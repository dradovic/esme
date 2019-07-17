using Blazor.Fluxor;
using esme.Shared;
using Force.DeepCloner;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationFailedReducer : Reducer<InvitationsState, PostInvitationFailedAction>
    {
        public override InvitationsState Reduce(InvitationsState state, PostInvitationFailedAction action)
        {
            var newState = state.DeepClone();
            var invitation = newState.Invitations.SingleFirst(i => i.Id == action.Invitation.Id);
            invitation.Error = action.ErrorMessage;
            return newState;
        }
    }
}
