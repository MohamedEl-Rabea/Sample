using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Infrastructure.Seed
{
    [ScopedService]
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
