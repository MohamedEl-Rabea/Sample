using Microsoft.EntityFrameworkCore;
using Moj.CMS.Shared.Interfaces;

namespace Moj.CMS.Shared.Infrastructure
{
    public class QueryBuilderCreator<TContext> : IQueryBuilderCreator<TContext> where TContext: DbContext
    {
        private readonly TContext _appDbContext;
        public QueryBuilderCreator(TContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryBuilder Create()
        {
            _appDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return new QueryBuilder<TContext>(_appDbContext);
        }
    }
}
