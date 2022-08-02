using MediatR;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Promissory.Queries
{
    public class GetPromissoryQuery : Query<Result<PromissoryDto>>
    {
        public string PromissoryNumber { get; set; }

    }

    public class GetPromissoryQueryHandler : IRequestHandler<GetPromissoryQuery, Result<PromissoryDto>>
    {
        private readonly IPromissoryQueries _promissoryQueries;

        public GetPromissoryQueryHandler(IPromissoryQueries promissoryQueries)
        {
            _promissoryQueries = promissoryQueries;

        }

        public async Task<Result<PromissoryDto>> Handle(GetPromissoryQuery request, CancellationToken cancellationToken)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(request.PromissoryNumber, nameof(request.PromissoryNumber));
            var promissory = await _promissoryQueries.GetPromissory(request.PromissoryNumber);
            return Result<PromissoryDto>.Success(promissory);
        }
    }
}
