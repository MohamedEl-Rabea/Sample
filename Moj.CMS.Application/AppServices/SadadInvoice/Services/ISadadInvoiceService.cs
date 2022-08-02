using Moj.CMS.Application.AppServices.SadadInvoice.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.AppServices.SadadInvoice.Services
{

    [ScopedService]
    public interface ISadadInvoiceService
    {
        Task CreateSadadInvoiceAsync(IEnumerable<CreateSadadInvoiceDto> createSadadInvoiceDto);
    }
}
