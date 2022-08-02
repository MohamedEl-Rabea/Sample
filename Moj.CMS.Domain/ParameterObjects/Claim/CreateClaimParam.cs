using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.ParameterObjects.Claim
{
    public class CreateClaimParam
    {
        public string ClaimNumber { get; set; }
        public DebtTypeEnum DebtTypeId { get; set; }
        public DateTime ClaimDate { get; set; }
        public string CaseNumber { get; set; }
        public string PromissoryNumber { get; set; }
        public string ComplaintPartyNumber { get; set; }
        public bool IsJoint { get; set; }
        public Money BasicAmount { get; set; }
        public Money RequiredAmount { get; set; }
        public Money RemainingAmount { get; set; }
        public IEnumerable<ClaimDetails> ClaimDetailsList { get; set; }
        public IEnforceCaseIsFound EnforceCaseIsFound { get; set; }
        public IGetCasePartiesNumbers EnforcePartiesAreAssignedToCase { get; set; }
        public bool ShouldRegisterDomainEvent { get; set; } = true;
    }
}
