using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using System.Threading;
using System.Threading.Tasks;
using Moj.CMS.Shared.Constants;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Services;
using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class ExportPartyToExcel : Query<ExportedFileInfo>
    {
        public PagedRequest<PartyListItemDto> PagedRequest { get; set; }

    }

    public class ExportCasesToExcelHandler : IRequestHandler<ExportPartyToExcel, ExportedFileInfo>
    {
        private readonly IFileBuilder _fileBuilder;
        private readonly IPartyQueries partyQueries;

        public ExportCasesToExcelHandler(IFileBuilder excelService, IPartyQueries partyQueries)
        {
            _fileBuilder = excelService;
            this.partyQueries = partyQueries;
        }

        public async Task<ExportedFileInfo> Handle(ExportPartyToExcel request, CancellationToken cancellationToken)
        {
            var result = await partyQueries.GetAllPartiesAsync(request.PagedRequest);

            var fileBytes = _fileBuilder.GenerateExcel(result.Data, ExcelEntitiesNames.Parties);
            ExportedFileInfo fileInfo = new ExportedFileInfo()
            {
                FileData = fileBytes,
                FileName = ExcelEntitiesNames.Parties
            };
            return fileInfo;
        }
    }
}
