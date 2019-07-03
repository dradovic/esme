using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace esme.Infrastructure.Data
{
    public class Membership
    {
        public Guid CircleId { get; set; }
        public Circle Circle { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int NumberOfReadMessages { get; set; }

        [NotMapped]
        public int NumberOfUnreadMessages => Circle.NumberOfMessages - NumberOfReadMessages;
    }
}
