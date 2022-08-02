using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Models.Audit;
using Moj.CMS.Shared.Queries;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System.Linq;
using System.Threading.Tasks;
using User = Moj.CMS.Application.Models.User;

namespace Moj.CMS.Infrastructure.Queries
{
    public class LogsQueries : ILogsQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;

        private readonly IMapper _mapper;
        public LogsQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator, IMapper mapper)
        {
            _queryBuilderCreator = queryBuilderCreator;
            _mapper = mapper;
        }

        public async Task<PagedResult<Log>> GetAllAsync(PagedRequest<Log> request)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var logs = await queryBuilder.Query<Log>().ToPagedListAsync(request);
                var result = _mapper.Map<PagedResult<Log>>(logs);
                return result;
            }
        }

        public async Task<PagedResult<EntityHistoryDto>> GetEntitiesHistoryAsync(PagedRequest<EntityHistoryDto> request)
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var history = await (from audit in queryBuilder.Query<Audit>()
                                 join user in queryBuilder.Query<User>() on audit.UserId equals user.Id
                                 select new EntityHistoryDto
                                 {
                                     Id = audit.Id,
                                     RequestId = audit.RequestId.ToString(),
                                     RequestName = audit.RequestName,
                                     EntityId = audit.EntityId,
                                     EntityName = audit.EntityName,
                                     OperationType = audit.Type,
                                     CreationDate = audit.DateTime,
                                     UserName = user.Name,
                                     NewValues = audit.NewValues,
                                     OldValues = audit.OldValues
                                 }).ToPagedListAsync(request);

            return history;
        }

        public async Task<EntityHistoryDto> GetEntityHistoryByRequestId(string requestId)
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var history = await queryBuilder.Query<Audit>()
                .Where(a => a.RequestId.ToString() == requestId)
                .Select(a => new EntityHistoryDto
                {
                    Id = a.Id,
                    RequestId = a.RequestId.ToString(),
                    RequestName = a.RequestName,
                    EntityId = a.EntityId,
                    EntityName = a.EntityName,
                    OperationType = a.Type,
                    CreationDate = a.DateTime,
                    OldValues = a.OldValues,
                    NewValues = a.NewValues
                }).FirstOrDefaultAsync();

            return history;
        }
    }
}
