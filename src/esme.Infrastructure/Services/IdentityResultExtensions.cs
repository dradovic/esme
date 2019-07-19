using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace esme.Infrastructure.Services
{
    public static class IdentityResultExtensions
    {
        public static string ToErrorString(this IdentityResult result)
        {
            return string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
        }
    }
}
