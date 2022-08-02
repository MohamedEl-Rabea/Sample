using Moj.CMS.Domain.Shared.Entities;

namespace Moj.CMS.Domain.Shared.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {

    }
}

