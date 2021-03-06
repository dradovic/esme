﻿using esme.Infrastructure;
using esme.Infrastructure.Data;
using esme.Infrastructure.Services;
using esme.Shared;
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
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _db; // FEATURE: da, use MediatR
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AzureBlobStorage _storage;
        private readonly IHubContext<EventsHub, IEventsHub> _hub;

        public MessagesController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            AzureBlobStorage storage,
            IHubContext<EventsHub, IEventsHub> hub)
        {
            _db = db;
            _userManager = userManager;
            _storage = storage;
            _hub = hub;
        }

        [HttpPost]
        [Route(Urls.PostReadMessages)]
        public async Task<ActionResult<IEnumerable<MessageViewModel>>> Messages(Guid circleId, [FromBody]ReadMessagesOptions options)
        {
            Guid userId = _userManager.ParseUserId(User);
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
            return Ok(messages.Select(m => new MessageViewModel
            {
                Id = m.Id,
                ContentType = m.ContentType,
                Content = m.Content,
                SentAt = m.SentAt,
                SentByMe = m.SentBy == userId,
                SenderName = m.SenderName,
            }));
        }

        [HttpPost]
        [Route(Urls.PostTextMessage)]
        public Task<ActionResult<MessageViewModel>> Messages(Guid circleId, [FromBody]TextMessageEditModel model)
        {
            return Messages(circleId, model.Id, ContentType.Text, () => Task.FromResult(model.Text) );
        }

        [HttpPost]
        [Route(Urls.PostVoiceMessage)]
        public Task<ActionResult<MessageViewModel>> Messages(Guid circleId, [FromBody]VoiceMessageEditModel model)
        {
            return Messages(circleId, model.Id, ContentType.Voice, async () => {
                var blobUri = await _storage.StoreBytesAsync(circleId, $"{model.Id}.mp3", model.Recording, "messages/voice");
                return blobUri.AbsoluteUri;
            });
        }

        private async Task<ActionResult<MessageViewModel>> Messages(Guid circleId, Guid messageId, ContentType contentType, Func<Task<string>> getContent)
        {
            var userId = _userManager.ParseUserId(User);
            var membership = await GetMembershipIncludingCircle(userId, circleId);
            if (membership == null) return NotFound();

            var content = await getContent();
            Message message = new Message
            {
                Id = messageId,
                CircleId = circleId,
                ContentType = contentType,
                Content = content,
                SentAt = DateTimeOffset.UtcNow,
                SentBy = userId,
                SenderName = _userManager.GetUserName(User),
            };
            _db.Messages.Add(message);
            membership.Circle.NumberOfMessages++; // FIXME: da, possible concurrent DB update exception?
            membership.NumberOfReadMessages++; // FIXME: da, possible concurrent DB update exception?
            await _db.SaveChangesAsync();
            await _hub.Clients.All.MessagePosted(new MessagePostedEvent { CircleId = circleId, MessageId = message.Id }); // FIXME: da, do not send to *all* users
            return Ok(message);
        }

        private async Task<Membership> GetMembershipIncludingCircle(Guid userId, Guid circleId)
        {
            var user = await _db.Users
                .Include(u => u.Memberships)
                .ThenInclude(m => m.Circle)
                .SingleFirstOrDefaultAsync(u => u.Id == userId);
            return user.Memberships.SingleOrDefault(c => c.CircleId == circleId);
        }
    }
}
