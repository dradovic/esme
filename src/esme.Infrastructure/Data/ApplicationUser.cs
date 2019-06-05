using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace esme.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public List<CircleUser> Circles { get; set; }
    }
}