using MediatR;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries.GetPartyBankAccounts
{

    public class GetPartyBankAccountsQuery : Query<Result<IEnumerable<PartyBankAccountDto>>>
    {
        public int PartyId { get; set; }
    }

    public class GetPartyBankAccountsQueryHandler : IRequestHandler<GetPartyBankAccountsQuery, Result<IEnumerable<PartyBankAccountDto>>>
    {

        public GetPartyBankAccountsQueryHandler()
        {

        }

        public async Task<Result<IEnumerable<PartyBankAccountDto>>> Handle(GetPartyBankAccountsQuery query, CancellationToken cancellationToken)
        {
            var result = new List<PartyBankAccountDto> {
            new PartyBankAccountDto
            {
                AccountName="Account Name1",
                AccountNumber="SA011",
                AccountType="حقيقي",
                BankName="البلاد",
                CaseNumber="147",
                AccountValidityStatus="No",
                AccountVerificationStatus="Yes",
                PartyNumber="test 123",
                NotHasAccountReason="??",
                AccountMovementsUrl="url",
            },
            new PartyBankAccountDto{
                AccountName="Account Name2",
                AccountNumber="SA012",
                AccountType="حقيقي",
                BankName="2البلاد",
                CaseNumber="148",
                AccountValidityStatus="No",
                AccountVerificationStatus="Yes",
                PartyNumber="test 123",
                NotHasAccountReason="??",
                AccountMovementsUrl="url",
            }
            };
            return Result<IEnumerable<PartyBankAccountDto>>.Success(result);
        }
    }
}
