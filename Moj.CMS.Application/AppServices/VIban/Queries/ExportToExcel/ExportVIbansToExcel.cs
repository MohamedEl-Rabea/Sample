using MediatR;
using Moj.CMS.Application.AppServices.VIban.Queries.GetAllVIbans;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Constants;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Services;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.VIban.Queries.ExportToExcel
{
    public class ExportVIbansToExcel : Query<Result<ExportedFileInfo>>
    {
        public PagedRequest<VIbanDto> PagedRequest { get; set; }
    }

    public class ExportVIbansToExcelHandler : IRequestHandler<ExportVIbansToExcel, Result<ExportedFileInfo>>
    {
        private readonly IVIbanQueries _vIbanQueries;
        private readonly IFileBuilder _fileBuilder;

        public ExportVIbansToExcelHandler(IVIbanQueries vIbanQueries, IFileBuilder excelService)
        {
            _vIbanQueries = vIbanQueries;
            _fileBuilder = excelService;
        }

        public async Task<Result<ExportedFileInfo>> Handle(ExportVIbansToExcel query, CancellationToken cancellationToken)
        {
            var result = await _vIbanQueries.GetAllAsync(query.PagedRequest);

            var fileBytes = _fileBuilder.GenerateExcel(result.Data, ExcelEntitiesNames.Cases);
            ExportedFileInfo fileInfo = new ExportedFileInfo
            {
                FileData = fileBytes,
                FileName = ExcelEntitiesNames.VIbans
            };
            return Result<ExportedFileInfo>.Success(fileInfo);
        }
    }
}

