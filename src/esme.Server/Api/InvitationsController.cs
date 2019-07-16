using esme.Infrastructure;
using esme.Infrastructure.Data;
using esme.Shared;
using esme.Shared.Invitations;
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
    [ApiController] // FIXME: da, decorate on assembly level
    [Authorize]
    public class InvitationsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public InvitationsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        [Route(Urls.GetInvitations)]
        public async Task<ActionResult<IEnumerable<InvitationViewModel>>> Invitations()
        {
            var user = await GetUserIncludingInvitations();
            return Ok(user.Invitations.Select(ToViewModel));
        }

        [HttpPost]
        [Route(Urls.PostInvitation)]
        public async Task<ActionResult<InvitationViewModel>> Invitations([FromBody] InvitationEditModel model)
        {
            if (!ModelState.IsValid) return BadRequest(); // FIXME: da, should be automatic for [ApiController] (see https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.0)

            var user = await GetUserIncludingInvitations();
            var invitation = new Invitation
            {
                Id = model.Id,
                To = model.To,
                SentAt = DateTimeOffset.UtcNow,
            };
            user.Invitations.Add(invitation);
            await _db.SaveChangesAsync();
            return Ok(ToViewModel(invitation));
        }

        private async Task<ApplicationUser> GetUserIncludingInvitations()
        {
            Guid userId = _userManager.ParseUserId(User);
            return await _db.Users
                .Include(u => u.Invitations)
                .SingleFirstOrDefaultAsync(u => u.Id == userId);
        }

        private static InvitationViewModel ToViewModel(Invitation invitation)
        {
            return new InvitationViewModel
            {
                Id = invitation.Id,
                To = invitation.To,
                SentAt = invitation.SentAt,
                IsAccepted = invitation.AcceptedAt.HasValue,
            };
        }
    }
}
