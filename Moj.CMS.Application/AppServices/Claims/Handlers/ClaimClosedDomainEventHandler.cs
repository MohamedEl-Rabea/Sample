using Moj.CMS.Application.AppServices.CaseHistory.Handlers;
using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Shared.Values;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class ClaimClosedDomainEventHandler : CaseChangedDomainEventHandlerBase<ClaimClosedDomainEvent>
    {
        private readonly IPartyQueries _partyQueries;
        public ClaimClosedDomainEventHandler(ICaseHistoryService caseHistoryService, IPartyQueries partyQueries) : base(caseHistoryService)
        {
            _partyQueries = partyQueries;
        }

        public override async Task<LocalizedText> GetDetailsAsync(ClaimClosedDomainEvent domainEvent)
        {
            var source = domainEvent.EventSource as Claim;
            var partyBasicInfo =
                (await _partyQueries.GetPartiesBasicInfoByNumbersAsync(new List<string> { source.ComplaintPartyNumber }))
                ?.FirstOrDefault();

            var enMessage =
                $"Financial Claim {source.Id}  for complaint Party  {partyBasicInfo.Name}  with required amount {source.TotalRequiredAmount} is Closed .";
            var arMessage =
                $" تم إغلاق مطالبة مالية رقم {source.Id} للدائن  {partyBasicInfo.Name} بقيمة مطلوبة  {source.TotalRequiredAmount}";
            return new LocalizedText(enMessage, arMessage);
        }
    }
}