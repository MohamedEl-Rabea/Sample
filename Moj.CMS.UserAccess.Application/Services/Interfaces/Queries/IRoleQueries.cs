using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.UserAccess.Application.Services.Interfaces.Queries
{
    [TransientService]
    public interface IRoleQueries
    {
        Task<IEnumerable<IdentityRole>> GetAllAsync();
    }
}
