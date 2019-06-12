using esme.Admin.Shared.ViewModels;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using System;

namespace esme.Admin.Shared.Services
{
    public interface ICirclesGridService
    {
        ItemsDTO<CircleViewModel> GetCirclesGridRows(Action<IGridColumnCollection<CircleViewModel>> columns,
                QueryDictionary<StringValues> query);
    }
}
