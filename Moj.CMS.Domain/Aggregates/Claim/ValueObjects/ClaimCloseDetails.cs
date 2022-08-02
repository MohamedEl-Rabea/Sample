using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Claim.ValueObjects
{
    public class ClaimCloseDetails : ValueObject
    {
        public string ReferenceNumber { get; private set; }
        public DateTime CloseDate { get; private set; }

        public static ClaimCloseDetails Create(string referenceNumber, DateTime closeDate)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(referenceNumber, nameof(ReferenceNumber));

            var closeDetails = new ClaimCloseDetails
            {
                ReferenceNumber = referenceNumber,
                CloseDate = closeDate
            };
            return closeDetails;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ReferenceNumber;
            yield return CloseDate;
        }
    }
}