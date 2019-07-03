using System;
using System.Threading.Tasks;

namespace esme.Admin.Shared.Services
{
    public interface IUsersService
    {
        Task<bool> IsAmbassador(Guid userId);
        Task GrantAmbassador(Guid userId);
        Task DenyAmbassador(Guid userId);
    }
}
