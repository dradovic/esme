using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace esme.Infrastructure.Data
{
    public class Circle
    {
        public static readonly Circle OpenCircle = new Circle {
            Id = -1,
            Name = "Open Circle",
        };

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<CircleUser> Users { get; set; }
    }
}
