using esme.Infrastructure;
using esme.Infrastructure.Data;
using esme.Infrastructure.Services;
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
        private readonly InvitationService _invitationService;

        public InvitationsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            InvitationService invitationService)
        {
            _db = db;
            _userManager = userManager;
            _invitationService = invitationService;
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
                To = model.To, // FIXME: da, make sure To does not already have an invitation and invitation e-mail gets sent only once
                SentAt = DateTimeOffset.UtcNow,
            };
            user.Invitations.Add(invitation);
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; // see: https://stackoverflow.com/a/47051481/331281
            await _invitationService.SendInvitation(invitation, confirmationCode => $"{baseUrl}/{invitation.To}/{confirmationCode}");
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
