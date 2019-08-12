using esme.Shared.Invitations;
using System.Collections.Generic;

namespace esme.Client.Store.Invitations
{
    public class FetchInvitationsSucceededAction
    {
        public FetchInvitationsSucceededAction(List<InvitationViewModel> invitations)
        {
            Invitations = invitations;
        }

        public List<InvitationViewModel> Invitations { get; }
    }
}
