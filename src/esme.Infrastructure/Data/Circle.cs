using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace esme.Infrastructure.Data
{
    public class Circle
    {
        public const int OpenCircleId = -1;

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<CircleUser> Users { get; set; }

        public int NumberOfMessages { get; set; }
    }
}
