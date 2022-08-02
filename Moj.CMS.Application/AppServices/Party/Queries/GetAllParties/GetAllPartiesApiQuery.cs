using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class GetAllPartiesApiQuery : Query<IResult<PagedResult<PartyListItemDto>>>
    {
        public PartiesPagedApiRequest Request { get; set; }
    }

    public class GetAllPartiesApiQueryHandler : IRequestHandler<GetAllPartiesApiQuery, IResult<PagedResult<PartyListItemDto>>>
    {
        private readonly IPartyQueries _partyQueries;

        public GetAllPartiesApiQueryHandler(IPartyQueries partyQueries)
        {
            _partyQueries = partyQueries;
        }

        public async Task<IResult<PagedResult<PartyListItemDto>>> Handle(GetAllPartiesApiQuery query, CancellationToken cancellationToken)
        {
            var result = await _partyQueries.GetAllPartiesAsync(query.Request);
            return Result<PagedResult<PartyListItemDto>>.Success(result);
        }
    }
}
