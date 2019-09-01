using System;
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
        private readonly IMailingService _mailing;

        public InvitationService(UserManager<ApplicationUser> userManager, IMailingService mailing)
        {
            _userManager = userManager;
            _mailing = mailing;
        }

        public async Task<Invitation> SendInvitation(Guid invitationId, string to, Func<string, string> getSignupUrl)
        {
            // Note: creating a user with the invitation e-mail first makes sure that the user ever only receives
            // one invitation as the Identity will refuse to create two users with the same e-mail.
            var user = new ApplicationUser
            {
                UserName = invitationId.ToString(),
                Email = to,
            };
            Assert(await _userManager.CreateAsync(user));

            // send invitiation e-mail
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = getSignupUrl(code);
            string error = await _mailing.Send(to, "Join the esme Community", $"Follow this link: {url} to join the community. This invitation expires in {Constants.JoinInvitationExpirationDays} days."); // FIXME: da, can we set the sender to invitor's e-mail?
            return new Invitation
            {
                Id = invitationId,
                To = to,
                SentAt = DateTimeOffset.UtcNow,
                Error = error,
            };
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
            user.Memberships.Add(new Membership
            {
                CircleId = Circle.OpenCircleId
            });
            invitation.AcceptedAt = DateTimeOffset.UtcNow;
            return null; // success
        }

        private void Assert(IdentityResult result)
        {
            if (!result.Succeeded) throw new InvalidOperationException(result.ToErrorString());
        }
    }
}
