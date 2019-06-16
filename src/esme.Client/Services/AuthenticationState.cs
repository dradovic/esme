using esme.Shared.Users;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public class AuthenticationState
    {
        private readonly IAuthorizationApi _authorizationApi;
        private readonly ClientHub _hub;

        public UserViewModel User { get; private set; }

        public AuthenticationState(IAuthorizationApi authorizationApi, ClientHub hub)
        {
            _authorizationApi = authorizationApi;
            _hub = hub;
        }

        public async Task<bool> IsLoggedIn()
        {
            var user = await _authorizationApi.TryGetUser();
            await SetUser(user);
            return User != null;
        }

        public async Task Login(LoginParameters loginParameters)
        {
            var user = await _authorizationApi.Login(loginParameters);
            await SetUser(user);
        }

        public async Task Signup(SignupParameters registerParameters)
        {
            var user = await _authorizationApi.Register(registerParameters);
            await SetUser(user);
        }

        public async Task Logout()
        {
            await _authorizationApi.Logout();
            await SetUser(null);
        }

        private async Task SetUser(UserViewModel user)
        {
            User = user;
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
