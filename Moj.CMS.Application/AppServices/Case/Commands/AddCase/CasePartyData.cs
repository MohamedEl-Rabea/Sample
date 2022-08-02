using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCase
{
    public class CasePartyData
    {
        public int PartyId { get; set; }
        public int PromissoryId { get; set; }
        public PartyRoleEnum PartyRole { get; set; }
        public PartyClassificationEnum PartyClassificationId { get; set; }
        public bool IsApplicant { get; set; }
    }
}