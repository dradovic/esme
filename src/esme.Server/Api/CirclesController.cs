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
    [Route("api/my/[controller]/[action]")]
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

        public async Task<ActionResult<IEnumerable<CircleViewModel>>> GetCircles()
        {
            string userId = _userManager.GetUserId(User);
            var user = await _db.Users.Include(u => u.Circles).SingleOrDefaultAsync(u => u.Id == userId);
            return Ok(user.Circles.Select(c => new CircleViewModel { Name = c.Circle.Name })); // FIXME: da, use AutoMapper
        }
    }
}
