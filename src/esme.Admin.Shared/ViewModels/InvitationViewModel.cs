using esme.Shared;
using System;

namespace esme.Admin.Shared.ViewModels
{
    public class InvitationViewModel
    {
        public Guid Id { get; set; }
        public string To { get; set; }
        public string SentBy { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public string Error { get; set; }
        public DateTimeOffset? AcceptedAt { get; set; }

        public bool IsAccepted => AcceptedAt.HasValue;
        public bool Expired => !IsAccepted && SentAt.AddDays(Constants.JoinInvitationExpirationDays) < DateTimeOffset.UtcNow;

    }
}
