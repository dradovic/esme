using esme.Shared.Users;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public class AuthenticationState
    {
        private readonly IAuthorizationApi _authorizationApi;

        public UserViewModel User { get; private set; }

        public AuthenticationState(IAuthorizationApi authorizationApi)
        {
            _authorizationApi = authorizationApi;
        }

        public async Task<bool> IsLoggedIn()
        {
            User = await _authorizationApi.TryFetchUser();
            return User != null;
        }

        public async Task Login(LoginParameters loginParameters)
        {
            User = await _authorizationApi.Login(loginParameters);
        }

        public async Task Signup(SignupParameters registerParameters)
        {
            User = await _authorizationApi.Register(registerParameters);
        }

        public async Task Logout()
        {
            await _authorizationApi.Logout();
            User = null;
        }
    }
}
