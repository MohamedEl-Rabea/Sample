using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyListDto
    {
        IReadOnlyCollection<PartyDto> Parties { get; set; }
    }
}
