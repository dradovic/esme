using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
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
        Task<UserViewModel> TryFetchUser();
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
            var stringContent = new StringContent(Json.Serialize(loginParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Authorize/Login", stringContent);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();

            return Json.Deserialize<UserViewModel>(await result.Content.ReadAsStringAsync());
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/Authorize/Logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task<UserViewModel> Register(SignupParameters signupParameters)
        {
            var stringContent = new StringContent(Json.Serialize(signupParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/Authorize/Register", stringContent);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();

            return Json.Deserialize<UserViewModel>(await result.Content.ReadAsStringAsync());
        }

        public async Task<UserViewModel> TryFetchUser()
        {
            var result = await _httpClient.GetJsonAsync<UserViewModel>("api/Authorize/UserInfo");
            return result;
        }
    }
}
