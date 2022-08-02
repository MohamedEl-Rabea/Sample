using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseBasicDetails
{
    public class GetCaseBasicDetailsQuery : Query<Result<CaseBasicDetailsDto>>
    {
        public int CaseId { get; set; }
    }
    public class GetCaseBasicDetailsQueryHandler : IRequestHandler<GetCaseBasicDetailsQuery, Result<CaseBasicDetailsDto>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCaseBasicDetailsQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<CaseBasicDetailsDto>> Handle(GetCaseBasicDetailsQuery request, CancellationToken cancellationToken)
        {
            var data = await _caseQueries.GetCaseBasicDetailsAsync(request.CaseId);
            return Result<CaseBasicDetailsDto>.Success(data);
        }
    }
}
