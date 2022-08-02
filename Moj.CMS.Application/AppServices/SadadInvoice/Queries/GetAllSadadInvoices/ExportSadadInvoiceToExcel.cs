using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Constants;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Services;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.SadadInvoice.Queries.GetAllSadadInvoice
{
    public class ExportSadadInvoiceToExcel: Query<Result<ExportedFileInfo>>
    {
        public PagedRequest<SadadInvoiceDto> PagedRequest { get; set; }
    }
    public class ExportSadadInvoiceToExcelHandler : IRequestHandler<ExportSadadInvoiceToExcel, Result<ExportedFileInfo>>
    {
        private readonly ISadadInvoiceQueries _sadadInvoiceQueries;
        private readonly IFileBuilder _fileBuilder;
        public ExportSadadInvoiceToExcelHandler(ISadadInvoiceQueries sadadInvoiceQueries, IFileBuilder excelService)
        {
            _fileBuilder = excelService;
            _sadadInvoiceQueries = sadadInvoiceQueries;
        }

        public async Task<Result<ExportedFileInfo>> Handle(ExportSadadInvoiceToExcel request, CancellationToken cancellationToken)
        {
            var result = await _sadadInvoiceQueries.GetAllAsync(request.PagedRequest);

            var fileBytes = _fileBuilder.GenerateExcel(result.Data, ExcelEntitiesNames.SadadInvoice);
            ExportedFileInfo fileInfo = new ExportedFileInfo()
            {
                FileData = fileBytes,
                FileName = ExcelEntitiesNames.SadadInvoice
            };
            return Result<ExportedFileInfo>.Success(fileInfo);
        }
    }
}