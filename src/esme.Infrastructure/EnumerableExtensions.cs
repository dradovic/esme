using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace esme.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static Task<TSource> SingleFirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            Debug.Assert(source.Count(predicate) <= 1);
            return source.FirstOrDefaultAsync(predicate);
        }
    }
}
