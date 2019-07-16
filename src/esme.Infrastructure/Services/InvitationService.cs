using System;
using System.Linq;
using System.Threading.Tasks;
using esme.Infrastructure.Data;
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

            //await _userManager.SetLockoutEnabledAsync(user, true); // FIXME: da, lockout user (see: https://stackoverflow.com/a/46052714/331281)

            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = getSignupUrl(code);
            await _mailing.Send(invitation.To, "Join the esme Community", $"Follow this link: {url} to join the community."); // FIXME: da, can we set the sender to invitor's e-mail?
        }

        public async Task AcceptInvitation(Invitation invitation, SignupParameters parameters)
        {
            var user = await _userManager.FindByEmailAsync(invitation.To);
            Assert(await _userManager.ConfirmEmailAsync(user, parameters.ConfirmationCode));
            Assert(await _userManager.ChangePasswordAsync(user, string.Empty, parameters.Password));
            user.UserName = parameters.UserName;
            invitation.AcceptedAt = DateTimeOffset.UtcNow;
        }

        private void Assert(IdentityResult result)
        {
            if (!result.Succeeded) throw new InvalidOperationException(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }
    }
}
