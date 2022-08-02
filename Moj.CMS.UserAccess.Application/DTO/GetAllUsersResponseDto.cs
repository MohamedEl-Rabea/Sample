using System.Collections.Generic;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class GetAllUsersResponseDto
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}