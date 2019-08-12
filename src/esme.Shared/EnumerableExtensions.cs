using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace esme.Shared
{
    public static class EnumerableExtensions
    {
        public static TSource SingleFirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Debug.Assert(source.Count(predicate) <= 1);
            return source.FirstOrDefault(predicate);
        }

        public static TSource SingleFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Debug.Assert(source.Count(predicate) <= 1);
            return source.First(predicate);
        }
    }
}
