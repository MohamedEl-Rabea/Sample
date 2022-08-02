using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseNumbersbyPartyId
{
    public class GetCaseNumbersbyPartyIdQuery : Query<Result<List<string>>>
    {
        public int PartyId { get; set; }
    }

    public class GetCaseNumbersbyPartyIdQueryHandler : IRequestHandler<GetCaseNumbersbyPartyIdQuery, Result<List<string>>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCaseNumbersbyPartyIdQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<List<string>>> Handle(GetCaseNumbersbyPartyIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _caseQueries.GetCaseNumbersByPartyIdAsync(request.PartyId);
            return Result<List<string>>.Success(result);
        }
    }
}
