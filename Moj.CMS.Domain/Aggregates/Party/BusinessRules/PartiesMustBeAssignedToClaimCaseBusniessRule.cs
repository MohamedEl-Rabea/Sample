using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.Lookups;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Party.BusinessRules
{
    public class PartiesMustBeAssignedToClaimCaseBusniessRule : IAsyncBusinessRule
    {
        private readonly IGetCasePartiesNumbers _getCasePartiesNumbers;
        private readonly IEnumerable<string> _partiesNumbers;
        private readonly string _caseNumber;
        private readonly string _complaintPartyNumber;

        public PartiesMustBeAssignedToClaimCaseBusniessRule(string caseNumber, string ComplaintPartyNumber, IEnumerable<string> partiesNumbers, IGetCasePartiesNumbers getCasePartiesNumbers)
        {
            _partiesNumbers = partiesNumbers;
            _caseNumber = caseNumber;
            _complaintPartyNumber = ComplaintPartyNumber;
            _getCasePartiesNumbers = getCasePartiesNumbers;
        }

        public string Message { get; private set; }

        public async Task<bool> IsBrokenAsync()
        {
            var casePartiesInfo = await _getCasePartiesNumbers.GetAsync(_caseNumber);
            var partiesRolesAreValid = ValidatePartiesRolesAreValid(casePartiesInfo);
            return !partiesRolesAreValid;
        }

        private bool ValidatePartiesRolesAreValid(Dictionary<string, PartyRoleEnum> casePartiesInfo)
        {
            var isComplaintPartyInResult = casePartiesInfo.ContainsKey(_complaintPartyNumber);
            var isComplaintPartyRoleValid = isComplaintPartyInResult ? PartyRoleLookup.Find(casePartiesInfo[_complaintPartyNumber]).IsComplaint : false;
            if (!isComplaintPartyRoleValid)
                Message += $"Complaint Party with number={_complaintPartyNumber} are not assigned to complaint role in case= {_caseNumber} ** ";

            var notExisitingAccusedParties = _partiesNumbers.Except(casePartiesInfo.Keys);
            var accusedPartiesRoles = casePartiesInfo.Where(cp => cp.Key != _complaintPartyNumber && _partiesNumbers.Contains(cp.Key));
            var invalidAccusedParties = accusedPartiesRoles.Where(accusedRole => PartyRoleLookup.Find(accusedRole.Value).IsComplaint).Select(x => x.Key);
            var allInvalidParties = notExisitingAccusedParties.Concat(invalidAccusedParties);
            if (allInvalidParties.Any())
                Message += $"Accused Parties with the following  numbers [{string.Join(",", allInvalidParties)}] are not assigned to accused role in case= {_caseNumber} ** ";

            return string.IsNullOrEmpty(Message);
        }
    }
}
