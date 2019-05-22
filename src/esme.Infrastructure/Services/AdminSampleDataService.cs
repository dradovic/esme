using System.Threading.Tasks;
using esme.Admin.Shared.Services;

namespace esme.Infrastructure.Services
{
    public class AdminSampleDataService : ISampleDataService
    {
        public Task ResetAllWithSampleData()
        {
            return Task.CompletedTask;
        }
    }
}
