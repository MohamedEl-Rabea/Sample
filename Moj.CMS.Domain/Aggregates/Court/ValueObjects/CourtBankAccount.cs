using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Court.ValueObjects
{
    public class CourtBankAccount : ValueObject, ICreationAudited
    {
        private CourtBankAccount()
        {
        }

        public string AccountNumber { get; private set; }
        public bool IsActive { get; private set; }
        public string CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }

        public static CourtBankAccount Create(string accountNumber, bool isActive)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(accountNumber, nameof(accountNumber));

            return new CourtBankAccount
            {
                AccountNumber = accountNumber,
                IsActive = isActive,
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AccountNumber;
            yield return IsActive;
        }
    }
}