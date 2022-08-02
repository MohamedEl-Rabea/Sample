using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Shared.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.CaseHistory.Handlers
{
    public class CaseCourtDetailsChangedDomainEventHandler : CaseChangedDomainEventHandlerBase<CaseCourtDetailsChangedDomainEvent>
    {
        private readonly ILookupsQueries _lookupsQueries;
        private readonly ICourtQueries _courtQueries;

        public CaseCourtDetailsChangedDomainEventHandler(ICaseHistoryService caseHistoryService, ILookupsQueries lookupsQueries,
            ICourtQueries courtQueries) : base(caseHistoryService)
        {
            _lookupsQueries = lookupsQueries;
            _courtQueries = courtQueries;
        }

        public async override Task<LocalizedText> GetDetailsAsync(CaseCourtDetailsChangedDomainEvent domainEvent)
        {
            var previousCaseDetails = domainEvent.PreviousCaseDetails;
            var caseAgg = (Domain.Aggregates.Case.Case)domainEvent.EventSource;
            var newCourtDetails = caseAgg.CaseDetails.FirstOrDefault(c => c.IsCurrent);

            var judgeNameList = await _lookupsQueries.GetJudgeNameByCodeAsync(new string[] { previousCaseDetails.JudgeCode, newCourtDetails.JudgeCode });
            var courtNameList = await _courtQueries.GetCourtsByCodeAsync(new string[] { previousCaseDetails.CourtCode, newCourtDetails.CourtCode });
            var divisionNameList = await _courtQueries.GetDivisionsByCodeAsync(new string[] { previousCaseDetails.DivisionCode, newCourtDetails.DivisionCode });

            var prevJudgeName = judgeNameList.First(j => j.Code == previousCaseDetails.JudgeCode);
            var newJudgeName = judgeNameList.First(j => j.Code == newCourtDetails.JudgeCode);
            var prevCourtName = courtNameList.First(j => j.Code == previousCaseDetails.CourtCode);
            var newCourtName = courtNameList.First(j => j.Code == newCourtDetails.CourtCode);
            var prevDivisionName = divisionNameList.First(j => j.Code == previousCaseDetails.DivisionCode);
            var newDivisionName = divisionNameList.First(j => j.Code == newCourtDetails.DivisionCode);

            var enMessage = $"Court details of the case with number - {caseAgg.CaseNumber} - changed from court - {prevCourtName} - to court - {newCourtName} - division - {prevDivisionName} - to division - {newDivisionName} - and judge - {prevJudgeName} - to judge - {newJudgeName} .";
            var arMessage = $" تم تغير تفاصيل المحكمة الخاصة بالقضية رقم - {caseAgg.CaseNumber} - من محكمة - {prevCourtName} - إلى محكمة - {newCourtName} - ومن دائرة - {prevDivisionName} - إلى دائرة - {newDivisionName} - ومن قاضى - {prevJudgeName} - إلى قاضى - {newJudgeName} .";
            return new LocalizedText(enMessage, arMessage);
        }
    }
}
