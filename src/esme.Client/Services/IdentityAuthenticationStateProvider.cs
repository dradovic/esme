using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthorizationApi _authorizationApi;
        private readonly ClientHub _hub;
        private readonly ILogger<IdentityAuthenticationStateProvider> _logger;

        public UserViewModel User { get; private set; }

        public IdentityAuthenticationStateProvider(IAuthorizationApi authorizationApi, ClientHub hub, ILogger<IdentityAuthenticationStateProvider> logger)
        {
            _authorizationApi = authorizationApi;
            _hub = hub;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (User == null)
            {
                User = await _authorizationApi.FetchUser();
                if (User.IsAuthenticated)
                {
                    _logger.LogDebug($"User logged in: '{User.UserName}'.");
                    await _hub.Connect();
                }
                else
                {
                    _logger.LogDebug($"User logged out.");
                    await _hub.Disconnect();
                }
            }
            if (User.IsAuthenticated)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, User.UserName) }
                    .Concat(User.Claims.Select(c => new Claim(c.Key, c.Value)));
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task<string> Login(LoginParameters loginParameters)
        {
            var error = await _authorizationApi.Login(loginParameters);
            SignalStateChange();
            return error;
        }

        public async Task<string> Signup(SignupParameters registerParameters)
        {
            var error = await _authorizationApi.Signup(registerParameters);
            SignalStateChange();
            return error;
        }

        public async Task Logout()
        {
            await _authorizationApi.Logout();
            SignalStateChange();
        }

        private void SignalStateChange()
        {
            User = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
