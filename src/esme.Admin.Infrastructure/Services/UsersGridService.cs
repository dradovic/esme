using esme.Admin.Shared.ViewModels;
using esme.Infrastructure.Data;
using System.Linq;

namespace esme.Admin.Infrastructure.Services
{
    public class UsersGridService : GridService<ApplicationUser, UserViewModel>
    {
        public UsersGridService(ApplicationDbContext db) : base(db)
        {
        }

        protected override IQueryable<UserViewModel> FetchViewModels()
        {
            return Entities.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
            });
        }
    }
}
