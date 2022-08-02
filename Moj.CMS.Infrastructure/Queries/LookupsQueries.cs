using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Queries
{
    public class LookupsQueries : ILookupsQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;
        private readonly IMapper _mapper;

        public LookupsQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator, IMapper mapper)
        {
            _queryBuilderCreator = queryBuilderCreator;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SelectListItem>> GetPartiesRolesItemsNamesAsync(IEnumerable<int> itemsIds)
        {

            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var itemsValues = await queryBuilder.Query<PartyRole>()
                    .Where(item => itemsIds.Contains(item.Id))
                    .Select(item => new SelectListItem { Key = item.Id, Text = item.Name })
                    .ToListAsync();
                return itemsValues;
            }
        }
        public async Task<string> GetLookupItemNameAsync(int itemId)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var itemValue = await queryBuilder.Query<LookupBase>()
                    .Where(item => item.Id == itemId)
                    .Select(item => item.Name)
                    .FirstOrDefaultAsync();
                return itemValue;
            }
        }

        public async Task<int> GetJudgeIdByCodeAsync(string judgeCode)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var judgeId = await queryBuilder.Query<Judge>()
                    .Where(item => item.Code == judgeCode)
                    .Select(item => item.Id)
                    .FirstOrDefaultAsync();
                return judgeId;
            }
        }

        public async Task<IEnumerable<CaseStatus>> GetCaseStatusListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<CaseStatus>().ToListAsync();
            }
        }

        public async Task<IEnumerable<VIbanReferenceType>> GetVIbanReferenceTypeListAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            return await queryBuilder.Query<VIbanReferenceType>().ToListAsync();
        }

        public async Task<IEnumerable<CaseOperation>> GetCaseOperationListAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            return await queryBuilder.Query<CaseOperation>().ToListAsync();
        }

        public async Task<IEnumerable<CaseType>> GetCaseTypeListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<CaseType>().ToListAsync();
            }
        }

        public async Task<IEnumerable<Judge>> GetJudgeListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<Judge>().ToListAsync();
            }
        }

        public async Task<IEnumerable<PromissoryType>> GetPromissoryTypeListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<PromissoryType>().ToListAsync();
            }
        }

        public async Task<IEnumerable<DebtType>> GetDebtTypeListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<DebtType>().ToListAsync();
            }
        }

        public async Task<IEnumerable<ClaimFinancialStatus>> GetClaimFinancialStatusListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<ClaimFinancialStatus>().ToListAsync();
            }
        }
        public async Task<IEnumerable<ClaimStatus>> GetClaimStatusListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<ClaimStatus>().ToListAsync();
            }
        }

        public async Task<IEnumerable<PartyIdentityType>> GetPartyIdentityTypeListAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            return await queryBuilder.Query<PartyIdentityType>().ToListAsync();
        }

        public async Task<IEnumerable<PartyLocation>> GetPartyLocationListAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            return await queryBuilder.Query<PartyLocation>().ToListAsync();
        }

        public async Task<IEnumerable<Area>> GetAreaListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<Area>().ToListAsync();
            }
        }

        public async Task<IEnumerable<PartyType>> GetPartyTypeListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<PartyType>().ToListAsync();
            }
        }
        public async Task<IEnumerable<Nationality>> GetNationalityList()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<Nationality>().ToListAsync();
            }
        }

        public async Task<IEnumerable<PartyRole>> GetPartyRoleListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<PartyRole>().ToListAsync();
            }
        }

        public async Task<List<Judge>> GetJudgeNameByCodeAsync(string[] judgeCodes)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var judgeList = await queryBuilder.Query<Judge>()
                    .Where(item => judgeCodes.Contains(item.Code))
                    .ToListAsync();
                return judgeList;
            }
        }

        public async Task<IEnumerable<IbanPurpose>> GetIbanPurposeListAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            return await queryBuilder.Query<IbanPurpose>().ToListAsync();
        }
    }
}
