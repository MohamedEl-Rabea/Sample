using MediatR;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseCollectionChannels;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims
{
    public class GetCaseCollectionChannelsQuery : Query<Result<CollectionChannelsDto>>
    {
        public int CaseId { get; set; }
    }

    public class GetCaseCollectionChannelsQueryHandler : IRequestHandler<GetCaseCollectionChannelsQuery, Result<CollectionChannelsDto>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCaseCollectionChannelsQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<CollectionChannelsDto>> Handle(GetCaseCollectionChannelsQuery request, CancellationToken cancellationToken)
        {
            var adjustmentChannels = await _caseQueries.GetCaseAdjustmentChannelsAsync(request.CaseId);

            var result = new CollectionChannelsDto
            {
                AdjustmentChannels = adjustmentChannels.ToList()
            };

            return Result<CollectionChannelsDto>.Success(result);
        }
    }
}
