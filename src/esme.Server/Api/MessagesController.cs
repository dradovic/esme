using esme.Infrastructure;
using esme.Infrastructure.Data;
using esme.Shared.Circles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [HttpPost]
        [Route("actions/read")]
        public async Task<ActionResult<IEnumerable<MessageViewModel>>> Messages(int circleId)
        {
            var userId = _userManager.ParseUserId(User);
            var circle = await GetMembershipIncludingCircle(userId, circleId);
            if (circle == null) return NotFound();

            var messages = _db.Messages.Where(m => m.CircleId == circleId); // FIXME: da, implement paging
            circle.NumberOfReadMessages = circle.Circle.NumberOfMessages;
            await _db.SaveChangesAsync();
            return Ok(messages.Select(ToViewModel));
        }

        [HttpPost]
        public async Task<IActionResult> Messages(int circleId, [FromBody]MessageEditModel model)
        {
            var userId = _userManager.ParseUserId(User);
            var circle = await GetMembershipIncludingCircle(userId, circleId);
            if (circle == null) return NotFound();

            Message message = new Message
            {
                CircleId = circleId,
                Text = model.Text, // FIXME: da, validate model for max length
                SentAt = DateTimeOffset.UtcNow,
                SentBy = userId,
                SenderName = _userManager.GetUserName(User),
            };
            _db.Messages.Add(message);
            circle.Circle.NumberOfMessages++; // FIXME: da, possible concurrent DB update exception?
            await _db.SaveChangesAsync();
            await _messagesHub.Clients.All.MessageAdded(1); // FIXME: da, do not send to *all* users
            return Ok();
        }

        private async Task<Membership> GetMembershipIncludingCircle(Guid userId, int circleId)
        {
            var user = await _db.Users
                .Include(u => u.Memberships)
                .ThenInclude(m => m.Circle)
                .SingleFirstOrDefaultAsync(u => u.Id == userId);
            return user.Memberships.SingleOrDefault(c => c.CircleId == circleId);
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
