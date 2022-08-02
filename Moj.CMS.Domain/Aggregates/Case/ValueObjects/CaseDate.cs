using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Case.ValueObjects
{
    public class CaseDate : ValueObject
    {
        public DateTime ReceiveDate { get; private set; }
        public DateTime JudgeAcceptanceDate { get; private set; }

        public static CaseDate Create(DateTime receiveDate, DateTime judgeAcceptanceDate)
        {
            var caseDate = new CaseDate
            {
                ReceiveDate = receiveDate,
                JudgeAcceptanceDate = judgeAcceptanceDate,
            };
            return caseDate;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return JudgeAcceptanceDate;
            yield return ReceiveDate;
        }
    }
}