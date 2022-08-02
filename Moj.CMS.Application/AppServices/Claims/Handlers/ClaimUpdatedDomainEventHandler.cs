using Moj.CMS.Application.AppServices.CaseHistory.Handlers;
using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.Events;
using Moj.CMS.Domain.Shared.Values;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class ClaimUpdatedDomainEventHandler : CaseChangedDomainEventHandlerBase<ClaimUpdatedDomainEvent>
    {
        private readonly IPartyQueries _partyQueries;
        public ClaimUpdatedDomainEventHandler(ICaseHistoryService caseHistoryService, IPartyQueries partyQueries) : base(caseHistoryService)
        {
            _partyQueries = partyQueries;
        }

        public override async Task<LocalizedText> GetDetailsAsync(ClaimUpdatedDomainEvent domainEvent)
        {
            var source = domainEvent.EventSource as Claim;
            var partyBasicInfo = (await _partyQueries.GetPartiesBasicInfoByNumbersAsync(new List<string> { source.ComplaintPartyNumber }))?.FirstOrDefault();
            var enMessage = $"Financial Claim number {source.Id} with required amount {domainEvent.OldTotalRequiredAmount} for complaint Party {partyBasicInfo.Name} amount is update to be {source.TotalRequiredAmount}.";
            var arMessage = $" تم تحديث الحق المطلوب للمطالبة رقم  {source.Id} للدائن  {partyBasicInfo.Name}  من قيمة  {domainEvent.OldTotalRequiredAmount}  لتصبح  {source.TotalRequiredAmount}.";
            return (new LocalizedText(enMessage, arMessage));
        }
    }
}
