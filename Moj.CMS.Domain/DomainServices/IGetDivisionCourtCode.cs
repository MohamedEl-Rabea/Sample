using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices
{
    [TransientService]
    public interface IGetDivisionCourtCode
    {
        Task<string> GetDivisionCourtCodeAsync(string divisionCode);
    }
}
