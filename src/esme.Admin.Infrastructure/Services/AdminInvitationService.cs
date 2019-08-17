using esme.Admin.Shared.Services;
using esme.Admin.Shared.ViewModels;
using esme.Infrastructure.Data;
using esme.Infrastructure.Services;
using esme.Shared;
using System;
using System.Net;
using System.Threading.Tasks;

namespace esme.Admin.Infrastructure.Services
{
    public class AdminInvitationService : IInvitationService
    {
        private readonly InvitationService _invitationService;
        private readonly ApplicationDbContext _db;

        public AdminInvitationService(InvitationService invitationService, ApplicationDbContext db)
        {
            _invitationService = invitationService;
            _db = db;
        }

        public async Task Invite(InvitationEditModel model, string baseUri)
        {
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                To = model.To,
                SentAt = DateTimeOffset.UtcNow,
            };
            await _invitationService.SendInvitation(invitation, confirmationCode => $"{baseUri}/join/{WebUtility.UrlEncode(invitation.To)}/{WebUtility.UrlEncode(confirmationCode)}");
            await _db.SaveChangesAsync();
        }
    }
}
