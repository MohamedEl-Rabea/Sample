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

namespace Moj.CMS.Application.Lookups.Area
{
    public class AddOrUpdateAreaCommand : Command<Result<ResourceCreatedDto>>
    {
        public IEnumerable<AreaDto> AreaDtoList { get; set; }
        public bool IsCreate { get; set; }
    }

    public class AddOrUpdateAreaCommandHandler : IRequestHandler<AddOrUpdateAreaCommand, Result<ResourceCreatedDto>>
    {
        private readonly IRepository<Domain.Shared.LookupModels.Area> _areaRepository;
        private readonly IMapper _mapper;

        public AddOrUpdateAreaCommandHandler(IRepository<Domain.Shared.LookupModels.Area> AreaRepository, IMapper mapper)
        {
            _areaRepository = AreaRepository;
            _mapper = mapper;
        }

        public async Task<Result<ResourceCreatedDto>> Handle(AddOrUpdateAreaCommand request, CancellationToken cancellationToken)
        {
            var areaList = _mapper.Map<IEnumerable<Domain.Shared.LookupModels.Area>>(request.AreaDtoList).ToList();
            var inputAreaCodes = areaList.Select(j => j.Code).ToList();
            var exists = await _areaRepository.GetAllListAsync(a => inputAreaCodes.Contains(a.Code));
            var existCodes = exists.Select(e => e.Code).ToList();
            if (request.IsCreate)
            {
                if (exists.Any())
                    throw new DuplicateEntryException($"Following areas codes [{ string.Join(',', existCodes) }] already exist");

                await _areaRepository.InsertRangeAsync(areaList);
            }
            else
            {
                if (inputAreaCodes.Except(existCodes).Any())
                    throw new System.Exception($"Following areas codes [{ string.Join(',', inputAreaCodes.Except(existCodes)) }] not exist");

                areaList.ForEach(a =>
                {
                    var area = exists.First(e => e.Code == a.Code);
                    area.Name = a.Name;
                    area.IsActive = a.IsActive;
                });
            }
            var savedAreas = areaList.Select(a => a.Id).MapToResourceCreatedDto();
            return Result<ResourceCreatedDto>.Success(savedAreas);
        }
    }
}
