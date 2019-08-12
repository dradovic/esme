using esme.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace esme.Server.Api
{
    public static class UserManagerExtensions
    {
        public static Guid ParseUserId(this UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
        {
            return Guid.Parse(userManager.GetUserId(user));
        }
    }
}
