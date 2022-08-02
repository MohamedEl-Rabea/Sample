using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Shared.Queries;
using System.Threading.Tasks;

namespace Moj.CMS.Application.DomainServices.Case
{
    public class EnforceCourtIsExists : IEnforceCourtIsExists
    {
        private readonly ICourtQueries _courtQueries;
        public EnforceCourtIsExists(ICourtQueries courtQueries)
        {
            _courtQueries = courtQueries;
        }

        public async Task<bool> IsExistAsync(string courtCode)
        {
            var courtId = await _courtQueries.GetCourtIdByCodeAsync(courtCode);
            return courtId != 0;
        }
    }
}
