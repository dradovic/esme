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
    public class CirclesGridService : ICirclesGridService
    {
        private readonly ApplicationDbContext _db;

        public CirclesGridService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ItemsDTO<CircleViewModel> GetCirclesGridRows(Action<IGridColumnCollection<CircleViewModel>> columns,
                QueryDictionary<StringValues> query)
        {
            var circles = _db.Circles.Select(c => new CircleViewModel
            {
                Id = c.Id,
                Name = c.Name,
                NumberOfUsers = c.Memberships.Count,
                NumberOfMessages = c.NumberOfMessages,
            });
            var server = new GridServer<CircleViewModel>(circles, new QueryCollection(query),
                true, "circlesGrid", columns, 50)
                .Sortable()
                .Filterable();

            // return items to displays
            return server.ItemsToDisplay;
        }
    }
}
