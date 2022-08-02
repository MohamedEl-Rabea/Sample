using System;
using System.Linq;

namespace Moj.CMS.Shared.Interfaces
{
    public interface IQueryBuilder : IDisposable
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    }
}