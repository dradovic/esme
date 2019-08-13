using esme.Admin.Shared.Services;
using esme.Infrastructure.Data;
using GridMvc.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace esme.Admin.Infrastructure.Services
{
    public abstract class GridService<TEntity, TViewModel> : IGridService<TViewModel>
        where TEntity: class
        where TViewModel: class
    {
        private readonly ApplicationDbContext _db;

        public GridService(ApplicationDbContext db)
        {
            _db = db;
        }

        protected DbSet<TEntity> Entities => _db.Set<TEntity>();

        public ItemsDTO<TViewModel> GetRows(Action<IGridColumnCollection<TViewModel>> columns,
                QueryDictionary<StringValues> query)
        {
            var models = FetchViewModels();
            var server = new GridServer<TViewModel>(models, new QueryCollection(query),
                true, "circlesGrid", columns, 50)
                .Sortable()
                .Filterable();

            // return items to displays
            return server.ItemsToDisplay;
        }

        protected abstract IQueryable<TViewModel> FetchViewModels();

        private class QueryCollection : IQueryCollection
        {
            private QueryDictionary<StringValues> _query;

            public QueryCollection(QueryDictionary<StringValues> query)
            {
                _query = query;
            }

            public StringValues this[string key] => _query.ContainsKey(key) ? _query[key] : new StringValues();

            public int Count => _query.Count;

            public ICollection<string> Keys => _query.Keys;

            public bool ContainsKey(string key)
            {
                return _query.ContainsKey(key);
            }

            public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator()
            {
                return _query.GetEnumerator();
            }

            public bool TryGetValue(string key, out StringValues value)
            {
                return _query.TryGetValue(key, out value);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _query.GetEnumerator();
            }
        }
    }
}
