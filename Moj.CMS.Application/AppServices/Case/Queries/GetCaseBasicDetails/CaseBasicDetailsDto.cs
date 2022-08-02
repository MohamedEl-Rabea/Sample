using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseBasicDetails
{
    public class CaseBasicDetailsDto
    {
        public string CaseNumber { get; set; }
        public string CourtName { get; set; }
        public string DivisionName { get; set; }
        public string JudgeName { get; set; }
        public string MahasaJudgeName { get; set; }
        public MoneyDto Amount { get; set; } = new MoneyDto();
        public string DebtType { get; set; }
    }
}