using esme.Admin.Shared.Services;
using esme.Admin.Shared.ViewModels;
using Microsoft.AspNetCore.Components;

namespace esme.Admin.App.Pages
{
    public abstract class GrantAmbassadorButtonBase : ComponentBase
    {
        [Parameter]
        protected UserViewModel Item { get; set; }

        [Inject]
        private IUsersService UsersService { get; set; }

        protected void GrantAmbassador()
        {
            UsersService.GrantAmbassador(Item.Id);
        }
    }
}
