using Moj.CMS.Integration.ClientGenerator.Services.TahseelService;
using Moj.CMS.Integration.Contracts.ThirdParties.Tahseel.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.Contracts.ThirdParties.Tahseel
{
    public class TahseelClient : ITahseelClient
    {
        private readonly TahseelService _tahseelService;
        private readonly TahseelApiOptions _apiOptions;

        public TahseelClient(TahseelService tahseelService, TahseelApiOptions apiOptions)
        {
            _tahseelService = tahseelService;
            _apiOptions = apiOptions;
        }

        public Task<SadadInvoiceCreationResponse> CreateSadadInvoiceAsync(IEnumerable<SadadInvoiceCreationRequest> createInvoiceRequest)
        {
            var createdIvoices = createInvoiceRequest.Select(r => new CreatedSadadInvoice
            {
                InvoiceNumber = $"{r.InvoiceReferenceId}_InvoiceNumber",
                ReferenceNumber = r.InvoiceReferenceId
            }).ToList();
            return Task.FromResult(new SadadInvoiceCreationResponse { CreatedSadadInvoices = createdIvoices });
        }
    }
}
