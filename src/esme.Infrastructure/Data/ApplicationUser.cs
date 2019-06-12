using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace esme.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<CircleUser> Circles { get; set; } = new List<CircleUser>();
    }
}