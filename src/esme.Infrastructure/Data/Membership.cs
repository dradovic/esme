using System;

namespace esme.Infrastructure.Data
{
    public class Membership
    {
        public int CircleId { get; set; }
        public Circle Circle { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int NumberOfReadMessages { get; set; }
    }
}
