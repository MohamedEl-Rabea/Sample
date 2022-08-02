using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetEffectsAndDiscount
{
    public class GetClaimEffectsQuery : Query<Result<IEnumerable<GetEffectsAndDiscountDto>>>
    {
        public int CaseId { get; set; }
    }
    public class GetClaimEffectsQueryHandler : IRequestHandler<GetClaimEffectsQuery, Result<IEnumerable<GetEffectsAndDiscountDto>>>
    {
        private readonly IClaimQueries _claimQueries;

        public GetClaimEffectsQueryHandler(IClaimQueries claimQueries)
        {
            _claimQueries = claimQueries;
        }
        public async Task<Result<IEnumerable<GetEffectsAndDiscountDto>>> Handle(GetClaimEffectsQuery request, CancellationToken cancellationToken)
        {
            var data = await _claimQueries.GetClaimEffectsAsync(request.CaseId);
            return Result<IEnumerable<GetEffectsAndDiscountDto>>.Success(data);
        }
    }
}
