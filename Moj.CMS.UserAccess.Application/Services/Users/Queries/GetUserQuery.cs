using AutoMapper;
using MediatR;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Interfaces.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Users.Queries
{
    public class GetUserQuery : Query<Result<UserDto>>
    {
        public string UserId { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserDto>>
    {
        private readonly IUserQueries _userQueries;
        private readonly IMapper _mapper;
        public GetUserQueryHandler( IMapper mapper, IUserQueries userQueries)
        {
            _mapper = mapper;
            _userQueries = userQueries;
        }
        public async Task<Result<UserDto>> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _userQueries.GetAsync(query.UserId);
            var result = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(result);
        }
    }
}
