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
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ApplicationDbContext _db;

        public UsersService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public async Task<bool> IsAmbassador(Guid userId)
        {
            var user = await FindUserById(userId);
            return await _userManager.IsInRoleAsync(user, ApplicationRoles.Ambassador);
        }

        public async Task GrantAmbassador(Guid userId)
        {
            await CreateRoleIfNotExists(ApplicationRoles.Ambassador);
            var user = await FindUserById(userId);
            await _userManager.AddToRoleAsync(user, ApplicationRoles.Ambassador);
        }

        public async Task DenyAmbassador(Guid userId)
        {
            var user = await FindUserById(userId);
            await _userManager.RemoveFromRoleAsync(user, ApplicationRoles.Ambassador);
        }

        private async Task CreateRoleIfNotExists(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole<Guid>
                {
                    Name = roleName
                };
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task<ApplicationUser> FindUserById(Guid userId)
        {
            return await _db.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
        }

    }
}
