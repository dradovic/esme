using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace esme.Infrastructure.Data
{
    public class Circle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<CircleUser> Users { get; set; }
    }
}
