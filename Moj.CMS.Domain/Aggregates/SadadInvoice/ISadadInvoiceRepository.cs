using Moj.CMS.Domain.Shared.Repositories;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.Aggregates.SadadInvoice
{
    [ScopedService]
    public interface ISadadInvoiceRepository : IDomainRepository<SadadInvoice>
    {
    }
}
