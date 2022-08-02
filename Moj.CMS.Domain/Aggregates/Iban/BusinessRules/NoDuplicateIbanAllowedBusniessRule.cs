using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Entities;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Iban.BusinessRules
{
    public class NoDuplicateIbanAllowedBusniessRule : IAsyncBusinessRule
    {
        private readonly string _ibanNumber;
        private readonly IEnforceIbanNumberIsUnique _enforceIbanNumberIsUnique;
        public string Message => "Cannot add duplicate Bank Account";

        public NoDuplicateIbanAllowedBusniessRule(string IbanNumber, IEnforceIbanNumberIsUnique enforceIbanNumberIsUnique)
        {
            _ibanNumber = IbanNumber;
            _enforceIbanNumberIsUnique = enforceIbanNumberIsUnique;
        }

        public async Task<bool> IsBrokenAsync()
        {
            var isUnique = await _enforceIbanNumberIsUnique.IsUniqueIbanAsync(_ibanNumber);
            return !isUnique;
        }
    }
}