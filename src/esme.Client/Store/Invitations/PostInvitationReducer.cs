using Blazor.Fluxor;
using esme.Shared.Invitations;
using Force.DeepCloner;

namespace esme.Client.Store.Invitations
{
    public class PostInvitationReducer : Reducer<InvitationsState, PostInvitationAction>
    {
        public override InvitationsState Reduce(InvitationsState state, PostInvitationAction action)
        {
            var newState = state.DeepClone();
            if (newState.Invitations != null)
            {
                var invitationViewModel = new InvitationViewModel
                {
                    Id = action.Invitation.Id,
                    To = action.Invitation.To,
                };
                newState.Invitations.Add(invitationViewModel);
            }
            return newState;
        }
    }
}
