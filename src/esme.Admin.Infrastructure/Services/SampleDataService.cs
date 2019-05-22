using esme.Admin.Shared.Services;
using System.Threading.Tasks;

namespace esme.Admin.Infrastructure.Services
{
    public class SampleDataService : ISampleDataService
    {
        public Task ResetAllWithSampleData()
        {
            return Task.CompletedTask;
        }
    }
}
