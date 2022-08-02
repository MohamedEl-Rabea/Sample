using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents
{
    public class GetCaseEventsQuery : Query<Result<IEnumerable<CaseEventsDto>>>
    {
        public int CaseId { get; set; }
    }

    public class GetCaseEventsQueryHandler : IRequestHandler<GetCaseEventsQuery, Result<IEnumerable<CaseEventsDto>>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCaseEventsQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<IEnumerable<CaseEventsDto>>> Handle(GetCaseEventsQuery request, CancellationToken cancellationToken)
        {
            var data = await _caseQueries.GetCaseEventsAsync(request.CaseId);
            return Result<IEnumerable<CaseEventsDto>>.Success(data); ;
        }
    }
}
