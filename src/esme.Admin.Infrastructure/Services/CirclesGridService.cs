using esme.Admin.Shared.ViewModels;
using esme.Infrastructure.Data;
using System.Linq;

namespace esme.Admin.Infrastructure.Services
{
    public class CirclesGridService : GridService<Circle, CircleViewModel>
    {
        public CirclesGridService(ApplicationDbContext db) : base(db)
        {
        }

        protected override IQueryable<CircleViewModel> FetchViewModels()
        {
            return Entities.Select(c => new CircleViewModel
            {
                Id = c.Id,
                Name = c.Name,
                NumberOfUsers = c.Memberships.Count,
                NumberOfMessages = c.NumberOfMessages,
            });
        }
    }
}
