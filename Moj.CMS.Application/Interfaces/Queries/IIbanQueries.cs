using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.Interfaces.Queries
{
    [ScopedService]
    public interface IIbanQueries
    {
        Task<int> GetIbanIdAsyc(string ibanNumber);
    }
}