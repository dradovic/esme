using esme.Client.Services;
using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class LoginBase : ComponentBase
    {
        protected LoginParameters LoginParameters { get; private set; } = new LoginParameters();
        protected string Error { get; private set; }

        [Inject]
        private IdentityAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected async Task OnSubmit()
        {
            Error = await AuthenticationStateProvider.Login(LoginParameters);
            if (string.IsNullOrEmpty(Error))
            {
                UriHelper.NavigateTo("/home");
            }
        }
    }
}
