using esme.Admin.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace esme.Admin.App.Pages
{
    public class DataBase : ComponentBase
    {
        [Inject]
        private IDataService SampleDataService { get; set; }

        protected bool IsWorking { get; private set; }

        protected async Task DeleteAll()
        {
            IsWorking = true;
            StateHasChanged();
            await Task.Delay(10);
            await SampleDataService.DeleteAllAsync();
            IsWorking = false;
        }

        protected async Task ResetAllWithSampleData()
        {
            IsWorking = true;
            StateHasChanged();
            await Task.Delay(10);
            await SampleDataService.ResetAllWithSampleDataAsync();
            IsWorking = false;
        }
    }
}
