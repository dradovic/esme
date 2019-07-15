using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Invitations
{
    public class InvitationEditModel
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // setter used by JSON deserializer

        [Required]
        [EmailAddress]
        public string To { get; set; }
    }
}
