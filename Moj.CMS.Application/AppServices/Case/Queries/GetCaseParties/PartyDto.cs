using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseParties
{
    public class PartyDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Role { get; set; }
        public PartyRoleEnum RoleType { get; set; }
    }
}
