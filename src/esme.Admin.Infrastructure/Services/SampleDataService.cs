using esme.Admin.Shared.Services;
using esme.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Admin.Infrastructure.Services
{
    public class SampleDataService : ISampleDataService
    {
        private readonly SampleDataOptions _options;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public SampleDataService(IOptions<SampleDataOptions> options, UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            _options = options.Value;
            _userManager = userManager;
            _db = db;
        }

        public async Task ResetAllWithSampleData()
        {
            await DeleteAll();
            await AddUser();
            AddData();
            await _db.SaveChangesAsync();
        }

        private void AddData()
        {
            //_db.Circles.Add(new Circle
            //{
            //    Name = "My First Circle",
            //});
            _db.Messages.Add(new Message
            {
                CircleId = Circle.OpenCircle.Id,
                Text = "Hello everybody!",
                SentAt = DateTimeOffset.UtcNow,
                SentBy = _options.UserName,
            });
        }

        private async Task AddUser()
        {
            var user = new ApplicationUser
            {
                UserName = _options.UserName,
                Email = _options.Email,
            };
            var result = await _userManager.CreateAsync(user, _options.Password);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Could not create sample user: {errors}.");
            }
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

    public class SampleDataOptions
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
