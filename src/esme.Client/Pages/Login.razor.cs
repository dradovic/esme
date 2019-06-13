using esme.Client.Services;
using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class LoginBase : ComponentBase
    {
        protected LoginParameters LoginParameters { get; private set; } = new LoginParameters();
        protected string Error { get; private set; }

        [Inject]
        private AuthenticationState State { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected async Task OnSubmit()
        {
            Error = null;
            try
            {
                await State.Login(LoginParameters);
                UriHelper.NavigateTo("/home");
            }
            catch (Exception x)
            {
                Error = x.Message;
            }
        }
    }
}
