using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.CaseHistory.Handlers
{
    public class CasePartyCreatedDomainEventHandler : CaseChangedDomainEventHandlerBase<CasePartyCreatedDomainEvent>
    {
        private readonly IPartyQueries _partyQueries;
        private readonly IPromissoryQueries _promissoryQueries;
        private readonly ILookupsService _lookupsService;
        public CasePartyCreatedDomainEventHandler(ICaseHistoryService caseHistoryService, IPartyQueries partyQueries,
            IPromissoryQueries promissoryQueries,
            ILookupsService lookupsService) : base(caseHistoryService)
        {
            _partyQueries = partyQueries;
            _promissoryQueries = promissoryQueries;
            _lookupsService = lookupsService;
        }

        public override async Task<LocalizedText> GetDetailsAsync(CasePartyCreatedDomainEvent casePartyCreatedDomainEvent)
        {
            var caseParties = casePartyCreatedDomainEvent.CaseParties;

            var partiesNumbers = caseParties.Select(c => c.PartyNumber);
            var partiesBasicInfo = await _partyQueries.GetPartiesBasicInfoByNumbersAsync(partiesNumbers);

            var promissoriesNumbers = caseParties.Select(c => c.PromissoryNumber);
            var promissoriesInfo = await _promissoryQueries.GetPromissoriesBasicInfoByNumbers(promissoriesNumbers);

            var partiesRolesIds = caseParties.Select(c => (int)c.PartyRoleId);
            var partiesRoles = await _lookupsService.GetPartiesRolesItemsNamesAsync(partiesRolesIds);

            var enMessage = $"Parties with the following info [{GetAddedPartiesInfo(caseParties, partiesBasicInfo, promissoriesInfo, partiesRoles, isArabic: false)}].";
            var arMessage = $"تم اضافة المستفيد/المستفيدين [{GetAddedPartiesInfo(caseParties, partiesBasicInfo, promissoriesInfo, partiesRoles, isArabic: true)}].";
            return new LocalizedText(enMessage, arMessage);
        }

        private static string GetAddedPartiesInfo(IEnumerable<CaseParty> caseParties, IEnumerable<PartyBasicInfoDto> partiesBasicInfo,
            IEnumerable<PromissoryBasicInfoDto> promissoriesInfo, IEnumerable<SelectListItem> partiesRoles, bool isArabic)
        {
            var nameLabel = isArabic ? "الاسم: " : "Name: ";
            var roleLabel = isArabic ? "الدور: " : "Role: ";
            var promissoryNumberLabel = isArabic ? "رقم السند: " : "Promissory number: ";

            return string.Join(',', caseParties.Select(cp => $"{nameLabel + partiesBasicInfo.First(p => p.Number == cp.PartyNumber).Name} " +
            $"- {roleLabel}({partiesRoles.First(r => r.Key == (int)cp.PartyRoleId).Text}) " +
            $"- {promissoryNumberLabel + promissoriesInfo.First(pr => pr.Number == cp.PromissoryNumber).Number}"));
        }
    }
}
