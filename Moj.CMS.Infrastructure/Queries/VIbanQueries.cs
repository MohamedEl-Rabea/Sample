using Moj.CMS.Application.AppServices.VIban.Queries.GetAllVIbans;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.VIban;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Queries
{
    public class VIbanQueries : IVIbanQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;

        public VIbanQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator)
        {
            _queryBuilderCreator = queryBuilderCreator;
        }

        public async Task<PagedResult<VIbanDto>> GetAllAsync(PagedRequest<VIbanDto> request)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var query = queryBuilder.Query<VIban>().Select(v => new VIbanDto
            {
                AccountNumber = v.Number,
                Alias = v.Alias,
                IssueDate = v.IssueDate,
                IsActive = v.IsActive,
                ReferenceNumber = v.ReferenceDetails.ReferenceNumber,
                ReferenceType = v.ReferenceDetails.ReferenceType
            });
            var result = await query.ToPagedListAsync(request);
            return result;
        }
    }
}
