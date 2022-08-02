using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Shared.Queries;
using System.Threading.Tasks;

namespace Moj.CMS.Application.DomainServices.Case
{
    public class EnforceJudgeIsExists : IEnforceJudgeIsExists
    {
        private readonly ILookupsQueries _lookupQueries;
        public EnforceJudgeIsExists(ILookupsQueries lookupQueries)
        {
            _lookupQueries = lookupQueries;
        }

        public async Task<bool> IsExistAsync(string judgeCode)
        {
            var judgeId = await _lookupQueries.GetJudgeIdByCodeAsync(judgeCode);
            return judgeId != 0;
        }
    }
}
