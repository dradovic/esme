using System.Threading.Tasks;

namespace esme.Admin.Shared.Services
{
    public interface IDataService
    {
        Task DeleteAll();
        Task ResetAllWithSampleData();
        Task Migrate();
    }
}
