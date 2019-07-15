using Blazor.Fluxor;
using Force.DeepCloner;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationFailedReducer : Reducer<InvitationsState, PostInvitationFailedAction>
    {
        public override InvitationsState Reduce(InvitationsState state, PostInvitationFailedAction action)
        {
            var newState = state.DeepClone();
            newState.ErrorMessage = action.ErrorMessage;
            return newState;
        }
    }
}
