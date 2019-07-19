using esme.Shared;
using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public interface IAuthorizationApi
    {
        Task<string> Login(LoginParameters loginParameters);
        Task<string> Signup(SignupParameters signupParameters);
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

        public async Task<string> Login(LoginParameters loginParameters)
        {
            return await _httpClient.PostAsync(Urls.PostLogin, loginParameters);
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync(Urls.PostLogout, null);
            result.EnsureSuccessStatusCode();
        }

        public async Task<string> Signup(SignupParameters signupParameters)
        {
            return await _httpClient.PostAsync(Urls.PostSignup, signupParameters);
        }

        public async Task<UserViewModel> FetchUser()
        {
            return await _httpClient.GetJsonAsync<UserViewModel>(Urls.GetMe);
        }
    }
}
