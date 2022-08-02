using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries.GetPartySadadBilling
{

    public class GetPartySadadBillingQuery : Query<IResult<IEnumerable<PartySadadBillingDto>>>
    {
        public int PartyId { get; set; }
    }

    public class GetPartySadadBillingQueryHandler : IRequestHandler<GetPartySadadBillingQuery, IResult<IEnumerable<PartySadadBillingDto>>>
    {
        private readonly IPartyQueries _partyQueries;

        public GetPartySadadBillingQueryHandler(IPartyQueries partyQueries)
        {
            _partyQueries = partyQueries;
        }

        public async Task<IResult<IEnumerable<PartySadadBillingDto>>> Handle(GetPartySadadBillingQuery query, CancellationToken cancellationToken)
        {
            var result = new List<PartySadadBillingDto> {
                        new PartySadadBillingDto
                        {
                        CollectionChannel="1سداد",
                        CollectionChannelType="سداد",
                        ReferenceNumber="??",
                        PaymentNoticeDate=DateTime.UtcNow,
                        CachNoticeDate=DateTime.UtcNow,
                        SettlementDate=DateTime.UtcNow,
                        PaymentNoticeAmount=MoneyDto.Default,
                        CachNoticeAmount=MoneyDto.Default,
                         CollectedAmount=MoneyDto.Default,
                         SettlementResult="??"
                        },
                        new PartySadadBillingDto{
                        CollectionChannel="2سداد",
                        CollectionChannelType="سداد",
                        ReferenceNumber="??",
                        PaymentNoticeDate=DateTime.UtcNow,
                        CachNoticeDate=DateTime.UtcNow,
                        SettlementDate=DateTime.UtcNow,
                        PaymentNoticeAmount=MoneyDto.Default,
                        CachNoticeAmount=MoneyDto.Default,
                         CollectedAmount=MoneyDto.Default,
                         SettlementResult="??"
                        }
                        };
            return Result<IEnumerable<PartySadadBillingDto>>.Success(result);
        }
    }
}
