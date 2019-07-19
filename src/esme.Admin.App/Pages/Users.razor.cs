using esme.Admin.Shared.Services;
using esme.Admin.Shared.ViewModels;
using GridBlazor;
using GridShared;
using GridShared.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace esme.Admin.App.Pages
{
    public abstract class UsersBase : ComponentBase
    {
        protected CGrid<UserViewModel> Grid { get; private set; }
        protected Task Task { get; private set; }

        [Inject]
        private IGridService<UserViewModel> UsersGridService { get; set; }

        protected override async Task OnInitAsync()
        {
            Action<IGridColumnCollection<UserViewModel>> columns = c =>
            {
                c.Add(o => o.Id).Titled("Id").Sortable(true);
                c.Add(o => o.UserName).Titled("Name").Sortable(true);
                c.Add(o => o.Email).Titled("Email").Sortable(true);
                c.Add().Titled("Ambassador").Encoded(false).Sanitized(false).SetWidth(30).RenderComponentAs(typeof(AmbassadorCell));
            };

            var query = new QueryDictionary<StringValues>();
            query.Add("grid-page", "1");

            var client = new GridClient<UserViewModel>(q => UsersGridService.GetRows(columns, q), query, false, "usersGrid", columns);
            Grid = client.Grid;

            // Set new items to grid
            Task = client.UpdateGrid();
            await Task;
        }
    }
}
