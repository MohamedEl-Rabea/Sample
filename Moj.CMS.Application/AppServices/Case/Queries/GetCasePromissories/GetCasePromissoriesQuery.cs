using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCasePromissories
{
    public class GetCasePromissoriesQuery : Query<Result<IEnumerable<GetCasePromissoriesDto>>>
    {
        public int CaseId { get; set; }
    }
    public class GetCasePromissoriesQueryHandler : IRequestHandler<GetCasePromissoriesQuery, Result<IEnumerable<GetCasePromissoriesDto>>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCasePromissoriesQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<IEnumerable<GetCasePromissoriesDto>>> Handle(GetCasePromissoriesQuery request, CancellationToken cancellationToken)
        {
            var data = await _caseQueries.GetCasePromissoriesAsync(request.CaseId);
            return  Result<IEnumerable<GetCasePromissoriesDto>>.Success(data);
        }
    }
}
