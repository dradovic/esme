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
    public abstract class CirclesBase : ComponentBase
    {
        protected CGrid<CircleViewModel> Grid { get; private set; }
        protected Task Task { get; private set; }

        [Inject]
        private IGridService<CircleViewModel> CirclesGridService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Action<IGridColumnCollection<CircleViewModel>> columns = c =>
            {
                c.Add(o => o.Id).Titled("Id").Sortable(true);
                c.Add(o => o.Name).Titled("Name").Sortable(true);
                c.Add(o => o.NumberOfUsers).Sortable(true);
                c.Add(o => o.NumberOfMessages).Sortable(true);
            };

            var query = new QueryDictionary<StringValues>();
            query.Add("grid-page", "1");

            var client = new GridClient<CircleViewModel>(q => CirclesGridService.GetRows(columns, q), query, false, "ordersGrid", columns);
            Grid = client.Grid;

            // Set new items to grid
            Task = client.UpdateGrid();
            await Task;
        }
    }
}
