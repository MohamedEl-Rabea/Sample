using AutoMapper;
using MediatR;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Interfaces.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Roles.Queries
{
    public class GetAllRolesQuery : Query<Result<IEnumerable<RoleDto>>>
    {
    }

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<RoleDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleQueries _roleQueries;
        public GetAllRolesQueryHandler( IMapper mapper, IRoleQueries roleQueries)
        {
            _mapper = mapper;
            _roleQueries = roleQueries;
        }
        public async Task<Result<IEnumerable<RoleDto>>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = await _roleQueries.GetAllAsync();
            var rolesResponse = _mapper.Map<List<RoleDto>>(roles);
            return Result<IEnumerable<RoleDto>>.Success(rolesResponse);
        }
    }
}
