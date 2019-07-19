using esme.Infrastructure;
using esme.Infrastructure.Data;
using esme.Infrastructure.Services;
using esme.Shared;
using esme.Shared.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Server.Api
{
    [ApiController]
    [Authorize]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly InvitationService _invitationService;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext db, InvitationService invitationService, ILogger<AuthorizationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _invitationService = invitationService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(Urls.PostLogin)]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(parameters.Email);
            if (user == null) return BadRequest("Invalid user or password");
            var checkPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
            if (!checkPasswordResult.Succeeded) return BadRequest("Invalid user or password");

            await _signInManager.SignInAsync(user, parameters.RememberMe);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(Urls.PostSignup)]
        public async Task<IActionResult> Signup(SignupParameters parameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var invitation = await _db.Invitations.SingleFirstOrDefaultAsync(i => i.To == parameters.Email);
            if (invitation == null)
            {
                _logger.LogWarning($"Signup attempt with invalid invitation e-mail: {parameters.Email}."); // FIXME: da, rather throw security event with request origin details (IP, port, ...)
                return NotFound("Invitation not found.");
            }

            await _invitationService.AcceptInvitation(invitation, parameters);
            await _db.SaveChangesAsync();

            return await Login(new LoginParameters
            {
                Email = parameters.Email,
                Password = parameters.Password
            });
        }

        [HttpPost]
        [Route(Urls.PostLogout)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [AllowAnonymous] // this is ok since the client will just get information about itself
        [HttpGet]
        [Route(Urls.GetMe)]
        public ActionResult<UserViewModel> Me()
        {
            return new UserViewModel
            {
                UserName = User.Identity.Name,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}