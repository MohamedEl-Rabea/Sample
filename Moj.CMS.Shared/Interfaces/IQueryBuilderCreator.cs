using Microsoft.EntityFrameworkCore;

namespace Moj.CMS.Shared.Interfaces
{
    public interface IQueryBuilderCreator<TContext> where TContext : DbContext
    {
        IQueryBuilder Create();
    }
}
