using System.Threading.Tasks;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Entities;

namespace Moj.CMS.Domain.Aggregates.Case.BusinessRules
{
    public class NoDuplicatePromissoryNumberAllowedBusniessRule : IAsyncBusinessRule
    {
        private readonly IEnforcePromissoryNumberIsUnique _enforcePromissoryNumberIsUnique;
        private readonly int _promissoryId;
        private readonly string _promissoryStampNumber;


        public NoDuplicatePromissoryNumberAllowedBusniessRule(IEnforcePromissoryNumberIsUnique enforcePromissoryStampNumberIsUnique, string promissoryStampNumber, int promissoryId)
        {
            _enforcePromissoryNumberIsUnique = enforcePromissoryStampNumberIsUnique;
            _promissoryStampNumber = promissoryStampNumber;
            _promissoryId = promissoryId;
        }

        public string Message => "Cannot add duplicate Promissory number.";

        public async Task<bool> IsBrokenAsync()
        {
            var isUnique = await _enforcePromissoryNumberIsUnique.IsUniqueAsync(_promissoryId, _promissoryStampNumber);
            return !isUnique;
        }
    }
}
