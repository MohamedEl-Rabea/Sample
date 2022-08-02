namespace Moj.CMS.Application.AppServices.Party.Queries.GetPartyBankAccounts
{
    public class PartyBankAccountDto
    {
        public string PartyNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string AccountMovementsUrl { get; set; }
        public string AccountVerificationStatus { get; set; }
        public string AccountValidityStatus { get; set; }
        public string NotHasAccountReason { get; set; }
        public string CaseNumber { get; set; }
        public string BankName { get; set; }
    }
}
