namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateCaseVIban
{
    public class UpdateCaseVIbanDto
    {
        public string CaseNumber { get; set; }
        public string VIban { get; set; }
        public decimal CAP { get; set; }
        public string Alias { get; set; }
    }
}