using MediatR;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Extensions;
using Moj.CMS.Domain.Aggregates.Court.ValueObjects;
using Moj.CMS.Domain.Shared.Exceptions;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Court
{
    public class AddOrUpdateCourtCommand : Command<Result<ResourceCreatedDto>>
    {
        public IEnumerable<CourtDto> CourtDtoList { get; set; }
        public bool IsCreate { get; set; }
    }

    public class AddCourtCommandHandler : IRequestHandler<AddOrUpdateCourtCommand, Result<ResourceCreatedDto>>
    {
        private readonly IRepository<Domain.Aggregates.Court.Court> _courtRepository;

        public AddCourtCommandHandler(IRepository<Domain.Aggregates.Court.Court> courtRepository)
        {
            _courtRepository = courtRepository;
        }

        public async Task<Result<ResourceCreatedDto>> Handle(AddOrUpdateCourtCommand request, CancellationToken cancellationToken)
        {
            var inputCourtList = request.CourtDtoList.ToList();
            var courtList = inputCourtList.Select(inputDto => Domain.Aggregates.Court.Court.Create(BuildCourtInfoParam(inputDto))).ToList();
            var inputCourtCodes = courtList.Select(c => c.Code).ToList();
            var exists = await _courtRepository.GetAllListAsync(j => inputCourtCodes.Contains(j.Code));
            var existCodes = exists.Select(e => e.Code).ToList();
            if (request.IsCreate)
            {
                if (exists.Any())
                    throw new DuplicateEntryException($"Following courts codes [{ string.Join(',', existCodes) }] already exist");

                await _courtRepository.InsertRangeAsync(courtList);
            }
            else
            {
                if (inputCourtCodes.Except(existCodes).Any())
                    throw new System.Exception($"Following courts codes [{ string.Join(',', inputCourtCodes.Except(existCodes)) }] not exist");

                inputCourtList.ForEach(input =>
                {
                    var court = exists.First(e => e.Code == input.Code);
                    court.Update(BuildCourtInfoParam(input));
                });
            }
            var savedCourts = courtList.Select(a => a.Id).MapToResourceCreatedDto();
            return Result<ResourceCreatedDto>.Success(savedCourts);
        }

        private static Domain.ParameterObjects.Court.CourtInfoParam BuildCourtInfoParam(CourtDto inputDto)
        {
            //TODO: availableVIbanCount should be read from input or settings
            return new Domain.ParameterObjects.Court.CourtInfoParam
            {
                Name = inputDto.Name,
                Code = inputDto.Code,
                AreaCode = inputDto.AreaCode,
                IsActive = inputDto.IsActive,
                BanckAccounts = inputDto.BankAccounts.Select(inputAccount => CourtBankAccount.Create(inputAccount, isActive: true))
            };
        }
    }
}