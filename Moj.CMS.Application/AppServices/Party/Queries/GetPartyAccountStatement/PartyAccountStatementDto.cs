using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Party.Queries.Dtos
{
    public class PartyAccountStatementDto
    {
        public string FinancialTransactionNumber { get; set; }
        public DateTime FinancialTransactionDate { get; set; }
        public FinancialTransactionTypeEnum FinancialTransactionType { get; set; }
        public string FinancialTransactionRef { get; set; }
        public string FinancialTransactionSource { get; set; }
        public MoneyDto FinancialTransactionPaidAmount { get; set; }
        public MoneyDto FinancialTransactionTransferedAmount { get; set; }
        public MoneyDto FinancialTransactionRemainingAmount { get; set; }
        public MoneyDto FinalBalanceAmount { get; set; }
        public string FinancialTransactionCaseNumber { get; set; }

    }
}
