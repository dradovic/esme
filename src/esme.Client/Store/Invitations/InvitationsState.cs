using esme.Shared.Invitations;
using System;
using System.Collections.Generic;

namespace esme.Client.Store.Invitations
{
    public class InvitationsState
    {
        public InvitationsState(bool isLoading, string errorMessage, List<InvitationViewModel> invitations)
        {
            IsLoading = isLoading;
            ErrorMessage = errorMessage;
            Invitations = invitations;
        }

        public bool IsLoading { get; }
        public string ErrorMessage { get; set; }
        public List<InvitationViewModel> Invitations { get; set; }

        public bool IsLoadedWithoutErrors => !IsLoading && ErrorMessage == null;

        internal void Merge(InvitationViewModel invitation)
        {
            if (Invitations == null) Invitations = new List<InvitationViewModel>();
            int index = Invitations.FindIndex(m => m.Id == invitation.Id);
            if (index >= 0)
            {
                Invitations[index].Merge(invitation);
            }
            else
            {
                Invitations.Add(invitation);
            }
        }
    }
}
