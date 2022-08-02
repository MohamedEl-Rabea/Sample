using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.DomainServices;
using System.Threading.Tasks;

namespace Moj.CMS.Application.DomainServices.Case
{
    public class GetDivisionCourtCode : IGetDivisionCourtCode
    {
        private readonly ICourtQueries _courtQueries;
        public GetDivisionCourtCode(ICourtQueries courtQueries)
        {
            _courtQueries = courtQueries;
        }

        public async Task<string> GetDivisionCourtCodeAsync(string divisionCode)
        {
            return await _courtQueries.GetDivisionCourtCodeAsync(divisionCode);
        }
    }
}
