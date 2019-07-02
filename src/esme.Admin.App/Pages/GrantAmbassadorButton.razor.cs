using esme.Admin.Shared.Services;
using esme.Admin.Shared.ViewModels;
using GridShared.Columns;
using Microsoft.AspNetCore.Components;

namespace esme.Admin.App.Pages
{
    public abstract class GrantAmbassadorButtonBase : ComponentBase, ICustomGridComponent<UserViewModel>
    {
        [Parameter]
        public UserViewModel Item { get; private set; }

        [Inject]
        private IUsersService UsersService { get; set; }

        protected void GrantAmbassador()
        {
            UsersService.GrantAmbassador(Item.Id);
        }
    }
}
