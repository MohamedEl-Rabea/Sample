using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.UserAccess.Application.Services.Interfaces.Queries;
using Moj.CMS.UserAccess.Infrastructure.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Infrastructure.Queries
{
    public class RoleQueries : IRoleQueries
    {
        private readonly IQueryBuilderCreator<UserAccessDbContext> _queryBuilderCreator;

        public RoleQueries(IQueryBuilderCreator<UserAccessDbContext> queryBuilderCreator)
        {
            _queryBuilderCreator = queryBuilderCreator;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var roles = await queryBuilder.Query<IdentityRole>().ToListAsync();
                return roles;
            }
        }
    }
}
