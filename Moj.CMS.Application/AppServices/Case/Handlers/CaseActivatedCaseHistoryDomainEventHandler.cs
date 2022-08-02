using Moj.CMS.Application.AppServices.CaseHistory.Handlers;
using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Shared.Values;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Handlers
{
    public class CaseActivatedCaseHistoryDomainEventHandler : CaseChangedDomainEventHandlerBase<CaseActivatedDomainEvent>
    {
        public CaseActivatedCaseHistoryDomainEventHandler(ICaseHistoryService caseHistoryService) : base(caseHistoryService)
        {
        }

        public override Task<LocalizedText> GetDetailsAsync(CaseActivatedDomainEvent caseActivatedDomainEvent)
        {
            var enMessage = $"Case with number {caseActivatedDomainEvent.CaseNumber} was reactivated .";

            var arMessage = $"تم اعادة تفعيل القضية رقم {caseActivatedDomainEvent.CaseNumber} حاله {caseActivatedDomainEvent.OldStatus}";

            return Task.FromResult(new LocalizedText(enMessage, arMessage));
        }
    }
}
