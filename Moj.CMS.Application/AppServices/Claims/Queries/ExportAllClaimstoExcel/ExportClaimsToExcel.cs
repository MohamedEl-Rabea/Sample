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

namespace Moj.CMS.Application.AppServices.Claims.Queries
{
    public class ExportClaimsToExcel : Query<Result<ExportedFileInfo>>
    {
        public PagedRequest<GetAllClaimsDto> PagedRequest { get; set; }
    }
    public class ExportClaimsToExcelHandler : IRequestHandler<ExportClaimsToExcel, Result<ExportedFileInfo>>
    {
        private readonly IClaimQueries _ClaimQueries;
        private readonly IFileBuilder _fileBuilder;

        public ExportClaimsToExcelHandler(IClaimQueries ClaimQueries, IFileBuilder excelService)
        {
            _ClaimQueries = ClaimQueries;
            _fileBuilder = excelService;
        }

        public async Task<Result<ExportedFileInfo>> Handle(ExportClaimsToExcel query, CancellationToken cancellationToken)
        {
            var result = await _ClaimQueries.GetAllAsync(query.PagedRequest);

            var fileBytes = _fileBuilder.GenerateExcel(result.Data, ExcelEntitiesNames.Claims);
            ExportedFileInfo fileInfo = new ExportedFileInfo()
            {
                FileData = fileBytes,
                FileName = ExcelEntitiesNames.Claims
            };
            return Result<ExportedFileInfo>.Success(fileInfo);
        }
    }
}
