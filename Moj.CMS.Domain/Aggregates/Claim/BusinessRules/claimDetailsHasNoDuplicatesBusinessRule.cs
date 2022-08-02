using Moj.CMS.Domain.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Domain.Aggregates.Claim.BusinessRules
{
    public class ClaimDetailsHasNoDuplicatePartiesBusinessRule : IBusinessRule
    {
        private readonly IEnumerable<string> _requestParties;
        private readonly IEnumerable<string> _existingParties;
        private IEnumerable<string> _commonParties;

        public ClaimDetailsHasNoDuplicatePartiesBusinessRule(IEnumerable<string> requestParties, IEnumerable<string> existingParties)
        {
            _requestParties = requestParties;
            _existingParties = existingParties;
        }
        public string Message => $"Parties with numbers [{string.Join(",", _commonParties)}] already exist on the current claim";
        public bool IsBroken()
        {
            _commonParties = _requestParties.Intersect(_existingParties);
            return _commonParties.Any();
        }
    }
}
