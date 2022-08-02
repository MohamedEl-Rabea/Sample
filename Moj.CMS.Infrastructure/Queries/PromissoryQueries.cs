using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Application.AppServices.Promissory.Queries;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Application.Models;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Promissory;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Queries
{
    public class PromissoryQueries : IPromissoryQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;
        private readonly IMapper _mapper;

        public PromissoryQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator, IMapper mapper)

        {
            _queryBuilderCreator = queryBuilderCreator;
            _mapper = mapper;
        }

        public async Task<PromissoryDto> GetPromissory(string promissoryNumber)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var result = await queryBuilder.Query<Promissory>()
                                    .Where(p => p.Number == promissoryNumber)
                                    .Select(promissory => new PromissoryDto
                                    {
                                        PromissoryNumber = promissory.Number,
                                        PromissoryTypeId = promissory.TypeId,
                                        PromissoryIssueDate = promissory.PromissoryIssueDate
                                    }).SingleOrDefaultAsync();

                return result;
            }
        }

        public async Task<IEnumerable<PromissoryBasicInfoDto>> GetPromissoriesBasicInfoByIds(IEnumerable<int> promissoriesIds)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var result = await queryBuilder.Query<Promissory>()
                                    .Where(p => promissoriesIds.Contains(p.Id))
                                    .Select(promissory => new PromissoryBasicInfoDto
                                    {
                                        Id = promissory.Id,
                                        Number = promissory.Number
                                    }).ToListAsync();

                return result;
            }
        }

        public async Task<IEnumerable<PromissoryBasicInfoDto>> GetPromissoriesBasicInfoByNumbers(IEnumerable<string> numberList)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var result = (from promissory in queryBuilder.Query<Promissory>()
                              join type in queryBuilder.Query<PromissoryType>() on (int)promissory.TypeId equals type.Id
                              where numberList.Contains(promissory.Number)
                              select new PromissoryBasicInfoDto
                              {
                                  Id = promissory.Id,
                                  Number = promissory.Number,
                                  TypeName = type.Name
                              }).ToListAsync();
                return await result;
            }
        }

        public async Task<IEnumerable<PromissoryDto>> GetPromissoryListByNumber(IEnumerable<string> promissoryNumberList)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var promissoryList = await queryBuilder.Query<Promissory>()
                .Where(p => promissoryNumberList.Contains(p.Number))
                .Select(p => new PromissoryDto
                {
                    PromissoryNumber = p.Number,
                    PromissoryIssueDate = p.PromissoryIssueDate,
                    PromissoryTypeId = p.TypeId
                }).ToListAsync();

            return promissoryList;
        }

        public async Task<int> GetPromissoryId(string promissoryNumber)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var result = await queryBuilder.Query<Promissory>()
                                    .Where(p => p.Number == promissoryNumber)
                                    .Select(promissory => promissory.Id).SingleOrDefaultAsync();

                return result;
            }
        }

        public async Task<PagedResult<GetAllPromissoriesDto>> GetAllAsync(PagedRequest<GetAllPromissoriesDto> request)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var allPromissories = await (from promissory in queryBuilder.Query<Promissory>()
                                             join casePromissory in queryBuilder.Query<Case>().SelectMany(c => c.CasePromissories)
                                             on promissory.Number equals casePromissory.PromissoryNumber
                                             join user in queryBuilder.Query<User>() on promissory.LastModifierUserId ?? promissory.CreatorUserId equals user.Id
                                             join promissoryType in queryBuilder.Query<PromissoryType>()
                                             on (int)promissory.TypeId equals promissoryType.Id
                                             group promissory by new
                                             {
                                                 promissory.Number,
                                                 promissory.TypeId,
                                                 IssueDate = promissory.PromissoryIssueDate,
                                                 typeName = promissoryType.Name,
                                                 IsUpdate = promissory.LastModificationTime.HasValue,
                                                 LastUpdate = promissory.LastModificationTime ?? promissory.CreationTime,
                                                 UpdatedBy = user.Name,
                                             }
                           into promissoryGroup
                                             select new GetAllPromissoriesDto
                                             {
                                                 PromissoryType = promissoryGroup.Key.typeName,
                                                 TypeId = promissoryGroup.Key.TypeId,
                                                 PromissoryNumber = promissoryGroup.Key.Number,
                                                 PromissoryDate = promissoryGroup.Key.IssueDate,
                                                 IsUpdate = promissoryGroup.Key.IsUpdate,
                                                 LastUpdate = promissoryGroup.Key.LastUpdate,
                                                 UpdatedBy = promissoryGroup.Key.UpdatedBy,
                                                 NumberOfCases = promissoryGroup.Count()
                                             }).ToPagedListAsync(request);
                await SetPartiesCountAsync(allPromissories, queryBuilder);
                await SetClaimsCountAsync(allPromissories, queryBuilder);
                
                return allPromissories;
            }
        }

        #region Helper Methods
        /// <summary>
        /// this method is for adding the corresponding PartiesCount for each
        ///  promissory of the provided promissories list
        /// </summary>
        /// <param name="allPrmoissiries"></param>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        private static async Task SetPartiesCountAsync(PagedResult<GetAllPromissoriesDto> allPrmoissiries, IQueryBuilder queryBuilder)
        {
            var promissoryNumbers = allPrmoissiries.Data.Select(p => p.PromissoryNumber);
            var query = await (from caseAgg in queryBuilder.Query<Case>()
                               from party in caseAgg.CaseParties
                               where promissoryNumbers.Contains(party.PromissoryNumber)
                               select new
                               {
                                   party.PromissoryNumber
                               }).ToListAsync();
            var partiesCountResult = query.GroupBy(q => new { q.PromissoryNumber })
                .Select(qr => new
                {
                    qr.Key.PromissoryNumber,
                    PartiesCount = qr.Count()
                });

            foreach (var promossiory in allPrmoissiries.Data)
            {
                promossiory.NumberOfParties = partiesCountResult.Single(c => c.PromissoryNumber == promossiory.PromissoryNumber)?.PartiesCount ?? 0;
            }
        }
        /// <summary>
        /// this method is for adding the corresponding ClaimsCount for each
        ///  promissory of the provided promissories list
        /// </summary>
        /// <param name="allPrmoissiries"></param>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        private static async Task SetClaimsCountAsync(PagedResult<GetAllPromissoriesDto> allPrmoissiries, IQueryBuilder queryBuilder)
        {
            var promissoryNumbers = allPrmoissiries.Data.Select(p => p.PromissoryNumber);
            var query = await (from claim in queryBuilder.Query<Claim>()
                               where promissoryNumbers.Contains(claim.PromissoryNumber)
                               select new
                               {
                                   claim.PromissoryNumber
                               }).ToListAsync();
            var claimsCountResult = query.GroupBy(q => new { q.PromissoryNumber })
                .Select(qr => new
                {
                    qr.Key.PromissoryNumber,
                    ClaimsCount = qr.Count()
                });

            foreach (var promossiory in allPrmoissiries.Data)
            {
                promossiory.NumberOfClaims = claimsCountResult.Single(c => c.PromissoryNumber == promossiory.PromissoryNumber)?.ClaimsCount ?? 0;
            }
        } 
        #endregion
    }
}