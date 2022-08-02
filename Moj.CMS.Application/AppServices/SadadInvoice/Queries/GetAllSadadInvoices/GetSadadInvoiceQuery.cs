using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.SadadInvoice.Queries
{
    public class GetSadadInvoiceQuery : PagedQuery<SadadInvoiceDto>
    {

    }

    public class GetSadadInvoiceQueryHandler : IRequestHandler<GetSadadInvoiceQuery, PagedResult<SadadInvoiceDto>>
    {
        private readonly ISadadInvoiceQueries _sadadInvoiceQueries;
        public GetSadadInvoiceQueryHandler(ISadadInvoiceQueries sadadInvoiceQueries)
        {
            _sadadInvoiceQueries = sadadInvoiceQueries;
        }

        public async Task<PagedResult<SadadInvoiceDto>> Handle(GetSadadInvoiceQuery request, CancellationToken cancellationToken)
        {
            var SadadInvoices = await _sadadInvoiceQueries.GetAllAsync(request.PagedRequest);
            return SadadInvoices;
        }
    }
}
