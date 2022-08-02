using Moj.CMS.Domain.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Shared.Values
{
    public class VIbanReferenceDetails : ValueObject
    {
        private VIbanReferenceDetails()
        {

        }

        public string ReferenceNumber { get; private set; }
        public VIbanReferenceTypeEnum ReferenceType { get; private set; }

        public static VIbanReferenceDetails Create(string referenceNumber, VIbanReferenceTypeEnum referenceType)
        {
            Guard.Guard.AssertArgumentNotNullOrEmptyOrWhitespace(referenceNumber, nameof(referenceNumber));

            var referenceDetails = new VIbanReferenceDetails
            {
                ReferenceNumber = referenceNumber,
                ReferenceType = referenceType
            };
            return referenceDetails;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ReferenceNumber;
            yield return ReferenceType;
        }
    }
}
