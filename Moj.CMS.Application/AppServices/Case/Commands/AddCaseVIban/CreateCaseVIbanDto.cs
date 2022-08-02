using System;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCaseVIban
{
    public class CreateCaseVIbanDto
    {
        public string CaseNumber { get; set; }
        public string VIban { get; set; }
        public decimal CAP { get; set; }
        public string Alias { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
