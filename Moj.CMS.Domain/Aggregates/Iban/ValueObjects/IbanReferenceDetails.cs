using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Iban.ValueObjects
{
    public class IbanReferenceDetails: ValueObject
    {
        private IbanReferenceDetails()
        {

        }
        public string ReferenceNumber { get;private set; }
        public IbanPurposeEnum ReferenceType { get;private set; }

        public static IbanReferenceDetails Create(string referenceNumber, IbanPurposeEnum referenceType)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(referenceNumber, nameof(referenceNumber));

            var referenceDetails = new IbanReferenceDetails
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
