using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Case.ValueObjects
{
    public class CaseDetails : ValueObject, ICreationAudited
    {
        private CaseDetails()
        {

        }
        public string CourtCode { get; private set; }
        public string DivisionCode { get; private set; }
        public string JudgeCode { get; private set; }
        public bool IsCurrent { get; private set; }
        public static CaseDetails Create(string divisionCode, string courtCode, string judgeCode)
        {
            var caseParty = new CaseDetails
            {
                CourtCode = courtCode,
                DivisionCode = divisionCode,
                JudgeCode = judgeCode,
                IsCurrent = true
            };
            return caseParty;
        }
        public CaseDetails Moved()
        {
            var caseDetail = new CaseDetails
            {
                CourtCode = CourtCode,
                DivisionCode = DivisionCode,
                JudgeCode = JudgeCode,
                IsCurrent = false

            };
            return caseDetail;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CourtCode;
            yield return DivisionCode;
            yield return JudgeCode;
        }

        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}