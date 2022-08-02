using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Promissory.Queries.GetAllPromissories
{
    public class GetAllPromissoriesApiQuery : Query<IResult<PagedResult<GetAllPromissoriesDto>>>
    {
        public PromissoriesPagedApiRequest Request { get; set; }
    }

    public class GetAllCasesApiQueryHandler : IRequestHandler<GetAllPromissoriesApiQuery, IResult<PagedResult<GetAllPromissoriesDto>>>
    {
        private readonly IPromissoryQueries _promissoryQueries;

        public GetAllCasesApiQueryHandler(IPromissoryQueries promissoryQueries)
        {
            _promissoryQueries = promissoryQueries;
        }

        public async Task<IResult<PagedResult<GetAllPromissoriesDto>>> Handle(GetAllPromissoriesApiQuery query, CancellationToken cancellationToken)
        {
            var result = await _promissoryQueries.GetAllAsync(query.Request);
            return Result<PagedResult<GetAllPromissoriesDto>>.Success(result);
        }

    }
}
