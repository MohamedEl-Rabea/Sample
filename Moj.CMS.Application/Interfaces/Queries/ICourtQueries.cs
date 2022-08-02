using Moj.CMS.Domain.Aggregates.Court;
using Moj.CMS.Domain.Aggregates.Court.Entities;
using Moj.CMS.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.Interfaces.Queries
{
    [ScopedService]
    public interface ICourtQueries
    {
        Task<string> GetDivisionCourtCodeAsync(string divisionCode);

        Task<int> GetDivisionIdByCodeAsync(string divisionCode);

        Task<int> GetCourtIdByCodeAsync(string courtCode);

        Task<IEnumerable<SelectListItem>> GetDivisionListAsync();

        Task<IEnumerable<SelectListItem>> GetDivisionListByCourtCodeAsync(string courtCode);

        Task<IEnumerable<SelectListItem>> GetCourtListAsync();

        Task<List<Division>> GetDivisionsByCodeAsync(string[] divisionCodes);

        Task<List<Court>> GetCourtsByCodeAsync(string[] courtCodes);
    }
}
