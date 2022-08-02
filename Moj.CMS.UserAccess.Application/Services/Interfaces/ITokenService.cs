using Moj.CMS.UserAccess.Application.DTO;
using System.Threading.Tasks;
using Moj.CMS.Shared.Wrapper;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.UserAccess.Application.Services.Interfaces
{
    [ScopedService]
    public interface ITokenService
    {
        Task<Result<TokenResponseDto>> GenerateAsync(TokenRequestDto model);

        Task<Result<TokenResponseDto>> GetRefreshTokenAsync(RefreshTokenRequestDto model);

    }
}
