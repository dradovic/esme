using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace esme.Client.Services
{
    public interface IAuthorizationApi
    {
        Task<UserViewModel> Login(LoginParameters loginParameters);
        Task<UserViewModel> Register(SignupParameters signupParameters);
        Task Logout();
        Task<UserViewModel> TryGetUser();
    }

    public class AuthorizationApi : IAuthorizationApi
    {
        private readonly HttpClient _httpClient;

        public AuthorizationApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserViewModel> Login(LoginParameters loginParameters)
        {
            return await _httpClient.PostJsonAsync<UserViewModel>("api/authorization/login", loginParameters);
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/authorization/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task<UserViewModel> Register(SignupParameters signupParameters)
        {
            return await _httpClient.PostJsonAsync<UserViewModel>("api/authorization/register", signupParameters);
        }

        public async Task<UserViewModel> TryGetUser()
        {
            try
            {
                var result = await _httpClient.GetJsonAsync<UserViewModel>("api/authorization/me");
                return result;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
