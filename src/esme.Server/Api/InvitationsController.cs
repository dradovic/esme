using esme.Infrastructure;
using esme.Infrastructure.Data;
using esme.Infrastructure.Services;
using esme.Shared;
using esme.Shared.Invitations;
using esme.Shared.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace esme.Server.Api
{
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Ambassador)]
    public class InvitationsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InvitationService _invitationService;
        private readonly IWebHostEnvironment _environment;

        public InvitationsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            InvitationService invitationService, IWebHostEnvironment environment)
        {
            _db = db;
            _userManager = userManager;
            _invitationService = invitationService;
            _environment = environment;
        }

        [HttpGet]
        [Route(Urls.GetInvitations)]
        public async Task<ActionResult<IEnumerable<InvitationViewModel>>> Invitations()
        {
            var user = await GetUserIncludingInvitations();
            return Ok(user.Invitations.Select(ToViewModel).OrderBy(i => i.IsAccepted).ThenBy(i => i.To));
        }

        [HttpPost]
        [Route(Urls.PostInvitation)]
        public async Task<ActionResult<InvitationViewModel>> Invitations([FromBody] InvitationEditModel model)
        {
            if (await _db.Users.AnyAsync(u => u.Email == model.To) || 
                await _db.Invitations.AnyAsync(i => i.To == model.To))
            {
                return BadRequest("This user has already been invited.");
            }

            var user = await GetUserIncludingInvitations();
            var invitation = new Invitation
            {
                Id = model.Id,
                To = model.To,
                SentAt = DateTimeOffset.UtcNow,
            };
            user.Invitations.Add(invitation);
            string baseUrl = _environment.IsProduction() ? Urls.AppUrl : $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; // see: https://stackoverflow.com/a/47051481/331281
            await _invitationService.SendInvitation(invitation, confirmationCode => $"{baseUrl}/join/{WebUtility.UrlEncode(invitation.To)}/{WebUtility.UrlEncode(confirmationCode)}");
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
