using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Constants;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Services;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Promissory.Queries
{
    public class ExportPromissoriesToExcel : Query<Result<ExportedFileInfo>>
    {
        public PagedRequest<GetAllPromissoriesDto> PagedRequest { get; set; }
    }
    public class ExportPromissoriesToExcelHandler : IRequestHandler<ExportPromissoriesToExcel, Result<ExportedFileInfo>>
    {
        private readonly IPromissoryQueries _PromissoryQueries;
        private readonly IFileBuilder _fileBuilder;

        public ExportPromissoriesToExcelHandler(IPromissoryQueries PromissoryQueries, IFileBuilder excelService)
        {
            _PromissoryQueries = PromissoryQueries;
            _fileBuilder = excelService;
        }

        public async Task<Result<ExportedFileInfo>> Handle(ExportPromissoriesToExcel query, CancellationToken cancellationToken)
        {
            var result = await _PromissoryQueries.GetAllAsync(query.PagedRequest);

            var fileBytes = _fileBuilder.GenerateExcel(result.Data, ExcelEntitiesNames.Promissories);
            ExportedFileInfo fileInfo = new ExportedFileInfo()
            {
                FileData = fileBytes,
                FileName = ExcelEntitiesNames.Promissories
            };
            return Result<ExportedFileInfo>.Success(fileInfo);
        }
    }
}
