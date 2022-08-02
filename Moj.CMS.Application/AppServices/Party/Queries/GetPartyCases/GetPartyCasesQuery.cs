using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class GetPartyCasesQuery : Query<Result<PartyCaseListDto>>
    {
        public int PartyId { get; set; }
    }

    public class GetPartyCasesQueryHandler : IRequestHandler<GetPartyCasesQuery, Result<PartyCaseListDto>>
    {
        private readonly IPartyQueries _partyQueries;

        public GetPartyCasesQueryHandler(IPartyQueries caseQueries)
        {
            _partyQueries = caseQueries;
        }

        public async Task<Result<PartyCaseListDto>> Handle(GetPartyCasesQuery request, CancellationToken cancellationToken)
        {
            var data = await _partyQueries.GetPartyCasesAsync(request.PartyId);
            return Result<PartyCaseListDto>.Success(data);
        }
    }
}
