using Moj.CMS.UserAccess.Application.DTO;
using System.Threading.Tasks;
using Moj.CMS.Shared.Wrapper;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.UserAccess.Application.Services.Interfaces
{
    [ScopedService]
    public interface IAuthenticationAppService
    {
        Task<IResult<string>> Login(LoginDto loginDto);
    }
}
