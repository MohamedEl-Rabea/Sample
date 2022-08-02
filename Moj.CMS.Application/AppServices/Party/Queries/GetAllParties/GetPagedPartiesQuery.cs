using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class GetPagedPartiesQuery : PagedQuery<PartyListItemDto>
    {
    }


    public class GetPagedPartiesQueryHandler : IRequestHandler<GetPagedPartiesQuery, PagedResult<PartyListItemDto>>
    {
        private readonly IPartyQueries partyQueries;

        public GetPagedPartiesQueryHandler(IPartyQueries partyQueries)
        {
            this.partyQueries = partyQueries;
        }

        public async Task<PagedResult<PartyListItemDto>> Handle(GetPagedPartiesQuery query, CancellationToken cancellationToken)
        {
            var result = await partyQueries.GetAllPartiesAsync(query.PagedRequest);
            return result;
        }
    }
}
