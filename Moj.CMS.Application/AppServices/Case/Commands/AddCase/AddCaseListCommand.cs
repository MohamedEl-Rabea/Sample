using AutoMapper;
using MediatR;
using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Application.AppServices.Claims.Services;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.AppServices.Party.Services;
using Moj.CMS.Application.AppServices.Promissory.Services;
using Moj.CMS.Application.AppServices.SadadInvoice.Dtos;
using Moj.CMS.Application.AppServices.SadadInvoice.Services;
using Moj.CMS.Application.AppServices.VIban.Dtos;
using Moj.CMS.Application.AppServices.VIban.Services;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Extensions;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Iban;
using Moj.CMS.Domain.Aggregates.SadadInvoice;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.DomainServices.SadadInvoice;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Domain.ParameterObjects.SadadInvoice;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCase
{
    public class AddCaseListCommand : Command<Result<ResourceCreatedDto>>
    {
        public IEnumerable<CaseFullDetailsDto> NewCaseInputList { get; set; } = new List<CaseFullDetailsDto>();
    }

    public class AddCaseListCommandHandler : IRequestHandler<AddCaseListCommand, Result<ResourceCreatedDto>>
    {
        private readonly IPartyService _partyService;
        private readonly IPartyQueries _partyQueries;
        private readonly IPromissoryService _promissoryService;
        private readonly IClaimService _claimService;
        private readonly IUnitOfwork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICaseRepository _caseRepository;
        private readonly ISadadInvoiceRepository _sadadInvoiceRepository;
        private readonly IEnforceCaseNumberIsUnique _enforceCaseNumberIsUnique;
        private readonly IEnforceJudgeIsExists _enforceJudgeIsExist;
        private readonly IEnforceCourtIsExists _enforceCourtIsExist;
        private readonly IClaimInfoProvider _claimInfoProvider;
        private readonly IGetDivisionCourtCode _getDivisionCourtCode;
        private readonly IRepository<Iban> _ibanQueries;
        private readonly IVIbanService _vIbanService;
        private readonly ISadadInvoiceService _sadadInvoiceService;

        public AddCaseListCommandHandler(IPromissoryService promissoryService, IPartyService partyService, IPartyQueries partyQueries,
            IClaimService claimService,
            IUnitOfwork unitOfWork,
            IMapper mapper,
            ICaseRepository caseRepository,
            ISadadInvoiceRepository sadadInvoiceRepository,
            IEnforceCaseNumberIsUnique enforceCaseNumberIsUnique,
            IEnforceJudgeIsExists enforceJudgeIsExist,
            IEnforceCourtIsExists enforceCourtIsExist,
            IClaimInfoProvider claimInfoProvider,
            IGetDivisionCourtCode getDivisionCourtCode,
            IRepository<Iban> ibanQueries,
            IVIbanService vIbanService,
            ISadadInvoiceService sadadInvoiceService)
        {
            _promissoryService = promissoryService;
            _partyService = partyService;
            _partyQueries = partyQueries;
            _claimService = claimService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _caseRepository = caseRepository;
            _sadadInvoiceRepository = sadadInvoiceRepository;
            _enforceCaseNumberIsUnique = enforceCaseNumberIsUnique;
            _enforceJudgeIsExist = enforceJudgeIsExist;
            _enforceCourtIsExist = enforceCourtIsExist;
            _claimInfoProvider = claimInfoProvider;
            _getDivisionCourtCode = getDivisionCourtCode;
            _ibanQueries = ibanQueries;
            _vIbanService = vIbanService;
            _sadadInvoiceService = sadadInvoiceService;
        }

        public async Task<Result<ResourceCreatedDto>> Handle(AddCaseListCommand request, CancellationToken cancellationToken)
        {
            var savedCaseList = new List<int>();
            foreach (var input in request.NewCaseInputList)
            {
                var courtAccountNumber = (await _ibanQueries.FirstOrDefaultAsync(i =>
                    i.IbanReferenceDetails.ReferenceType == IbanPurposeEnum.Court &&
                    i.IbanReferenceDetails.ReferenceNumber == input.BasicDetails.CourtCode &&
                    i.VIbanRemaining > 1 &&
                    i.IsActive)).Number;

                //1- Add parties
                await _partyService.AddPartiesAsync(input.Requesters.Select(p => p.Details), input.Respondents.Select(p => p.Details), true);

                //2- Add promissory
                var savedPromissories = await _promissoryService.AddPromissoryListAsync(input.PromissoryList);
                var casePromissory = savedPromissories.Single();

                //3- Add Case
                var caseId = await AddCaseAsync(input);

                //4- Add claims
                await AddCaseClaims(input, casePromissory.Number, courtAccountNumber);

                //5- Create Case VIban
                await CreateCaseVIbanAsync(input.BasicDetails, courtAccountNumber);

                savedCaseList.Add(caseId);
            }
            return Result<ResourceCreatedDto>.Success(savedCaseList.MapToResourceCreatedDto());
        }

        public async Task<int> AddCaseAsync(CaseFullDetailsDto caseDto)
        {
            var addNewCaseParam = BuildNewCaseCommandParameter(caseDto);
            var caseAggregate = await Domain.Aggregates.Case.Case.CreateAsync(addNewCaseParam);
            await _caseRepository.AddAsync(caseAggregate);
            await _unitOfWork.SaveCurrentChangesAsync();
            return caseAggregate.Id;
        }

        private AddNewCaseParameter BuildNewCaseCommandParameter(CaseFullDetailsDto input)
        {
            var basicDetails = input.BasicDetails;

            var promissoryNumber = input.PromissoryList.Single().PromissoryNumber;
            var requesters = input.Requesters.Select(rq =>
                CaseParty.Create(new CasePartyCreationParam
                {
                    PartyNumber = rq.Details.PartyNumber,
                    PromissoryNumber = promissoryNumber,
                    PartyRoleTypeId = rq.PartyRole,
                    IsApplicant = rq.IsApplicant,
                    PartyClassificationId = PartyClassificationEnum.Requester

                }));

            var respondents = input.Respondents.Select(rp =>
                CaseParty.Create(new CasePartyCreationParam
                {
                    PartyNumber = rp.Details.PartyNumber,
                    PromissoryNumber = promissoryNumber,
                    PartyRoleTypeId = rp.PartyRole,
                    IsApplicant = rp.IsApplicant,
                    PartyClassificationId = PartyClassificationEnum.Respondent
                }));


            return new AddNewCaseParameter
            {
                CaseNumber = basicDetails.CaseNumber,
                CaseTypeId = basicDetails.CaseType,
                CaseStatusId = basicDetails.CaseStatus,
                RequiredAmount = basicDetails.RequiredAmount.ToValueObject(),
                CaseBasicAmount = basicDetails.CaseBasicAmount.ToValueObject(),
                DatesInfo = CaseDate.Create(basicDetails.ReceiveDate, basicDetails.JudgeAcceptanceDate),
                CaseParties = requesters.Union(respondents),
                CasePromissories = new List<CasePromissory> { CasePromissory.Create(promissoryNumber) },
                EnforceCaseNumberIsUnique = _enforceCaseNumberIsUnique,
                CaseDetailsParam = new CaseDetailsParam
                {
                    CaseDetails = CaseDetails.Create(basicDetails.DivisionCode, basicDetails.CourtCode, basicDetails.JudgeCode),
                    EnforceJudgeIsExists = _enforceJudgeIsExist,
                    EnforceCourtIsExists = _enforceCourtIsExist,
                    GetDivisionCourtCode = _getDivisionCourtCode
                },
            };
        }

        private async Task AddCaseClaims(CaseFullDetailsDto input, string promissoryNumber, string courtAccountNumber)
        {
            var caseClaimsDto = _mapper.Map<List<CreateClaimDto>>(input.Claims);
            caseClaimsDto.ForEach(c =>
            {
                c.CaseNumber = input.BasicDetails.CaseNumber;
                c.PromissoryNumber = promissoryNumber;
            });

            var claims = await _claimService.AddCaseClaimsAsync(caseClaimsDto);
            await _unitOfWork.SaveCurrentChangesAsync();

            var includedAccusedParties = input.Claims.SelectMany(c => c.ClaimDetails)
                .Where(c => c.CreateSadadBill)
                .Select(c => c.AccusedPartyNumber);

            await CreateClaimsSadadInvoicesAsync(claims, includedAccusedParties, courtAccountNumber);
        }

        private async Task CreateClaimsSadadInvoicesAsync(List<Claim> claims,
            IEnumerable<string> includedAccusedParties, string courtAccountNumber)
        {
            List<CreateSadadInvoiceDto> createSadadInvoices = new List<CreateSadadInvoiceDto>();

            foreach (var claim in claims)
            {
                var accusedPartiesNumbers = claim.ClaimDetailsList.Where(c => includedAccusedParties.Contains(c.PartyNumber))
                    .Select(c => c.PartyNumber);
                var parties = await _partyQueries.GetPartiesBasicInfoByNumbersAsync(accusedPartiesNumbers);

                var draftInvoices = await CreateDraftInvoices(parties, claim);

                var createSadadInvoice = parties.Select(p => new CreateSadadInvoiceDto
                {
                    MinBillableAmount = 1,
                    Amount = claim.RequiredAmount.Value,
                    PartyName = p.Name,
                    PartyIdentityNumber = p.Number,
                    PartyIdentityTypeId = p.PartyIdentityTypeId,
                    InvoiceReferenceId = draftInvoices[p.Number].ToString(),
                    DueDate = CLock.Now, //TODO
                    ExpiryDate = CLock.Now, //TODO
                    Description = "", //TODO
                    DisplayLabel = new LocalizedText("", ""), //TODO
                    Category = "", //TODO
                });
                createSadadInvoices.AddRange(createSadadInvoice);
            }

            await _sadadInvoiceService.CreateSadadInvoiceAsync(createSadadInvoices);
            await CreateSadadInvoicesVIbansAsync(createSadadInvoices, courtAccountNumber);
        }

        private async Task CreateSadadInvoicesVIbansAsync(List<CreateSadadInvoiceDto> sadadInvoices, string courtAccountNumber)
        {
            foreach (var sadadInvoice in sadadInvoices)
            {
                await _vIbanService.CreateVIbanAsync(new CreateVIbanDto
                {
                    ParentAccount = courtAccountNumber,
                    Alias = "TestName", //TODO
                    ReferenceDetails = VIbanReferenceDetails.Create(sadadInvoice.InvoiceReferenceId, VIbanReferenceTypeEnum.Sadad),
                    CAP = 1000 //TODO
                });

            }
        }

        private async Task<Dictionary<string, int>> CreateDraftInvoices(IEnumerable<PartyBasicInfoDto> parties, Claim claim)
        {
            var result = new Dictionary<string, int>();

            foreach (var party in parties)
            {
                var param = new CreateSadadInvoiceParam
                {
                    PartyNumber = party.Number,
                    Amount = claim.ClaimDetailsList.First(p => p.PartyNumber == party.Number).RequiredAmount.Clone(),
                    ClaimNumber = claim.Id.ToString(),
                    MinBillableAmount = Money.Create("SAR", 1),
                    IssueDate = CLock.Now,
                    ClaimInfoProvider = _claimInfoProvider
                };
                var sadadInvoice = await Domain.Aggregates.SadadInvoice.SadadInvoice.Draft(param);
                await _sadadInvoiceRepository.AddAsync(sadadInvoice);
                result.Add(sadadInvoice.PartyNumber, sadadInvoice.Id);
            }

            return result;
        }

        private async Task CreateCaseVIbanAsync(CaseDetailsDto caseDetails, string courtAccountNumber)
        {
            if (caseDetails.CreateVIban)
            {
                await _vIbanService.CreateVIbanAsync(new CreateVIbanDto
                {
                    ParentAccount = courtAccountNumber,
                    Alias = "TestName", //TODO
                    ReferenceDetails = VIbanReferenceDetails.Create(caseDetails.CaseNumber, VIbanReferenceTypeEnum.Case),
                    CAP = 1000 //TODO
                });
            }
        }
    }
}
