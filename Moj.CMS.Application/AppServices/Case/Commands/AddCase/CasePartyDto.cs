using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCase
{
    public class CasePartyDto
    {
        public PartyRoleEnum PartyRole { get; set; }
        public bool IsApplicant { get; set; }
        public PartyDto Details { get; set; }
    }
}
