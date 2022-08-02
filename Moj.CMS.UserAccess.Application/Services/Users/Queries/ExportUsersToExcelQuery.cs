using MediatR;
using Moj.CMS.Shared.Constants;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Services;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Interfaces.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Users.Queries
{
    public class ExportUsersToExcelQuery : Query<Result<ExportedFileInfo>>
    {
        public PagedRequest<UserDto> PagedRequest { get; set; }
    }

    public class ExportUsersToExcelHandler : IRequestHandler<ExportUsersToExcelQuery, Result<ExportedFileInfo>>
    {
        private readonly IFileBuilder _fileBuilder;
        private readonly IUserQueries _userQueries;
        public ExportUsersToExcelHandler(IFileBuilder excelService, IUserQueries userQueries)
        {
            _fileBuilder = excelService;
            _userQueries = userQueries;
        }
        public async Task<Result<ExportedFileInfo>> Handle(ExportUsersToExcelQuery request, CancellationToken cancellationToken)
        {
            var result = await _userQueries.GetAllAsync();
            var file = _fileBuilder.GenerateExcel(result, ExcelEntitiesNames.Users);
            ExportedFileInfo exportedFileInfo = new ExportedFileInfo()
            {
                FileData = file,
                FileName = ExcelEntitiesNames.Users
            };
            return Result<ExportedFileInfo>.Success(exportedFileInfo);
        }
    }

}
