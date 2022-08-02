using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.SadadInvoice.ValueObjects
{
    public class SadadPaymentNotification : ValueObject, ICreationAudited
    {
        private SadadPaymentNotification()
        {

        }

        public decimal PaidAmount { get; private set; }
        public static SadadPaymentNotification Create(decimal paidAmount)
        {
            var sadadPaymentNotification = new SadadPaymentNotification
            {
                PaidAmount = paidAmount
            };
            return sadadPaymentNotification;
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PaidAmount;
        }

        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}
