using MediatR;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Interfaces.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Users.Queries
{

    public class GetAllUsersQuery : Query<Result<IEnumerable<UserDto>>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserDto>>>
    {
        private readonly IUserQueries _userQueries;
        public GetAllUsersQueryHandler(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }
        public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _userQueries.GetAllAsync();
            return Result<IEnumerable<UserDto>>.Success(result);
        }
    }
}
