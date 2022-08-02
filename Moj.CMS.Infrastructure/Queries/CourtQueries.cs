using Microsoft.EntityFrameworkCore;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Court;
using Moj.CMS.Domain.Aggregates.Court.Entities;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Queries
{
    public class CourtQueries : ICourtQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;

        public CourtQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator)
        {
            _queryBuilderCreator = queryBuilderCreator;
        }


        public async Task<List<Division>> GetDivisionsByCodeAsync(string[] divisionCodes)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var divisionList = await queryBuilder.Query<Court>()
                    .SelectMany(c => c.Divisions)
                    .Where(item => divisionCodes.Contains(item.Code))
                    .ToListAsync();
                return divisionList;
            }
        }

        public async Task<List<Court>> GetCourtsByCodeAsync(string[] courtCodes)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var courtList = await queryBuilder.Query<Court>()
                    .Where(item => courtCodes.Contains(item.Code))
                    .ToListAsync();
                return courtList;
            }
        }


        public async Task<string> GetDivisionCourtCodeAsync(string divisionCode)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var divisionCourtCode = await (from court in queryBuilder.Query<Court>()
                                               from division in court.Divisions
                                               where division.Code == divisionCode
                                               select court.Code).FirstOrDefaultAsync();
                return divisionCourtCode;
            }
        }

        public async Task<int> GetDivisionIdByCodeAsync(string divisionCode)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var divisionId = await queryBuilder.Query<Court>().SelectMany(c => c.Divisions)
                    .Where(division => division.Code == divisionCode)
                    .Select(division => division.Id)
                    .FirstOrDefaultAsync();
                return divisionId;
            }
        }

        public async Task<int> GetCourtIdByCodeAsync(string courtCode)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var courtId = await queryBuilder.Query<Court>()
                    .Where(item => item.Code == courtCode)
                    .Select(item => item.Id)
                    .FirstOrDefaultAsync();
                return courtId;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetDivisionListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<Court>().SelectMany(c => c.Divisions)
                    .Select(d => new SelectListItem
                    {
                        Key = d.Id,
                        Text = d.Name,
                        Code = d.Code
                    }).ToListAsync();
            }
        }
        public async Task<IEnumerable<SelectListItem>> GetDivisionListByCourtCodeAsync(string courtCode)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<Court>()
                    .Where(court => court.Code == courtCode)
                    .SelectMany(c => c.Divisions)
                    .Select(d => new SelectListItem
                    {
                        Key = d.Id,
                        Text = d.Name,
                        Code = d.Code
                    }).ToListAsync();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetCourtListAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                return await queryBuilder.Query<Court>().Select(c => new SelectListItem
                {
                    Key = c.Id,
                    Text = c.Name,
                    Code = c.Code
                }).ToListAsync();
            }
        }
    }
}
