using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.AppServices.Party.Services
{
    [ScopedService]
    public interface IPartyService
    {
        Task<IEnumerable<SavedPartyInfo>> AddPartiesAsync(IEnumerable<PartyDto> requesters, IEnumerable<PartyDto> respondents, bool ignoreExistingParties);
    }
}
