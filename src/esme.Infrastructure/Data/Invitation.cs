using esme.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esme.Infrastructure.Data
{
    public class Invitation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string To { get; set; }

        public ApplicationUser SentBy { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public DateTimeOffset? AcceptedAt { get; set; }
    }
}
