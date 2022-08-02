using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Application.AppServices.Claims.Queries.Dtos;
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
    public class CaseCreatedDomainEventHandler : CaseChangedDomainEventHandlerBase<CaseCreatedDomainEvent>
    {
        private readonly IPartyQueries _partyQueries;
        private readonly IPromissoryQueries _promissoryQueries;
        private readonly ILookupsService _lookupsService;
        private readonly IClaimQueries _claimsQueries;

        public CaseCreatedDomainEventHandler(ICaseHistoryService caseHistoryService, IPartyQueries partyQueries,
            IPromissoryQueries promissoryQueries,
            ILookupsService lookupsService,
            IClaimQueries claimsQueries) : base(caseHistoryService)
        {
            _partyQueries = partyQueries;
            _promissoryQueries = promissoryQueries;
            _lookupsService = lookupsService;
            _claimsQueries = claimsQueries;
        }

        public override async Task<LocalizedText> GetDetailsAsync(CaseCreatedDomainEvent caseCreatedDomainEvent)
        {
            var partiesText = await GetPartiesData(caseCreatedDomainEvent.CaseParties);
            var promissoriesText = await GetPromissoryListData(caseCreatedDomainEvent.CasePromissories);
            var claims = await GetClaimsData(caseCreatedDomainEvent.CaseNumber);

            var enMessage = $"Case with number {caseCreatedDomainEvent.CaseNumber} is created. With details :"
                            + System.Environment.NewLine + $"- {partiesText.EN}"
                            + System.Environment.NewLine + $"- {promissoriesText.EN}"
                            + System.Environment.NewLine + $"- {claims.EN}";

            var arMessage = $"تم انشاء طلب تنفيذ رقم {caseCreatedDomainEvent.CaseNumber} بالتفاصيل الاتية :"
                            + System.Environment.NewLine + $"- {partiesText.AR}"
                            + System.Environment.NewLine + $"- {promissoriesText.AR}"
                            + System.Environment.NewLine + $"- {claims.AR}";

            return new LocalizedText(enMessage, arMessage);
        }

        private async Task<LocalizedText> GetPartiesData(IEnumerable<CaseParty> caseParties)
        {
            var partiesNumbers = caseParties.Select(c => c.PartyNumber);
            var partiesBasicInfo = await _partyQueries.GetPartiesBasicInfoByNumbersAsync(partiesNumbers);

            var promissoryListNumbers = caseParties.Select(c => c.PromissoryNumber);
            var promissoryListInfo = await _promissoryQueries.GetPromissoriesBasicInfoByNumbers(promissoryListNumbers);

            var partiesRolesIds = caseParties.Select(c => (int)c.PartyRoleId);
            var partiesRoles = await _lookupsService.GetPartiesRolesItemsNamesAsync(partiesRolesIds);

            var enMessage = $"Parties [{GetAddedPartiesInfo(caseParties, partiesBasicInfo, promissoryListInfo, partiesRoles, isArabic: false)}].";
            var arMessage = $"المستفيدون [{GetAddedPartiesInfo(caseParties, partiesBasicInfo, promissoryListInfo, partiesRoles, isArabic: true)}].";
            return new LocalizedText(enMessage, arMessage);
        }

        private string GetAddedPartiesInfo(IEnumerable<CaseParty> caseParties,
            IEnumerable<PartyBasicInfoDto> partiesBasicInfo,
            IEnumerable<PromissoryBasicInfoDto> promissoryListInfo,
            IEnumerable<SelectListItem> partiesRoles,
            bool isArabic)
        {
            var nameLabel = isArabic ? "الاسم: " : "Name: ";
            var roleLabel = isArabic ? "الدور: " : "Role: ";
            var promissoryNumberLabel = isArabic ? "رقم السند: " : "Promissory number: ";
            var separator = isArabic ? "، " : ", ";

            return string.Join(separator, caseParties.Select(cp => $"{nameLabel + partiesBasicInfo.First(p => p.Number == cp.PartyNumber).Name} " +
            $"- {roleLabel}({partiesRoles.First(r => r.Key == (int)cp.PartyRoleId).Text}) "
            +
            $"- {promissoryNumberLabel + promissoryListInfo.First(pr => pr.Number == cp.PromissoryNumber).Number}"));
        }

        private async Task<LocalizedText> GetPromissoryListData(IEnumerable<CasePromissory> casePromissoryList)
        {
            var promissoryNumbers = casePromissoryList.Select(cs => cs.PromissoryNumber);
            var promissoryList = await _promissoryQueries.GetPromissoriesBasicInfoByNumbers(promissoryNumbers);
            var enMessage = $" Promissories [{GetAddedPromissoryListInfo(promissoryList, false)}].";
            var arMessage = $" السندات [{GetAddedPromissoryListInfo(promissoryList, true)}].";
            return new LocalizedText(enMessage, arMessage); ;
        }

        private string GetAddedPromissoryListInfo(IEnumerable<PromissoryBasicInfoDto> promissoryList, bool isArabic)
        {
            var numberLabel = isArabic ? "الرقم: " : "Number: ";
            var typeName = isArabic ? "النوع: " : "Type name: ";
            var debtTypeName = isArabic ? "نوع الدين: " : "Debt name: ";
            var separator = isArabic ? "، " : ", ";

            return string.Join(separator, promissoryList.Select(a => $"{numberLabel + promissoryList.First(p => p.Number == a.Number).Number} " +
                                                                     $"- {typeName + a.TypeName } "));
        }

        private async Task<LocalizedText> GetClaimsData(string caseNumber)
        {
            var claims = await _claimsQueries.GetCaseClaimsAsync(caseNumber);

            var enMessage = $"Claims [{GetAddedClaimsInfo(claims, false)}].";
            var arMessage = $"المطالبات الماليه [{GetAddedClaimsInfo(claims, true)}].";

            return new LocalizedText(enMessage, arMessage);
        }

        private string GetAddedClaimsInfo(IEnumerable<ClaimBasicDetailsDto> claims, bool isArabic)
        {
            var numberLabel = isArabic ? "الرقم: " : "Number: ";
            var partyLabel = isArabic ? "الدائن: " : "Complaint: ";
            var amountLabel = isArabic ? "المطلوب: " : "Required: ";
            var separator = isArabic ? "، " : ", ";

            return string.Join(separator, claims.Select(cp => $"{numberLabel + cp.ClaimNumber} " +
                                                              $"- {partyLabel + cp.ComplaintPartyName} " +
                                                              $"- {amountLabel + cp.RequiredAmount}"));
        }
    }
}
