using esme.Client.Services;
using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace esme.Client.Pages
{
    public abstract class JoinBase : ComponentBase
    {
        protected SignupParameters SignupParameters { get; private set; } = new SignupParameters();
        protected string Error { get; private set; }

        [Inject]
        private IdentityAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string Email { get; set; }

        [Parameter]
        protected string ConfirmationCode { get; set; }

        protected async Task OnSubmit()
        {
            Error = null;
            try
            {
                SignupParameters.Email = Email;
                SignupParameters.ConfirmationCode = ConfirmationCode;
                await AuthenticationStateProvider.Signup(SignupParameters);
                UriHelper.NavigateTo("/home");
            }
            catch (Exception x)
            {
                Error = x.Message;
            }
        }
    }
}
