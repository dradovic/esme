using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace esme.Admin.Infrastructure.Services
{
    public static class DbSetExtensions
    {
        public static void RemoveAll<T>(this DbSet<T> set)
            where T: class
        {
            set.ToList().ForEach(i => set.Remove(i));
        }
    }
}
