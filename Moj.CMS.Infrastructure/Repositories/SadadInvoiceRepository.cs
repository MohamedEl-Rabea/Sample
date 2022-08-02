using Moj.CMS.Domain.Aggregates.SadadInvoice;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure.Repositories;

namespace Moj.CMS.Infrastructure.Repositories
{
    public class SadadInvoiceRepository : BaseDomainRepository<SadadInvoice>, ISadadInvoiceRepository
    {
        public SadadInvoiceRepository(IRepository<SadadInvoice> genericRepository) : base(genericRepository)
        {
        }
    }
}
