using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseNumbersbyPromissoryNumber
{
    public class GetCaseNumbersbyPromissoryNumberQuery : Query<Result<List<string>>>
    {
        public string PromissoryNumber { get; set; }
    }

    public class GetCaseNumbersbyPromissoryNumberQueryHandler : IRequestHandler<GetCaseNumbersbyPromissoryNumberQuery, Result<List<string>>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCaseNumbersbyPromissoryNumberQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<List<string>>> Handle(GetCaseNumbersbyPromissoryNumberQuery request, CancellationToken cancellationToken)
        {
            var result = await _caseQueries.GetCaseNumbersByPromissoryNumberAsync(request.PromissoryNumber);
            return Result<List<string>>.Success(result);
        }
    }
}
