using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace esme.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<CircleUser> Circles { get; set; } = new List<CircleUser>();

        public IEnumerable<Circle> AllCircles => new[] { Circle.OpenCircle }.Union(Circles.Select(cu => cu.Circle));
    }
}