using Moj.CMS.Domain.Aggregates.CaseHistory;
using Moj.CMS.Domain.ParameterObjects.CaseHistory;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.CaseHistory.Services
{
    public class CaseHistoryService : ICaseHistoryService
    {
        private readonly ICaseHistoryRepository _caseHistoryRepository;
        public CaseHistoryService(ICaseHistoryRepository caseHistoryRepository)
        {
            _caseHistoryRepository = caseHistoryRepository;
        }

        public async Task AddCaseLogEntryAsync(CreateCaseHistoryParam caseHistoryParam)
        {
            var caseHistoryRecord = await Domain.Aggregates.CaseHistory.CaseHistory.CreateHistoryRecordAsync(caseHistoryParam);
            await _caseHistoryRepository.AddAsync(caseHistoryRecord);
        }
    }
}
