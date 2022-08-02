using MediatR;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents;
using Moj.CMS.Shared.Constants;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Services;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.CaseHistory.Queries
{
    public class ExportCaseEventToExcel : Query<Result<ExportedFileInfo>>
    {
        public IEnumerable<CaseEventsDto> CaseEvents { get; set; }
    }

    public class ExportCaseEventToExcelHandler : IRequestHandler<ExportCaseEventToExcel, Result<ExportedFileInfo>>
    {
        private readonly IFileBuilder _fileBuilder;

        public ExportCaseEventToExcelHandler(IFileBuilder fileBuilder)
        {
            _fileBuilder = fileBuilder;
        }
        public Task<Result<ExportedFileInfo>> Handle(ExportCaseEventToExcel request, CancellationToken cancellationToken)
        {
            var fileBytes = _fileBuilder.GenerateExcel(request.CaseEvents, ExcelEntitiesNames.CaseEvents);

            ExportedFileInfo fileInfo = new ExportedFileInfo
            {
                FileData = fileBytes,
                FileName = ExcelEntitiesNames.CaseEvents
            };
            return Task.FromResult(Result<ExportedFileInfo>.Success(fileInfo));
        }
    }
}