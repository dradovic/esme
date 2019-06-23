using esme.Admin.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace esme.Admin.App.Pages
{
    public class DataBase : ComponentBase
    {
        [Inject]
        private ISampleDataService SampleDataService { get; set; }

        protected bool IsDisabled { get; private set; }

        protected async Task ResetAllWithSampleData()
        {
            IsDisabled = true;
            StateHasChanged();
            await Task.Delay(10);
            await SampleDataService.ResetAllWithSampleData();
            IsDisabled = false;
        }
    }
}
