namespace Moj.CMS.Application.AppServices.Case.Commands.AddSadadInvoice
{
    public class CaseSadadDto
    {
        public string CaseNumber { get; set; }
        public string PartyIdentityNumber { get; set; }
        public string SadadNumber { get; set; }
        public decimal RequiredAmount { get; set; }
        public decimal CAP { get; set; }
    }
}
