using System;
using System.Linq;
using System.Threading.Tasks;
using esme.Infrastructure.Data;
using esme.Shared;
using esme.Shared.Users;
using Microsoft.AspNetCore.Identity;

namespace esme.Infrastructure.Services
{
    public class InvitationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MailingService _mailing;

        public InvitationService(UserManager<ApplicationUser> userManager, MailingService mailing)
        {
            _userManager = userManager;
            _mailing = mailing;
        }

        public async Task SendInvitation(Invitation invitation, Func<string, string> getSignupUrl)
        {
            var user = new ApplicationUser
            {
                UserName = invitation.Id.ToString(),
                Email = invitation.To,
            };
            Assert(await _userManager.CreateAsync(user));
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = getSignupUrl(code);
            await _mailing.Send(invitation.To, "Join the esme Community", $"Follow this link: {url} to join the community. This invitation expires in {Constants.JoinInvitationExpirationDays} days."); // FIXME: da, can we set the sender to invitor's e-mail?
        }

        public async Task<string> AcceptInvitation(Invitation invitation, SignupParameters parameters)
        {
            var user = await _userManager.FindByEmailAsync(invitation.To);
            var result = await _userManager.ConfirmEmailAsync(user, parameters.ConfirmationCode);
            if (!result.Succeeded) // the token might have expired
            {
                return result.ToErrorString();
            }
            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            Assert(await _userManager.ResetPasswordAsync(user, resetPasswordToken, parameters.Password));
            user.UserName = parameters.UserName;
            invitation.AcceptedAt = DateTimeOffset.UtcNow;
            return null; // success
        }

        private void Assert(IdentityResult result)
        {
            if (!result.Succeeded) throw new InvalidOperationException(result.ToErrorString());
        }
    }
}
