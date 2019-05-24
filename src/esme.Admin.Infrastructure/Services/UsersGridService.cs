using esme.Admin.Shared.Services;
using esme.Admin.Shared.ViewModels;
using esme.Infrastructure.Data;
using GridMvc.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace esme.Admin.Infrastructure.Services
{
    public class UsersGridService : IUsersGridService
    {
        private readonly ApplicationDbContext _db;

        public UsersGridService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ItemsDTO<UserViewModel> GetUsersGridRows(Action<IGridColumnCollection<UserViewModel>> columns,
                QueryDictionary<StringValues> query)
        {
            var users = _db.Users.Select(u => new UserViewModel
            {
                UserName = u.UserName,
                Email = u.Email,
            });
            var server = new GridServer<UserViewModel>(users, new QueryCollection(query),
                true, "ordersGrid", columns, 10)
                .Sortable()
                .Filterable();

            // return items to displays
            return server.ItemsToDisplay;
        }
    }
}
