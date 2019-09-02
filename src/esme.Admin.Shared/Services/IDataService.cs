using System.Threading.Tasks;

namespace esme.Admin.Shared.Services
{
    public interface IDataService
    {
        Task DeleteAllAsync();
        Task ResetAllWithSampleDataAsync();

        void MigrateDatabase();
    }
}
