using Blazor.Fluxor;
using esme.Client.Services;
using esme.Client.Store.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using System.Threading.Tasks;

namespace esme.Client.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        protected bool IsLoggedIn { get; set; }

        [Inject]
        protected IState<ApplicationState> ApplicationState { get; set; }

        [Inject]
        private IdentityAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected override void OnInit()
        {
            ApplicationState.Subscribe(this);
        }

        protected async Task Logout()
        {
            await AuthenticationStateProvider.Logout();
            UriHelper.NavigateTo("/login");
        }
    }
}
