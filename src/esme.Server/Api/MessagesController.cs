using esme.Infrastructure;
using esme.Infrastructure.Data;
using esme.Shared.Circles;
using esme.Shared.Events;
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
        private readonly IHubContext<EventsHub, IEventsHub> _hub;

        public MessagesController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IHubContext<EventsHub, IEventsHub> hub)
        {
            _db = db;
            _userManager = userManager;
            _hub = hub;
        }

        [HttpPost]
        [Route("actions/read")]
        public async Task<ActionResult<IEnumerable<MessageViewModel>>> Messages(int circleId, [FromBody]ReadMessagesOptions options)
        {
            var userId = _userManager.ParseUserId(User);
            var membership = await GetMembershipIncludingCircle(userId, circleId);
            if (membership == null) return NotFound();

            IEnumerable<Message> messages = _db.Messages
                .Where(m => m.CircleId == circleId)
                .OrderBy(m => m.SentAt); // FIXME: da, implement paging
            if (options == ReadMessagesOptions.Unread)
            {
                messages = messages.TakeLast(membership.NumberOfUnreadMessages); // FIXME: da, check generated SQL
            }
            membership.NumberOfReadMessages = membership.Circle.NumberOfMessages;
            await _db.SaveChangesAsync();
            Debug.Assert(membership.NumberOfUnreadMessages == 0);
            return Ok(messages.Select(ToViewModel));
        }

        [HttpPost]
        public async Task<IActionResult> Messages(int circleId, [FromBody]MessageEditModel model)
        {
            var userId = _userManager.ParseUserId(User);
            var membership = await GetMembershipIncludingCircle(userId, circleId);
            if (membership == null) return NotFound();

            Message message = new Message
            {
                CircleId = circleId,
                Text = model.Text, // FIXME: da, validate model for max length
                SentAt = DateTimeOffset.UtcNow,
                SentBy = userId,
                SenderName = _userManager.GetUserName(User),
            };
            _db.Messages.Add(message);
            membership.Circle.NumberOfMessages++; // FIXME: da, possible concurrent DB update exception?
            await _db.SaveChangesAsync();
            await _hub.Clients.All.MessagePosted(new MessagePostedEvent { CircleId = circleId }); // FIXME: da, do not send to *all* users
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
