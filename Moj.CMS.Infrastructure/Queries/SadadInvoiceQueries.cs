using Microsoft.Extensions.Localization;
using Moj.CMS.Application.AppServices.SadadInvoice.Queries;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Aggregates.SadadInvoice;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Queries
{
    public class SadadInvoiceQueries : ISadadInvoiceQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;
        private readonly IStringLocalizer<CMSLocalizer> _localizer;

        public SadadInvoiceQueries(IStringLocalizer<CMSLocalizer> localizer, IQueryBuilderCreator<CMSDbContext> queryBuilderCreator)
        {
            _queryBuilderCreator = queryBuilderCreator;
            _localizer = localizer;
        }
        public async Task<PagedResult<SadadInvoiceDto>> GetAllAsync(PagedRequest<SadadInvoiceDto> request)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var query = from sadadInvoice in queryBuilder.Query<SadadInvoice>()
                            join claim in queryBuilder.Query<Claim>() on sadadInvoice.ClaimNumber equals claim.Id.ToString()
                            join party in queryBuilder.Query<Party>() on sadadInvoice.PartyNumber equals party.PartyNumber
                            select new SadadInvoiceDto
                            {
                                ClaimNumber = sadadInvoice.ClaimNumber,
                                CaseNumber = claim.CaseNumber,
                                Amount = new MoneyDto { Value = sadadInvoice.Amount.Value, CurrencyIso = sadadInvoice.Amount.CurrencyIso },
                                PaidAmount = new MoneyDto {Value=0,CurrencyIso="SAR"},
                                RemainingAmount = new MoneyDto { Value = 0, CurrencyIso = "SAR" },
                                Description = sadadInvoice.Description,
                                ExpiryDate = sadadInvoice.ExpiryDate,
                                IssueDate = sadadInvoice.IssueDate,
                                MinBillableAmount = new MoneyDto { Value = sadadInvoice.MinBillableAmount.Value, CurrencyIso = sadadInvoice.MinBillableAmount.CurrencyIso },
                                VIban = sadadInvoice.VIban,
                                PartyIdentityNumber = sadadInvoice.PartyNumber,
                                PartyName = party.FullName,
                                Number = sadadInvoice.Number
                            };
                var result = await query.ToPagedListAsync(request);
                return result;
            }
        }
    }
}
