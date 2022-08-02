using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Shared.Interfaces;

namespace Moj.CMS.Shared.Infrastructure
{
    public class QueryBuilder<TContext> : IQueryBuilder where TContext : DbContext
    {
        private readonly TContext _context;

        public QueryBuilder(TContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }
    }
}