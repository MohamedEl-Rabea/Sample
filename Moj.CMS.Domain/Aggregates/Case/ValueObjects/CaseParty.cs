using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Case.ValueObjects
{
    public class CaseParty : ValueObject, ICreationAudited
    {
        private CaseParty()
        {

        }

        public string PartyNumber { get; private set; }
        public string PromissoryNumber { get; private set; }
        public PartyRoleEnum PartyRoleId { get; private set; }
        public bool IsApplicant { get; set; }
        public PartyClassificationEnum PartyClassificationId { get; set; }

        public static CaseParty Create(CasePartyCreationParam casePartyCreationParam)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(casePartyCreationParam.PartyNumber, nameof(casePartyCreationParam.PartyNumber));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(casePartyCreationParam.PromissoryNumber, nameof(casePartyCreationParam.PromissoryNumber));

            var caseParty = new CaseParty
            {
                PartyNumber = casePartyCreationParam.PartyNumber,
                PromissoryNumber = casePartyCreationParam.PromissoryNumber,
                PartyRoleId = casePartyCreationParam.PartyRoleTypeId,
                IsApplicant = casePartyCreationParam.IsApplicant,
                PartyClassificationId = casePartyCreationParam.PartyClassificationId
            };
            return caseParty;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PartyNumber;
            yield return PromissoryNumber;
            yield return IsApplicant;
            yield return PartyRoleId;
            yield return PartyClassificationId;
        }

        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}