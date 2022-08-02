using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Application.AppServices.Promissory.Queries;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.Interfaces.Queries
{
    [TransientService]
    public interface IPromissoryQueries
    {
        Task<PagedResult<GetAllPromissoriesDto>> GetAllAsync(PagedRequest<GetAllPromissoriesDto> request);
        Task<IEnumerable<PromissoryBasicInfoDto>> GetPromissoriesBasicInfoByNumbers(IEnumerable<string> numberList);
        Task<IEnumerable<PromissoryBasicInfoDto>> GetPromissoriesBasicInfoByIds(IEnumerable<int> promissoriesIds);
        Task<PromissoryDto> GetPromissory(string promissoryNumber);
        Task<IEnumerable<PromissoryDto>> GetPromissoryListByNumber(IEnumerable<string> promissoryNumberList);
        Task<int> GetPromissoryId(string promissoryStampNumber);
    }
}
