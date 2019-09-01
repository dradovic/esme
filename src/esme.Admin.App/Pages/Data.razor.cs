using esme.Admin.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace esme.Admin.App.Pages
{
    public class DataBase : ComponentBase
    {
        [Inject]
        private ISampleDataService SampleDataService { get; set; }

        protected bool IsDeleteAllDisabled { get; private set; }

        protected bool IsResetAllWithSampleDataDisabled { get; private set; }

        protected async Task DeleteAll()
        {
            IsDeleteAllDisabled = true;
            StateHasChanged();
            await Task.Delay(10);
            await SampleDataService.DeleteAll();
            IsDeleteAllDisabled = false;
        }

        protected async Task ResetAllWithSampleData()
        {
            IsResetAllWithSampleDataDisabled = true;
            StateHasChanged();
            await Task.Delay(10);
            await SampleDataService.ResetAllWithSampleData();
            IsResetAllWithSampleDataDisabled = false;
        }
    }
}
