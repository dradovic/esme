using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Invitations
{
    public class InvitationViewModel
    {
        public Guid Id { get; set; }

        [EmailAddress]
        public string To { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public bool IsAccepted { get; set; }

        public string Error { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Error);

        public void Merge(InvitationViewModel storedInvitation)
        {
            if (Id != storedInvitation.Id) return; // FIXME: da, log warning (should never be the case)

            SentAt = storedInvitation.SentAt;
        }
    }
}
