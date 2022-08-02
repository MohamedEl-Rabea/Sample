using Moj.CMS.Application.AppServices.VIban.Dtos;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.AppServices.VIban.Services
{
    [ScopedService]
    public interface IVIbanService
    {
        Task<string> CreateVIbanAsync(CreateVIbanDto createVIbanDto);
    }
}
