using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
      public class GetPartyCreditsSummaryQuery : Query<Result<PartyCreditsSummaryDto>>
    {
        public int PartyId { get; set; }
    }

    public class GetPartyCreditsSummaryQueryHandler : IRequestHandler<GetPartyCreditsSummaryQuery, Result<PartyCreditsSummaryDto>>
    {
        private readonly IPartyQueries _partyQueries;
        public GetPartyCreditsSummaryQueryHandler(IPartyQueries partyQueries)
        {
            _partyQueries = partyQueries;
        }

        public async Task<Result<PartyCreditsSummaryDto>> Handle(GetPartyCreditsSummaryQuery query, CancellationToken cancellationToken)
        {
            var data = await _partyQueries.GetPartyCreditsSummary(query.PartyId);
            return Result<PartyCreditsSummaryDto>.Success(data);
        }
    }
}
