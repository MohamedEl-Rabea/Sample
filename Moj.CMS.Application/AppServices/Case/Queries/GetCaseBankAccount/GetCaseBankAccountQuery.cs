using MediatR;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseBankAccount
{
    public class GetCaseBankAccountQuery : Query<IResult<IEnumerable<CaseBankAccountDto>>>
    {
        public int CaseId { get; set; }
    }

    public class GetCaseBankAccountQueryHandler : IRequestHandler<GetCaseBankAccountQuery, IResult<IEnumerable<CaseBankAccountDto>>>
    {

        public GetCaseBankAccountQueryHandler()
        {

        }

        public async Task<IResult<IEnumerable<CaseBankAccountDto>>> Handle(GetCaseBankAccountQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<CaseBankAccountDto> caseBankAccountDtos = new List<CaseBankAccountDto>
            {
                new CaseBankAccountDto
            {
                AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },
                new CaseBankAccountDto
            {
                AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },
                new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },    new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الاهلى",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },
                new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }, new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            },
                new CaseBankAccountDto
            {
               AccountName="محكمة الرياض",
                AccountNumber="SA-CNB-1",
                AccountType="حقيقى",
                BankName="الراجحي",
                CollectionAccount="UMNI Account",
                TransactionCount=2213,
                ReferenceName="محكمه",
                ReferenceType="01",
            }
            };
            return Result<IEnumerable<CaseBankAccountDto>>.Success(caseBankAccountDtos);
        }
    }
}
