using esme.Admin.Shared.ViewModels;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using System;

namespace esme.Admin.Shared.Services
{
    public interface IUsersGridService
    {
        ItemsDTO<UserViewModel> GetUsersGridRows(Action<IGridColumnCollection<UserViewModel>> columns,
                QueryDictionary<StringValues> query);
    }
}
