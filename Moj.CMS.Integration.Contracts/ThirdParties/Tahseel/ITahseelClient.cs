using Moj.CMS.Integration.Contracts.ThirdParties.Tahseel.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.Contracts.ThirdParties.Tahseel
{
    public interface ITahseelClient
    {
        Task<SadadInvoiceCreationResponse> CreateSadadInvoiceAsync(IEnumerable<SadadInvoiceCreationRequest> createInvoiceRequest);
    }
}
