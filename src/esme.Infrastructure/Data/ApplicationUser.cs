using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace esme.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<CircleUser> Circles { get; set; } = new List<CircleUser>();

        [NotMapped]
        public IEnumerable<Circle> AllCircles => new[] { Circle.OpenCircle }.Union(Circles.Select(cu => cu.Circle));
    }
}