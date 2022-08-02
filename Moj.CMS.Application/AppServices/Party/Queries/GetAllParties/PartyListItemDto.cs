using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyListItemDto : AuditedDto
    {
        public int Id { get; set; }
        [Exportable(Order = 1)]
        public string Number { get; set; }
        [Exportable(Order = 2)]
        public string Name { get; set; }

        [Exportable(Order = 4)]
        public string IdentityTypeText { get; set; }
        public int IdentityTypeId { get; set; }

        [Exportable(Order = 3)]
        public string IdentityNumber { get; set; }

        [Exportable(Order = 5)]
        public string PartyTypeText { get; set; }
        public int PartyTypeId { get; set; }
        [Exportable(Order = 6)]
        public string NationalityText { get; set; }
        public string NationalityCode { get; set; }
        [Exportable(Order = 7)]
        public int DebtCaseCount { get; set; }

        [Exportable(Order = 8)]
        public int CreditCaseCount { get; set; }

        [Exportable(Order = 9)]
        public decimal TotalCreditAmountValue { get; set; }

        [Exportable(Order = 10)]
        public decimal TotalDebtAmountValue { get; set; }

        [Exportable(Order = 11)]
        public string Currency { get; set; }
        [Exportable(Order = 12)]
        public string PartyIbanNumber { get; set; }
        [Exportable(Order = 13)]
        public string PartyPhoneNumber { get; set; }
        public MoneyDto TotalCreditAmountMoney => new MoneyDto(Currency, TotalCreditAmountValue);
        public MoneyDto TotalDebtAmountMoney => new MoneyDto(Currency, TotalDebtAmountValue);
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string LocationText { get; set; }
    }
}
