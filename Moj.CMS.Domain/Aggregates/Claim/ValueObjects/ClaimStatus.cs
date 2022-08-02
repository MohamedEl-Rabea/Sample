using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Claim.ValueObjects
{
    public class ClaimStatus : ValueObject
    {
        public ClaimFinancialStatusEnum FinancialStatus { get; private set; }
        public ClaimStatusEnum Status { get; private set; }

        private ClaimStatus()
        {

        }

        public static ClaimStatus Create(ClaimStatusEnum status, ClaimFinancialStatusEnum financialStatus)
        {
            return new ClaimStatus
            {
                Status = status,
                FinancialStatus = financialStatus
            };
        }

        public ClaimStatus Close()
        {
            return new ClaimStatus
            {
                Status = ClaimStatusEnum.Closed,
                FinancialStatus = FinancialStatus
            };
        }

        public ClaimStatus Paid()
        {
            return new ClaimStatus
            {
                Status = Status,
                FinancialStatus = ClaimFinancialStatusEnum.FullyPaid
            };
        }

        public ClaimStatus PartiallyPaid()
        {
            return new ClaimStatus
            {
                Status = Status,
                FinancialStatus = ClaimFinancialStatusEnum.PartiallyPaid
            };
        }

        public ClaimStatus NotPaid()
        {
            return new ClaimStatus
            {
                Status = Status,
                FinancialStatus = ClaimFinancialStatusEnum.NotPaid
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FinancialStatus;
            yield return Status;
        }
    }
}