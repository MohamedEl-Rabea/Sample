using Moj.CMS.Application.AppServices.Promissory.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.AppServices.Promissory.Services
{
    [ScopedService]
    public interface IPromissoryService
    {
        Task<IEnumerable<SavedPromissory>> AddPromissoryListAsync(IEnumerable<PromissoryDto> promissoryList);
    }
}
