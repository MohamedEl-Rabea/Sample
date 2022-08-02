using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class GetAllPartiesQuery : Query<Result<IEnumerable<PartyListDto>>>
    {
        public GetAllPartiesQuery()
        {

        }
    }
    public class GetAllPartiesQueryHandler : IRequestHandler<GetAllPartiesQuery, Result<IEnumerable<PartyListDto>>>
    {
        private readonly IPartyQueries _partyQueries;

        public GetAllPartiesQueryHandler(IPartyQueries partyQueries)
        {
            _partyQueries = partyQueries;
        }

        public async Task<Result<IEnumerable<PartyListDto>>> Handle(GetAllPartiesQuery request, CancellationToken cancellationToken)
        {
            var parties = await _partyQueries.GetAllPartiesAsync();
            return Result<IEnumerable<PartyListDto>>.Success(parties);
        }
    }
}
