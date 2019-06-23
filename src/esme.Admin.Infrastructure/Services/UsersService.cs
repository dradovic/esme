using System;
using System.Linq;
using System.Threading.Tasks;
using esme.Admin.Shared.Services;
using esme.Infrastructure.Data;
using esme.Shared.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace esme.Admin.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public UsersService(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task GrantAmbassador(Guid userId)
        {
            var user = await _db.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
            await _userManager.AddToRoleAsync(user, Roles.Ambassador);
        }
    }
}
