using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace esme.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Membership> Memberships { get; set; } = new List<Membership>();
    }
}