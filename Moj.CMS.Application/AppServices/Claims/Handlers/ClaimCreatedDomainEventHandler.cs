using Moj.CMS.Application.AppServices.CaseHistory.Handlers;
using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class ClaimCreatedDomainEventHandler : CaseChangedDomainEventHandlerBase<ClaimCreatedDomainEvent>
    {
        private readonly IPartyQueries _partyQueries;
        public ClaimCreatedDomainEventHandler(ICaseHistoryService caseHistoryService, IPartyQueries partyQueries) : base(caseHistoryService)
        {
            _partyQueries = partyQueries;
        }

        public override async Task<LocalizedText> GetDetailsAsync(ClaimCreatedDomainEvent domainEvent)
        {
            var source = domainEvent.EventSource as Claim;
            var partiesBasicInfo = await _partyQueries.GetPartiesBasicInfoByNumbersAsync(domainEvent.ClaimDetailsList.Select(ac => ac.PartyNumber));
            var partyBasicInfo = (await _partyQueries.GetPartiesBasicInfoByNumbersAsync(new List<string> { source.ComplaintPartyNumber }))?.FirstOrDefault();
            var enMessage = $"Financial Claim {source.Id}  for complaint Party  {partyBasicInfo.Name}  " +
                $"with required amount {source.TotalRequiredAmount} is created .{Environment.NewLine}" +
                $" - Accused Parties : {Environment.NewLine} [{GetAccusedPartiesInfo(domainEvent.ClaimDetailsList, partiesBasicInfo, false)}]";

            var arMessage = $" تم إنشاء مطالبة مالية رقم {source.Id} للدائن  {partyBasicInfo.Name} بقيمة مطلوبة" +
                $"  {source.TotalRequiredAmount}  {Environment.NewLine}  - المدينين : {Environment.NewLine}" +
                $"  [{GetAccusedPartiesInfo(domainEvent.ClaimDetailsList, partiesBasicInfo, true)}]";
            return new LocalizedText(enMessage, arMessage);
        }

        private static string GetAccusedPartiesInfo(IEnumerable<ClaimDetails> AccusedPartyList, IEnumerable<PartyBasicInfoDto> partiesBasicInfo, bool isArabic)
        {
            var nameLabel = isArabic ? "الاسم: " : "Name: ";
            var requiredAmountLabel = isArabic ? "الحق المطلوب: " : "required amount: ";

            return string.Join($",{Environment.NewLine} ", AccusedPartyList.Select(a => $"{nameLabel + partiesBasicInfo.First(p => p.Number == a.PartyNumber).Name} - {requiredAmountLabel + a.RequiredAmount }"));
        }
    }
}
