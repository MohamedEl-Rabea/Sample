using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.Shared.Enums;
using System;

namespace Moj.CMS.Domain.ParameterObjects.Party
{
    public class PartyInfoParam
    {
        public string PartyNumber { get; set; }
        public PartyTypeEnum PartyTypeId { get; set; }
        public PartyLocationEnum PartyLocationId { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string NationalityCode { get; set; }

        public PartyIdentity PartyIdentity { get; set; }
        public IEnforcePartyNumberIsUnique EnforcePartyNumberIsUnique { get; set; }
    }
}
