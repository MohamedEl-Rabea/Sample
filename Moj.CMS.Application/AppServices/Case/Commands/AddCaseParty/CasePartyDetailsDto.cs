using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCaseParty
{
    public class CasePartyDetailsDto
    {
        public string PartyNumber { get; set; }
        public string PromissoryNumber { get; set; }
        public PartyRoleEnum PartyRoleId { get; set; }
        public bool IsApplicant { get; set; }
    }
}
