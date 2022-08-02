using MediatR;
using Moj.CMS.Application.AppServices.Promissory.Services;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Extensions;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Promissory.Commands.AddCasePromissory
{
    public class AddCasePromissoryCommand : Command<Result<ResourceCreatedDto>>
    {
        public IEnumerable<AddCasePromissoryDto> CasePromissoryList { get; set; }
    }

    public class AddCasePromissoryCommandHandler : IRequestHandler<AddCasePromissoryCommand, Result<ResourceCreatedDto>>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IPromissoryService _promissoryService;
        private readonly IUnitOfwork _unitOfwork;

        public AddCasePromissoryCommandHandler(ICaseRepository caseRepository, IPromissoryService promissoryService, IUnitOfwork unitOfwork)
        {
            _caseRepository = caseRepository;
            _promissoryService = promissoryService;
            _unitOfwork = unitOfwork;
        }

        public async Task<Result<ResourceCreatedDto>> Handle(AddCasePromissoryCommand request, CancellationToken cancellationToken)
        {
            var createdPromissories = new List<int>();
            var caseNumbers = request.CasePromissoryList.Select(cp => cp.CaseNumber);
            var caseAggregateList = await _caseRepository.GetAllAsync(c => caseNumbers.Contains(c.CaseNumber));
            var nonExistCases = caseNumbers.Except(caseAggregateList.Select(c => c.CaseNumber));
            if (nonExistCases.Any())
                throw new System.Exception($"Following cases are not exist [{string.Join(',', nonExistCases)}]");

            foreach (var casePromissory in request.CasePromissoryList)
            {
                var inputPromissoryList = casePromissory.PromissoryDtoList.ToList();
                var savedPromissoryList = await _promissoryService.AddPromissoryListAsync(inputPromissoryList);
                createdPromissories.AddRange(savedPromissoryList.Select(p => p.Id));
                await _unitOfwork.SaveCurrentChangesAsync();

                var casePromissoryList = inputPromissoryList.Select(p => CasePromissory.Create(p.PromissoryNumber));
                var caseAggregate = caseAggregateList.First(c => c.CaseNumber == casePromissory.CaseNumber);
                caseAggregate.AssignPromissories(casePromissoryList);
            }
            return Result<ResourceCreatedDto>.Success(createdPromissories.MapToResourceCreatedDto());
        }
    }
}
