using Moj.CMS.Domain.ParameterObjects.VIban;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Domain.Aggregates.VIban
{
    public class VIban : FullAuditedAggregateRoot, IAggregateRoot
    {
        public VIban()
        {

        }

        public string Number { get; private set; }
        public string CaseNumber { get; private set; } //TODO
        public string Alias { get; private set; }
        public string BankName { get; private set; }
        public string Iban { get; private set; }
        public decimal CAP { get; private set; }
        public DateTime IssueDate { get; private set; }
        public bool IsActive { get; private set; }
        public VIbanReferenceDetails ReferenceDetails { get; private set; }

        public static VIban Create(CreateVIbanParam param)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.VIbanNumber, nameof(param.VIbanNumber));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.Alias, nameof(param.Alias));
            Guard.AssertNullableArgumentNotLessThanOrEqualToZero(param.CAP, nameof(param.CAP));

            var caseVIban = new VIban();
            caseVIban.SetVIbanInfo(param);
            return caseVIban;
        }

        private void SetVIbanInfo(CreateVIbanParam param)
        {
            Number = param.VIbanNumber;
            CAP = param.CAP;
            Alias = param.Alias;
            BankName = param.BankName;
            Iban = param.ParentAccountNumber;
            ReferenceDetails = param.ReferenceDetails;
            IssueDate = param.IssueDate;
            IsActive = true;
        }
    }
}
