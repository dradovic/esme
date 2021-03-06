using esme.Admin.Shared.Services;
using esme.Infrastructure.Data;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Admin.Infrastructure.Services
{
    public class DataService : IDataService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public DataService(UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task ResetAllWithSampleDataAsync()
        {
            await DeleteAllAsync();
            var users = await AddUsers();
            await AddData(users);
            await _db.SaveChangesAsync();
        }

        private async Task AddData(IEnumerable<ApplicationUser> users)
        {
            var openCircle = await _db.Circles.FindAsync(Circle.OpenCircleId);
            openCircle.NumberOfMessages = 0;
            foreach (var user in users)
            {
                _db.Messages.Add(new Message
                {
                    Id = Guid.NewGuid(),
                    CircleId = openCircle.Id,
                    ContentType = ContentType.Text,
                    Content = "Hello everybody!",
                    SentAt = DateTimeOffset.UtcNow,
                    SentBy = user.Id,
                    SenderName = user.UserName,
                });
                openCircle.NumberOfMessages++;
            }
        }

        private async Task<IEnumerable<ApplicationUser>> AddUsers()
        {
            var users = new List<ApplicationUser>();
            users.Add(await CreateUser(new ApplicationUser
            {
                UserName = "Eva",
                Email = "eva@example.com",
            }));
            users.Add(await CreateUser(new ApplicationUser
            {
                UserName = "Adam",
                Email = "adam@example.com",
            }));
            return users;
        }

        private async Task<ApplicationUser> CreateUser(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user, $"{user.UserName}_2019");
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Could not create sample user: {errors}.");
            }
            user.Memberships.Add(new Membership { UserId = user.Id, CircleId = Circle.OpenCircleId });
            return user;
        }

        public async Task DeleteAllAsync()
        {
            DeleteAllInvitations();
            await DeleteAllUsers();
            DeleteAllData();
        }

        private void DeleteAllInvitations()
        {
            _db.Invitations.RemoveAll();
        }

        private async Task DeleteAllUsers()
        {
            foreach (var user in _db.Users.ToArray())
            {
                await _userManager.DeleteAsync(user);
            }
        }

        private void DeleteAllData()
        {
            foreach (var circle in _db.Circles.Where(c => c.Id != Circle.OpenCircleId))
            {
                _db.Circles.Remove(circle);
            }
            _db.Messages.RemoveAll();
        }

        public void MigrateDatabase()
        {
            int? timeout = _db.Database.GetCommandTimeout();
            _db.Database.SetCommandTimeout(600);
            try
            {
                _db.Database.Migrate();
            }
            finally
            {
                _db.Database.SetCommandTimeout(timeout);
            }
        }
    }
}
