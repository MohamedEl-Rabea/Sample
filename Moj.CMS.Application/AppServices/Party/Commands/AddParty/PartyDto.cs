using Moj.CMS.Domain.Shared.Enums;
using System;

namespace Moj.CMS.Application.AppServices.Party.Commands.AddParty
{
    public class PartyDto
    {
        public string PartyNumber { get; set; }
        public PartyTypeEnum PartyTypeId { get; set; }
        public PartyIdentityTypeEnum PartyIdentityTypeId { get; set; }
        public PartyLocationEnum PartyLocationId { get; set; }
        public string PartyIdentityNumber { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string NationalityCode { get; set; }
    }
}
