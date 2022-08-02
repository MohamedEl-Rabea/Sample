using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using SSS.BackgroundJobs.Abstraction;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCaseVIban
{
    public class AddCaseVIbanCommand : Command<IResult>
    {
        public CreateCaseVIbanDto CaseVIbanDto { get; set; }
    }

    public class AddCaseVIbanCommandHandler : IRequestHandler<AddCaseVIbanCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IStringLocalizer<CMSLocalizer> _localizer;
        private readonly IMapper _mapper;

        public AddCaseVIbanCommandHandler(IMapper mapper, ICaseRepository caseRepository, IBackgroundJobManager backgroundJobManager)
        {
            _mapper = mapper;
            _caseRepository = caseRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task<IResult> Handle(AddCaseVIbanCommand request, CancellationToken cancellationToken)
        {
            //var createVIbanParam = _mapper.Map<CreateVIbanParam>(request.CaseVIbanDto);
            //var vIban = CaseVIban.Create(createVIbanParam);
            //var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.CaseVIbanDto.CaseNumber);
            ////caseAggregate.AddVIban(vIban);
            //Guard.AssertArgumentNotNull(vIban.CAP, nameof(vIban.CAP));

            //await _backgroundJobManager.EnqueueAsync(new CreateVIbanBackgroundJobArgs
            //{
            //    ParentAccount = vIban.VIban,
            //    Alias = vIban.Alias,
            //    Cap = vIban.CAP.Value
            //});
            return Result.Success();
        }
    }
}
