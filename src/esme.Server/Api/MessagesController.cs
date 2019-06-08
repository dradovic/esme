using esme.Infrastructure.Data;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
            var userId = _userManager.ParseUserId(User);
            if (!await UserIsInCircle(userId, circleId)) return NotFound();

            var messages = _db.Messages.Where(m => m.CircleId == circleId); // FIXME: da, implement paging
            return Ok(messages.Select(m => new MessageViewModel
            {
                Id = m.Id,
                Text = m.Text,
                SentAt = m.SentAt,
                SenderName = m.SenderName,
            })); // SUGGESTION: da, use AutoMapper
        }

        [HttpPost]
        public async Task<IActionResult> Messages(int circleId, [FromBody]MessageEditModel model)
        {
            var userId = _userManager.ParseUserId(User);
            if (!await UserIsInCircle(userId, circleId)) return NotFound();

            _db.Messages.Add(new Message
            {
                CircleId = circleId,
                Text = model.Text, // FIXME: da, validate model for max length
                SentAt = DateTimeOffset.UtcNow,
                SentBy = userId,
                SenderName = _userManager.GetUserName(User),
            }); ;
            await _db.SaveChangesAsync();
            return Ok();
        }

        private async Task<bool> UserIsInCircle(Guid userId, int circleId)
        {
            var user = await _db.Users.Include(u => u.Circles).SingleOrDefaultAsync(u => u.Id == userId);
            return user.AllCircles.Any(c => c.Id == circleId);
        }
    }
}
