using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.SadadInvoice.ValueObjects
{
    public class SadadCashNotification : ValueObject, ICreationAudited
    {
        private SadadCashNotification()
        {

        }

        public decimal CashAmount { get; private set; }
        public static SadadCashNotification Create(decimal cashAmount)
        {
            var sadadCashNotification = new SadadCashNotification
            {
                CashAmount = cashAmount
            };
            return sadadCashNotification;
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CashAmount;
        }

        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}