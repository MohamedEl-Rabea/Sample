using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Domain.ParameterObjects.Case
{
    public class CasePartyCreationParam
    {
        public string PartyNumber { get; set; }
        public string PromissoryNumber { get; set; }
        public PartyRoleEnum PartyRoleTypeId { get; set; }
        public bool IsApplicant { get; set; }
        public PartyClassificationEnum PartyClassificationId { get; set; }
    }
}
