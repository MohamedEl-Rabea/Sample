using MediatR;
using Moj.CMS.Application.AppServices.Party.Queries.Dtos;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class GetPartyAccountStatementQuery : Query<Result<IEnumerable<PartyAccountStatementDto>>>
    {
        public int PartyId { get; set; }
    }

    public class GetPartyAccountStatementQueryHandler : IRequestHandler<GetPartyAccountStatementQuery, Result<IEnumerable<PartyAccountStatementDto>>>
    {
        private readonly IClaimQueries _claimQueries;

        public GetPartyAccountStatementQueryHandler(IClaimQueries claimQueries)
        {
            _claimQueries = claimQueries;
        }

        public async Task<Result<IEnumerable<PartyAccountStatementDto>>> Handle(GetPartyAccountStatementQuery request, CancellationToken cancellationToken)
        {
            var data = await _claimQueries.GetPartyAccountStatement(request.PartyId);
            return Result<IEnumerable<PartyAccountStatementDto>>.Success(data);
        }
    }
}
