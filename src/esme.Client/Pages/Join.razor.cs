using esme.Client.Services;
using esme.Shared.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;
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

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Parameter]
        protected string EncodedEmail { get; set; }

        protected string Email => WebUtility.UrlDecode(EncodedEmail);

        [Parameter]
        protected string EncodedConfirmationCode { get; set; }

        private string ConfirmationCode => WebUtility.UrlDecode(EncodedConfirmationCode);

        protected override async Task OnAfterRenderAsync()
        {
            await JSRuntime.InvokeAsync<string>("startAnimation");
        }

        protected async Task OnSubmit()
        {
            SignupParameters.Email = Email;
            SignupParameters.ConfirmationCode = ConfirmationCode;
            Error = await AuthenticationStateProvider.Signup(SignupParameters);
            if (string.IsNullOrEmpty(Error))
            {
                UriHelper.NavigateTo("/home");
            }
        }
    }
}
