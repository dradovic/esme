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
    }
}
