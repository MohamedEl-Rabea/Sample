using Moj.CMS.UserAccess.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.UserAccess.Application.Services.Interfaces.Queries
{
    [TransientService]
    public interface IUserQueries
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
       Task<UserDto> GetAsync(string userId);
    }
}
