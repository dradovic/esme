using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esme.Infrastructure.Data
{
    public class Circle
    {
        public static readonly Guid OpenCircleId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Membership> Memberships { get; set; }

        public int NumberOfMessages { get; set; }
    }
}
