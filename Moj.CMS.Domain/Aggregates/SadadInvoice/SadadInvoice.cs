using Moj.CMS.Domain.Aggregates.SadadInvoice.BusinessRules;
using Moj.CMS.Domain.Aggregates.SadadInvoice.ValueObjects;
using Moj.CMS.Domain.ParameterObjects.SadadInvoice;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.SadadInvoice
{
    public class SadadInvoice : FullAuditedAggregateRoot, IAggregateRoot
    {
        public SadadInvoice()
        {
            _sadadPaymentNotification = new List<SadadPaymentNotification>();
            _sadadCashNotification = new List<SadadCashNotification>();
        }

        public string Number { get; private set; }
        public string ClaimNumber { get; private set; }
        public string PartyNumber { get; private set; }
        public string VIban { get; private set; }
        public Money Amount { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public string Description { get; private set; }
        public Money MinBillableAmount { get; private set; }
        public bool IsDraft { get; private set; }

        public IReadOnlyCollection<SadadPaymentNotification> SadadPaymentNotification => _sadadPaymentNotification.ToList();
        private readonly List<SadadPaymentNotification> _sadadPaymentNotification;
        public IReadOnlyCollection<SadadCashNotification> SadadCashNotifications => _sadadCashNotification.ToList();
        private readonly List<SadadCashNotification> _sadadCashNotification;

        public static async Task<SadadInvoice> Draft(CreateSadadInvoiceParam param)
        {
            var sadadInvoice = new SadadInvoice();
            sadadInvoice.IsDraft = true;
            await sadadInvoice.SetSadadInvoiceInfo(param);
            return sadadInvoice;
        }

        public void Final(string invoiceNumber)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(invoiceNumber, nameof(invoiceNumber));

            Number = invoiceNumber;
            IsDraft = false;
        }

        private void SetVIban(string vIban)
        {
            VIban = vIban;
        }

        private async Task SetSadadInvoiceInfo(CreateSadadInvoiceParam param)
        {
            //Arguments validations
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.Number, nameof(param.Number), () => !IsDraft);
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.ClaimNumber, nameof(param.ClaimNumber));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.PartyNumber, nameof(param.PartyNumber));
            Guard.AssertArgumentNotNull(param.Amount, nameof(param.Amount));

            //Business rules check
            await CheckRuleAsync(new ClaimMustBeExistsBusinessRule(param.ClaimNumber, param.ClaimInfoProvider));
            await CheckRuleAsync(new PartyMustBeAccusedBusinessRule(param.ClaimNumber, param.PartyNumber, param.ClaimInfoProvider));
            await CheckRuleAsync(new AmountMustBeLessThanOrEqualClaimAmountBusinessRule(param.ClaimNumber, param.Amount,
                param.ClaimInfoProvider));

            Number = param.Number;
            ClaimNumber = param.ClaimNumber;
            PartyNumber = param.PartyNumber;
            Amount = param.Amount;
            DueDate = param.DueDate;
            IssueDate = param.IssueDate;
            ExpiryDate = param.ExpiryDate;
            Description = param.Description;
            MinBillableAmount = param.MinBillableAmount;
        }

        public void AddSadadPaymentNotification(IEnumerable<SadadPaymentNotification> sadadPaymentNotifications)
        {
            _sadadPaymentNotification.AddRange(sadadPaymentNotifications);
        }
        public void AddSadadCashNotification(IEnumerable<SadadCashNotification> sadadCashNotifications)
        {
            _sadadCashNotification.AddRange(sadadCashNotifications);
        }
    }
}
