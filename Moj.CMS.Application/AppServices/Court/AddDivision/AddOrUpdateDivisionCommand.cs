using MediatR;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Shared.Exceptions;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.Lookups.Division
{
    public class AddOrUpdateDivisionCommand : Command<IResult>
    {
        public IEnumerable<DivisionDto> DivisionDtoList { get; set; }
        public bool IsCreate { get; set; }
    }

    public class AddOrUpdateDivisionCommandHandler : IRequestHandler<AddOrUpdateDivisionCommand, IResult>
    {
        private readonly IRepository<Domain.Aggregates.Court.Court> _courtRepository;
        private readonly ICourtQueries _courtQueries;

        public AddOrUpdateDivisionCommandHandler(IRepository<Domain.Aggregates.Court.Court> courtRepository,
            ICourtQueries courtQueries)
        {
            _courtRepository = courtRepository;
            _courtQueries = courtQueries;
        }

        public async Task<IResult> Handle(AddOrUpdateDivisionCommand request, CancellationToken cancellationToken)
        {
            var inputDivisionList = request.DivisionDtoList.ToList();
            var inputDivisionCodes = inputDivisionList.Select(d => d.Code).ToList();
            var inputDivisionCourtCodes = inputDivisionList.Select(d => d.CourtCode).ToList();
            var existsDivisions = await _courtQueries.GetDivisionsByCodeAsync(inputDivisionCodes.ToArray());
            var existCodes = existsDivisions.Select(e => e.Code).ToList();
            if (request.IsCreate)
            {
                if (existsDivisions.Any())
                    throw new DuplicateEntryException($"Following division codes [{ string.Join(',', existCodes) }] already exist");

                var courts = await _courtRepository.GetAllListAsync(c => inputDivisionCourtCodes.Contains(c.Code));
                foreach (var court in courts)
                {
                    var courtDivisions = inputDivisionList.Where(d => d.CourtCode == court.Code)
                        .Select(inputDivision => Domain.Aggregates.Court.Entities.Division.Create(inputDivision.Name,
                                                inputDivision.Code, inputDivision.IsActive ?? false))
                        .ToList();
                    court.AddDivisions(courtDivisions);
                }
            }
            else
            {
                if (inputDivisionCodes.Except(existCodes).Any())
                    throw new System.Exception($"Following division codes [{ string.Join(',', inputDivisionCodes.Except(existCodes)) }] not exist");

                var courts = await _courtRepository.GetAllListAsync(c => inputDivisionCourtCodes.Contains(c.Code));
                courts.ForEach(court =>
                {
                    var courtDivisions = inputDivisionList.Where(d => d.CourtCode == court.Code)
                    .Select(inputDivision => Domain.Aggregates.Court.Entities.Division.Create(inputDivision.Name,
                    inputDivision.Code, inputDivision.IsActive ?? false));
                    court.ReplaceDivisions(courtDivisions);
                });
            }
            return Result.Success();
        }
    }
}
