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
            var invitation = await _invitationService.SendInvitation(Guid.NewGuid(), model.To, confirmationCode => $"{baseUri}/join/{WebUtility.UrlEncode(model.To)}/{WebUtility.UrlEncode(confirmationCode)}");
            _db.Invitations.Add(invitation);
            await _db.SaveChangesAsync();
        }
    }
}
