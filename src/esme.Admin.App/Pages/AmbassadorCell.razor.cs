using System.Threading.Tasks;
using esme.Admin.Shared.Services;
using esme.Admin.Shared.ViewModels;
using Microsoft.AspNetCore.Components;

namespace esme.Admin.App.Pages
{
    public abstract class AmbassadorCellBase : ComponentBase
    {
        [Parameter]
        public UserViewModel Item { get; set; }

        protected bool? IsAmbassador { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500); // seems like adding/removing a user to/from a role immediately triggers a refresh of the grid
            IsAmbassador = await UsersService.IsAmbassador(Item.Id);
        }

        [Inject]
        private IUsersService UsersService { get; set; }

        protected async Task GrantAmbassador()
        {
            await UsersService.GrantAmbassador(Item.Id);
        }

        protected async Task DenyAmbassador()
        {
            await UsersService.DenyAmbassador(Item.Id);
        }
    }
}
