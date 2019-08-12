using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using System;

namespace esme.Admin.Shared.Services
{
    public interface IGridService<TViewModel>
    {
        ItemsDTO<TViewModel> GetRows(Action<IGridColumnCollection<TViewModel>> columns, QueryDictionary<StringValues> query);
    }
}
