using Moj.CMS.Application.AppServices.SadadInvoice.Queries;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.Interfaces.Queries
{
    [ScopedService]
    public interface ISadadInvoiceQueries
    {
        Task<PagedResult<SadadInvoiceDto>> GetAllAsync(PagedRequest<SadadInvoiceDto> request);
    }
}
