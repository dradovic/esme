using esme.Shared.Users;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public class AuthenticationState
    {
        private readonly IAuthorizationApi _authorizationApi;
        private readonly IJSRuntime _jsRuntime;

        public UserViewModel User { get; private set; }

        public AuthenticationState(IAuthorizationApi authorizationApi, IJSRuntime jsRuntime)
        {
            _authorizationApi = authorizationApi;
            _jsRuntime = jsRuntime;
        }

        public Task<bool> IsLoggedIn()
        {
            return _jsRuntime.InvokeAsync<bool>("Authorization_LoginCookieExists"); // FIXME: da, find a better sln to this (maybe: https://stackoverflow.com/questions/41298621/js-get-cookie-asp-net-core-identity)
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
