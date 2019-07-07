using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public interface IAuthorizationApi
    {
        Task Login(LoginParameters loginParameters);
        Task Register(SignupParameters signupParameters);
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
            await _httpClient.PostJsonAsync("api/authorization/login", loginParameters);
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/authorization/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(SignupParameters signupParameters)
        {
            await _httpClient.PostJsonAsync("api/authorization/register", signupParameters);
        }

        public async Task<UserViewModel> FetchUser()
        {
            return await _httpClient.GetJsonAsync<UserViewModel>("api/authorization/me");
        }
    }
}
