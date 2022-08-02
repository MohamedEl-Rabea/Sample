using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyBasicInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public PartyIdentityTypeEnum PartyIdentityTypeId { get; set; }
    }
}
