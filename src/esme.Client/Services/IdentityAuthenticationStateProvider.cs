using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthorizationApi _authorizationApi;
        private readonly ClientHub _hub;

        public UserViewModel User { get; private set; }

        public IdentityAuthenticationStateProvider(IAuthorizationApi authorizationApi, ClientHub hub)
        {
            _authorizationApi = authorizationApi;
            _hub = hub;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (User == null)
            {
                User = await _authorizationApi.FetchUser();
            }
            if (User.IsAuthenticated)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, User.UserName) }
                    .Concat(User.Claims.Select(c => new Claim(c.Key, c.Value)));
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Login(LoginParameters loginParameters)
        {
            await _authorizationApi.Login(loginParameters);
            await SetUser(null);
        }

        public async Task Signup(SignupParameters registerParameters)
        {
            await _authorizationApi.Register(registerParameters);
            await SetUser(null);
        }

        public async Task Logout()
        {
            await _authorizationApi.Logout();
            await SetUser(null);
        }

        private async Task SetUser(UserViewModel user)
        {
            User = user;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            if (User != null)
            {
                await _hub.SetupConnection();
            }
            else
            {
                // FIXME: da, disconnect from SignalR
            }
        }
    }
}
