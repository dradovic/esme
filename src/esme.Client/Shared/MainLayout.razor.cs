using esme.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using System.Threading.Tasks;

namespace esme.Client.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        protected bool IsLoggedIn { get; set; }

        [Inject]
        private IdentityAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected async Task Logout()
        {
            await AuthenticationStateProvider.Logout();
            UriHelper.NavigateTo("/");
        }
    }
}
