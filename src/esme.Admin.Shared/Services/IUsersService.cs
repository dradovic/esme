using System;
using System.Threading.Tasks;

namespace esme.Admin.Shared.Services
{
    public interface IUsersService
    {
        Task GrantAmbassador(Guid userId);
    }
}
