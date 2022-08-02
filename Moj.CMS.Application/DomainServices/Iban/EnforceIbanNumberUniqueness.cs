using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.DomainServices;
using System.Threading.Tasks;

namespace Moj.CMS.Application.DomainServices.Iban
{
    public class EnforceIbanNumberUniqueness : IEnforceIbanNumberIsUnique
    {
        private readonly IIbanQueries _IbanQueries;

        public EnforceIbanNumberUniqueness(IIbanQueries IbanQueries)
        {
            _IbanQueries = IbanQueries;
        }

        public async Task<bool> IsUniqueIbanAsync(string ibanNumber)
        {
            var ibanId = await _IbanQueries.GetIbanIdAsyc(ibanNumber);
            return ibanId ==0;
        }
    }
}