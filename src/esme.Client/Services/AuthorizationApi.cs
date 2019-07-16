using esme.Shared;
using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public interface IAuthorizationApi
    {
        Task Login(LoginParameters loginParameters);
        Task Signup(SignupParameters signupParameters);
        Task Logout();
        Task<UserViewModel> FetchUser();
    }

    public class AuthorizationApi : IAuthorizationApi
    {
        private readonly HttpClient _httpClient;

        public AuthorizationApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Login(LoginParameters loginParameters)
        {
            // FIXME: da, receive error message from server and display that as an error (ex. "wrong password")
            await _httpClient.PostJsonAsync(Urls.PostLogin, loginParameters);
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync(Urls.PostLogout, null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Signup(SignupParameters signupParameters)
        {
            await _httpClient.PostJsonAsync(Urls.PostSignup, signupParameters);
        }

        public async Task<UserViewModel> FetchUser()
        {
            return await _httpClient.GetJsonAsync<UserViewModel>(Urls.GetMe);
        }
    }
}
