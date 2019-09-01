using System.Threading.Tasks;

namespace esme.Admin.Shared.Services
{
    public interface ISampleDataService
    {
        Task DeleteAll();
        Task ResetAllWithSampleData();
    }
}
