using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Shared.Constants.User;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;
using Moj.CMS.UserAccess.Application.Services.Interfaces.Queries;
using Moj.CMS.UserAccess.Infrastructure.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Infrastructure.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IQueryBuilderCreator<UserAccessDbContext> _queryBuilderCreator;
        private readonly IMapper _mapper;

        public UserQueries(IQueryBuilderCreator<UserAccessDbContext> queryBuilderCreator, IMapper mapper)
        {
            _queryBuilderCreator = queryBuilderCreator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var users = await queryBuilder.Query<CMSUser>()
                .Where(u => u.Id != UserConstants.SystemUserId)
                .OrderBy(u => u.CreationTime)
                .ThenBy(u => u.FirstName).Select(u => new UserDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    IsActive = u.IsActive,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    EmailConfirmed = u.EmailConfirmed,
                    CreationTime = u.CreationTime
                }).ToListAsync();
            var result = _mapper.Map<IEnumerable<UserDto>>(users);
            return result;
        }

        public async Task<UserDto> GetAsync(string userId)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var users = await queryBuilder.Query<CMSUser>().FirstOrDefaultAsync(u => u.Id == userId);
                var result = _mapper.Map<UserDto>(users);
                return result;
            }
        }
    }
}
