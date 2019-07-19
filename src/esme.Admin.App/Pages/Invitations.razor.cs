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
    public abstract class InvitationsBase : ComponentBase
    {
        protected CGrid<InvitationViewModel> Grid { get; private set; }
        protected Task Task { get; private set; }

        [Inject]
        private IGridService<InvitationViewModel> GridService { get; set; }

        protected override async Task OnInitAsync()
        {
            Action<IGridColumnCollection<InvitationViewModel>> columns = c =>
            {
                c.Add(o => o.Id).Titled("Id").Sortable(true);
                c.Add(o => o.To).Titled("To").Sortable(true);
                c.Add(o => o.SentBy).Sortable(true);
                c.Add(o => o.SentAt).Sortable(true);
                c.Add(o => o.AcceptedAt).Sortable(true);
            };

            var query = new QueryDictionary<StringValues>();
            query.Add("grid-page", "1");

            var client = new GridClient<InvitationViewModel>(q => GridService.GetRows(columns, q), query, false, "grid", columns);
            Grid = client.Grid;

            // Set new items to grid
            Task = client.UpdateGrid();
            await Task;
        }
    }
}
