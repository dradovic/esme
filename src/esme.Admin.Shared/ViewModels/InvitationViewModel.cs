using System;

namespace esme.Admin.Shared.ViewModels
{
    public class InvitationViewModel
    {
        public Guid Id { get; set; }
        public string To { get; set; }
        public string SentBy { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public DateTimeOffset? AcceptedAt { get; set; }
    }
}
