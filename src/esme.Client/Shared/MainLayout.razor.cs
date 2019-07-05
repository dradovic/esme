using Blazor.Fluxor;
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
        protected Services.AuthenticationState AuthenticationState { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected override async Task OnInitAsync()
        {
            ApplicationState.Subscribe(this);
            if (ApplicationState.Value.State == State.Entered)
            {
                IsLoggedIn = await AuthenticationState.IsLoggedIn();
                if (!IsLoggedIn)
                {
                    UriHelper.NavigateTo("/login");
                }
            }
        }

        //protected override async Task OnParametersSetAsync()
        //{
        //}

        protected async Task Logout()
        {
            await AuthenticationState.Logout();
            UriHelper.NavigateTo("/login");
        }
    }
}
