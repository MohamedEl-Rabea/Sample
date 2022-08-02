using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Interfaces
{
    [ScopedService]
    public interface IDomainEventsDispatcher
    {
        Task DispatchAsync(DbContext dbContext);
    }
}
