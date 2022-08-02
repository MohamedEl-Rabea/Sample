using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class GetPartyDebtsSummaryQuery : Query<Result<PartyDebtsSummaryDto>>
    {
        public int PartyId { get; set; }
    }

    public class PartyDebtsSummaryQueryHandler : IRequestHandler<GetPartyDebtsSummaryQuery, Result<PartyDebtsSummaryDto>>
    {
        private readonly IPartyQueries _partyQueries;
        public PartyDebtsSummaryQueryHandler(IPartyQueries partyQueries)
        {
            _partyQueries = partyQueries;
        }

        public async Task<Result<PartyDebtsSummaryDto>> Handle(GetPartyDebtsSummaryQuery query, CancellationToken cancellationToken)
        {
            var data = await _partyQueries.GetPartyDebtsSummary(query.PartyId);
            return Result<PartyDebtsSummaryDto>.Success(data);
        }
    }
}
