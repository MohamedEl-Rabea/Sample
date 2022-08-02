using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Case.ValueObjects
{
    public class CasePromissory : ValueObject, ICreationAudited
    {
        private CasePromissory()
        {

        }

        public string PromissoryNumber { get; private set; }

        public static CasePromissory Create(string promissoryNumber)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(promissoryNumber, nameof(promissoryNumber));

            var casePromissory = new CasePromissory
            {
                PromissoryNumber = promissoryNumber
            };
            return casePromissory;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PromissoryNumber;
        }

        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}
