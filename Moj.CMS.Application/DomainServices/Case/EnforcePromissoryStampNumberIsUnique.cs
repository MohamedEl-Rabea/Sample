using Moj.CMS.Application.Interfaces.Queries;
using System.Threading.Tasks;
using Moj.CMS.Domain.DomainServices;

namespace Moj.CMS.Application.DomainServices.Case
{
    public class CheckPromissoryStampNumberUniqueness : IEnforcePromissoryNumberIsUnique
    {
        private readonly IPromissoryQueries _promissoryQueries;

        public CheckPromissoryStampNumberUniqueness(IPromissoryQueries promissoryQueries)
        {
            _promissoryQueries = promissoryQueries;
        }

        public async Task<bool> IsUniqueAsync(int promissoryId, string promissoryStampNumber)
        {
            var currentPromissoryId = (await _promissoryQueries.GetPromissoryId(promissoryStampNumber));
            return currentPromissoryId == 0 || currentPromissoryId == promissoryId;
        }
    }
}
