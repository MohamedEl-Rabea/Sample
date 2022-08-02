using System.Collections.Generic;

namespace Moj.CMS.Application.Jobs.CreateSadadInvoice
{
    public class CreateSadadInvoiceBackgroundJobArgs
    {
        public List<CreateSadadInvoiceJobInput> CreateSadadInvoiceJobInputs { get; set; }
    }
}
