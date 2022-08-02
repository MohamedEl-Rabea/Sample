namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateCaseCourtDetailsCommand
{
    public class UpdateCaseCourtDetailsDto
    {
        public string CaseNumber { get; set; }
        public string CourtCode { get; set; }
        public string DivisionCode { get; set; }
        public string JudgeCode { get; set; }
    }
}
