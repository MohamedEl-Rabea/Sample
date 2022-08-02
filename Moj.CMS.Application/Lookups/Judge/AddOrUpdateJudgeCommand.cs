using AutoMapper;
using MediatR;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Extensions;
using Moj.CMS.Domain.Shared.Exceptions;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.Lookups.Judge
{
    public class AddOrUpdateJudgeCommand : Command<Result<ResourceCreatedDto>>
    {
        public IEnumerable<JudgeDto> JudgeDtoList { get; set; }
        public bool IsCreate { get; set; }
    }

    public class AddOrUpdateJudgeCommandHandler : IRequestHandler<AddOrUpdateJudgeCommand, Result<ResourceCreatedDto>>
    {
        private readonly IRepository<Domain.Shared.LookupModels.Judge> _judgeRepository;
        private readonly IMapper _mapper;

        public AddOrUpdateJudgeCommandHandler(IRepository<Domain.Shared.LookupModels.Judge> judgeRepository, IMapper mapper)
        {
            _judgeRepository = judgeRepository;
            _mapper = mapper;
        }

        public async Task<Result<ResourceCreatedDto>> Handle(AddOrUpdateJudgeCommand request, CancellationToken cancellationToken)
        {
            var judgeList = _mapper.Map<IEnumerable<Domain.Shared.LookupModels.Judge>>(request.JudgeDtoList).ToList();
            var inputJudgeCodes = judgeList.Select(j => j.Code).ToList();
            var exists = await _judgeRepository.GetAllListAsync(j => inputJudgeCodes.Contains(j.Code));
            var existCodes = exists.Select(e => e.Code).ToList();

            if (request.IsCreate)
            {
                if (exists.Any())
                    throw new DuplicateEntryException($"Following judge codes [{ string.Join(',', existCodes) }] already exist");

                await _judgeRepository.InsertRangeAsync(judgeList);
            }
            else
            {
                if (inputJudgeCodes.Except(existCodes).Any())
                    throw new System.Exception($"Following judge codes [{ string.Join(',', inputJudgeCodes.Except(existCodes)) }] not exist");


                judgeList.ForEach(a =>
                {
                    var judge = exists.First(e => e.Code == a.Code);
                    judge.Name = a.Name;
                    judge.IsActive = a.IsActive;
                });
            }
            var savedJudgeList = judgeList.Select(a => a.Id).MapToResourceCreatedDto();
            return Result<ResourceCreatedDto>.Success(savedJudgeList);
        }
    }
}
