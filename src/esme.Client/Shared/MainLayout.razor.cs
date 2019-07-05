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
        private IStore Store { get; set; }

        [Inject]
        protected IState<ApplicationState> ApplicationState { get; set; }

        [Inject]
        private IdentityAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected override void OnInit()
        {
            ApplicationState.Subscribe(this);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (ApplicationState.Value.State == State.Entered)
            {
                // FIXME: da, can we instead use [Authorize] or redirect in the NotAuthorized fragment?
                if (!(await AuthenticationState).User.Identity.IsAuthenticated)
                {
                    UriHelper.NavigateTo("/login");
                }
            }
        }

        protected async Task Logout()
        {
            await AuthenticationStateProvider.Logout();
            UriHelper.NavigateTo("/login");
        }
    }
}
