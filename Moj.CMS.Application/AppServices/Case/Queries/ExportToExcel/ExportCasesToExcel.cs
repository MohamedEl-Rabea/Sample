using MediatR;
using Moj.CMS.Application.AppServices.Case.Queries.GetAllCases;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Constants;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Services;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.ExportToExcel
{
    public class ExportCasesToExcel : Query<Result<ExportedFileInfo>>
    {
        public PagedRequest<CaseListItemDto> PagedRequest { get; set; }
    }

    public class ExportCasesToExcelHandler : IRequestHandler<ExportCasesToExcel, Result<ExportedFileInfo>>
    {
        private readonly ICaseQueries _caseQueries;
        private readonly IFileBuilder _fileBuilder;

        public ExportCasesToExcelHandler(ICaseQueries caseQueries, IFileBuilder excelService)
        {
            _caseQueries = caseQueries;
            _fileBuilder = excelService;
        }

        public async Task<Result<ExportedFileInfo>> Handle(ExportCasesToExcel query, CancellationToken cancellationToken)
        {
            var result = await _caseQueries.GetAllAsync(query.PagedRequest);

            var fileBytes = _fileBuilder.GenerateExcel(result.Data, ExcelEntitiesNames.Cases);
            ExportedFileInfo fileInfo = new ExportedFileInfo()
            {
                FileData = fileBytes,
                FileName = ExcelEntitiesNames.Cases
            };
            return Result<ExportedFileInfo>.Success(fileInfo);
        }
    }
}
