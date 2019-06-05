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
    [Route("api/my/[action]")]
    [ApiController] // FIXME: da, decorate on assembly level
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _db; // FIXME: da, use MediatR
        private readonly UserManager<ApplicationUser> _userManager;

        public MessagesController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageViewModel>>> Messages(int circleId)
        {
            string userId = _userManager.GetUserId(User);
            var user = await _db.Users.Include(u => u.Circles).SingleOrDefaultAsync(u => u.Id == userId);
            if (!user.AllCircles.Any(c => c.Id == circleId))
            {
                return NotFound();
            }

            var messages = _db.Messages
                .Where(m => m.CircleId == circleId)
                .OrderBy(m => m.Id); // FIXME: da, implement paging
            return Ok(messages.Select(m => new MessageViewModel
            {
                Id = m.Id,
                Text = m.Text,
                SentAt = m.SentAt,
                SentBy = m.SentBy,
            })); // FIXME: da, use AutoMapper
        }
    }
}
