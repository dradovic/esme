using esme.Admin.Shared.Services;
using esme.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
            AddData(users);
            await _db.SaveChangesAsync();
        }

        private void AddData(IEnumerable<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                _db.Messages.Add(new Message
                {
                    CircleId = Circle.OpenCircle.Id,
                    Text = "Hello everybody!",
                    SentAt = DateTimeOffset.UtcNow,
                    SentBy = user.Id,
                    SenderName = user.UserName,
                });
            }
        }

        private async Task<IEnumerable<ApplicationUser>> AddUsers()
        {
            var users = new List<ApplicationUser>();
            var user = new ApplicationUser
            {
                UserName = "Eva",
                Email = "eva@example.com",
            };
            var result = await _userManager.CreateAsync(user, "Eva_2019");
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Could not create sample user: {errors}.");
            }
            users.Add(user);
            user = new ApplicationUser
            {
                UserName = "Adam",
                Email = "adam@example.com",
            };
            result = await _userManager.CreateAsync(user, "Adam_2019");
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Could not create sample user: {errors}.");
            }
            users.Add(user);
            return users;
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
