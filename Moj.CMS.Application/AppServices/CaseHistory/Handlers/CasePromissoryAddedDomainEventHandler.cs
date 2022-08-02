using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Shared.Values;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.CaseHistory.Handlers
{
    public class CasePromissoryAddedDomainEventHandler : CaseChangedDomainEventHandlerBase<CasePromissoryAddedDomainEvent>
    {
        private readonly IPromissoryQueries _promissoryQuery;
        public CasePromissoryAddedDomainEventHandler(ICaseHistoryService caseHistoryService, IPromissoryQueries promissoryQuery) : base(caseHistoryService)
        {
            _promissoryQuery = promissoryQuery;
        }

        public override async Task<LocalizedText> GetDetailsAsync(CasePromissoryAddedDomainEvent domainEvent)
        {
            var promissories = await _promissoryQuery.GetPromissoriesBasicInfoByNumbers(domainEvent.CasePromissories.Select(cs => cs.PromissoryNumber));
            var enMessage = $" Promissories with the following info [{GetPromissoriesInfo(promissories, false)}] .";
            var arMessage = $" تم إضافة السند/السندات  التنفيذية   [{GetPromissoriesInfo(promissories, true)}] .";
            return new LocalizedText(enMessage, arMessage);
        }

        private static string GetPromissoriesInfo(IEnumerable<PromissoryBasicInfoDto> promissoryBasicInfoDto, bool isArabic)
        {
            var nameLabel = isArabic ? "الرقم: " : "Number: ";
            var typeName = isArabic ? "نوع السند: " : "Type name: ";
            var debtTypeName = isArabic ? "نوع الدين: " : "Debt name: ";

            return string.Join(',', promissoryBasicInfoDto.Select(a => $"{nameLabel + promissoryBasicInfoDto.First(p => p.Number == a.Number).Number} " +
            $"- {typeName + a.TypeName } "));
        }
    }
}
