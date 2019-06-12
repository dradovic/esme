using esme.Admin.Shared.Services;
using esme.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Admin.Infrastructure.Services
{
    public class SampleDataService : ISampleDataService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public SampleDataService(UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task ResetAllWithSampleData()
        {
            await DeleteAll();
            var users = await AddUsers();
            await AddData(users);
            await _db.SaveChangesAsync();
        }

        private async Task AddData(IEnumerable<ApplicationUser> users)
        {
            var openCircle = await _db.Circles.FindAsync(Circle.OpenCircleId);
            foreach (var user in users)
            {
                _db.Messages.Add(new Message
                {
                    CircleId = openCircle.Id,
                    Text = "Hello everybody!",
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
            user.Circles.Add(new CircleUser { UserId = user.Id, CircleId = Circle.OpenCircleId });
            return user;
        }

        private async Task DeleteAll()
        {
            await DeleteAllUsers();
            DeleteAllData();
        }

        private void DeleteAllData()
        {
            foreach (var circle in _db.Circles.Where(c => c.Id >= 0))
            {
                _db.Circles.Remove(circle);
            }
            foreach (var message in _db.Messages)
            {
                _db.Messages.Remove(message);
            }
        }

        private async Task DeleteAllUsers()
        {
            foreach (var user in _db.Users.ToArray())
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
