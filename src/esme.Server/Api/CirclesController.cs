using esme.Infrastructure;
using esme.Infrastructure.Data;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Server.Api
{
    [Route("api/my/[action]")] // FIXME: da, move to Urls
    [ApiController] // FIXME: da, decorate on assembly level
    [Authorize]
    public class CirclesController : ControllerBase
    {
        private readonly ApplicationDbContext _db; // FIXME: da, use MediatR
        private readonly UserManager<ApplicationUser> _userManager;

        public CirclesController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CircleViewModel>>> Circles()
        {
            var userId = _userManager.ParseUserId(User);
            var user = await _db.Users
                .Include(u => u.Memberships)
                .ThenInclude(m => m.Circle)
                .SingleFirstOrDefaultAsync(u => u.Id == userId);
            return Ok(user.Memberships.Select(m => new CircleViewModel {
                Id = m.Circle.Id,
                Name = m.Circle.Name,
                NumberOfUnreadMessages = m.NumberOfUnreadMessages,
            })); // FEATURE: da, use AutoMapper
        }
    }
}
