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

        public SampleDataService(IOptions<SampleDataOptions> options, UserManager<ApplicationUser> userManager)
        {
            _options = options.Value;
            _userManager = userManager;
        }

        public async Task ResetAllWithSampleData()
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
    }

    public class SampleDataOptions
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
