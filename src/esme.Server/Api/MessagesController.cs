using esme.Infrastructure.Data;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly ApplicationDbContext _db; // FEATURE: da, use MediatR
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<MessagesHub, IMessagesHub> _messagesHub;

        public MessagesController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IHubContext<MessagesHub, IMessagesHub> messagesHub)
        {
            _db = db;
            _userManager = userManager;
            _messagesHub = messagesHub;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageViewModel>>> Messages(int circleId)
        {
            var userId = _userManager.ParseUserId(User);
            if (!await UserIsInCircle(userId, circleId)) return NotFound();

            var messages = _db.Messages.Where(m => m.CircleId == circleId); // FIXME: da, implement paging
            return Ok(messages.Select(ToViewModel));
        }

        [HttpPost]
        public async Task<IActionResult> Messages(int circleId, [FromBody]MessageEditModel model)
        {
            var userId = _userManager.ParseUserId(User);
            if (!await UserIsInCircle(userId, circleId)) return NotFound();

            Message message = new Message
            {
                CircleId = circleId,
                Text = model.Text, // FIXME: da, validate model for max length
                SentAt = DateTimeOffset.UtcNow,
                SentBy = userId,
                SenderName = _userManager.GetUserName(User),
            };
            _db.Messages.Add(message); ;
            await _db.SaveChangesAsync();
            await _messagesHub.Clients.All.MessageAdded(1); // FIXME: da, do not send to *all* users
            return Ok();
        }

        private async Task<bool> UserIsInCircle(Guid userId, int circleId)
        {
            var user = await _db.Users.Include(u => u.Circles).SingleOrDefaultAsync(u => u.Id == userId);
            return user.AllCircles.Any(c => c.Id == circleId);
        }

        private static MessageViewModel ToViewModel(Message message)
        {
            return new MessageViewModel
            {
                Id = message.Id,
                Text = message.Text,
                SentAt = message.SentAt,
                SenderName = message.SenderName,
            };
        }
    }
}
