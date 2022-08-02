using Moj.CMS.Application.AppServices.CaseHistory.Handlers;
using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.Events;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.Shared.Values;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class AccusedPartiesAddedToClaimDomainEventHandler : CaseChangedDomainEventHandlerBase<AccusedPartiesAddedToClaimDomainEvent>
    {
        private readonly IPartyQueries _partyQueries;
        public AccusedPartiesAddedToClaimDomainEventHandler(ICaseHistoryService caseHistoryService, IPartyQueries partyQueries) : base(caseHistoryService)
        {
            _partyQueries = partyQueries;
        }

        public override async Task<LocalizedText> GetDetailsAsync(AccusedPartiesAddedToClaimDomainEvent domainEvent)
        {
            var source = domainEvent.EventSource as Claim;
            var partiesBasicInfo = await _partyQueries.GetPartiesBasicInfoByNumbersAsync(source.ClaimDetailsList.Select(ac => ac.PartyNumber));
            var enMessage = $"Accused Parties  [{GetAccusedPartiesInfo(domainEvent.ClaimDetailsList, partiesBasicInfo, false)}]  are added to claim number {source.Id} .";
            var arMessage = $" تم إضافة مدينين [{GetAccusedPartiesInfo(domainEvent.ClaimDetailsList, partiesBasicInfo, true)}]  إلى المطالبة رقم  {source.Id} .";
            return (new LocalizedText(enMessage, arMessage));
        }

        private static string GetAccusedPartiesInfo(IEnumerable<ClaimDetails> AccusedPartyList, IEnumerable<PartyBasicInfoDto> partiesBasicInfo, bool isArabic)
        {
            var nameLabel = isArabic ? "الاسم: " : "Name: ";
            var requiredAmountLabel = isArabic ? "الحق المطلوب: " : "required amount: ";

            return string.Join(',', AccusedPartyList.Select(a => $"{nameLabel + partiesBasicInfo.First(p => p.Number == a.PartyNumber).Name} " +
            $"- {requiredAmountLabel + a.RequiredAmount } "));
        }
    }
}
