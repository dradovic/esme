using esme.Admin.Shared.Services;
using esme.Admin.Shared.ViewModels;
using esme.Shared;
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
        protected InvitationEditModel NewInvitation { get; private set; } = new InvitationEditModel();

        protected CGrid<InvitationViewModel> Grid { get; private set; }
        protected Task Task { get; private set; }

        [Inject]
        private IInvitationService InvitationService { get; set; }

        [Inject]
        private IGridService<InvitationViewModel> GridService { get; set; }

        [Inject]
        private IUriHelper UriHelper { get; set; }

        protected override async Task OnInitAsync()
        {
            Action<IGridColumnCollection<InvitationViewModel>> columns = c =>
            {
                c.Add(o => o.Id).Titled("Id").Sortable(true);
                c.Add(o => o.To).Titled("To").Sortable(true);
                c.Add(o => o.SentBy).Titled("By").Sortable(true);
                c.Add(o => o.SentAt).Titled("Sent").Sortable(true);
                c.Add(o => o.AcceptedAt).Titled("Accepted").Sortable(true);
                c.Add().Titled("Expired").RenderValueAs(i => i.Expired.ToString()).Sortable(true);
            };

            var query = new QueryDictionary<StringValues>();
            query.Add("grid-page", "1");

            var client = new GridClient<InvitationViewModel>(q => GridService.GetRows(columns, q), query, false, "grid", columns);
            Grid = client.Grid;

            // Set new items to grid
            Task = client.UpdateGrid();
            await Task;
        }

        protected void OnSubmit()
        {
            InvitationService.Invite(NewInvitation, UriHelper.GetBaseUri());
            NewInvitation = new InvitationEditModel(); // start over
        }
    }
}
