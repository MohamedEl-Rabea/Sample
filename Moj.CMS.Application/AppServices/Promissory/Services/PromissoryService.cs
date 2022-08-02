using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Promissory;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.ParameterObjects.Promissory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Promissory.Services
{
    public class PromissoryService : IPromissoryService
    {
        private readonly IPromissoryRepository _promissoryRepository;
        private readonly IPromissoryQueries _promissoryQueries;
        private readonly IEnforcePromissoryNumberIsUnique _enforcePromissoryNumberIsUnique;

        public PromissoryService(IPromissoryRepository promissoryRepository, IPromissoryQueries promissoryQueries,
            IEnforcePromissoryNumberIsUnique enforcePromissoryNumberIsUnique)
        {
            _promissoryRepository = promissoryRepository;
            _promissoryQueries = promissoryQueries;
            _enforcePromissoryNumberIsUnique = enforcePromissoryNumberIsUnique;
        }

        public async Task<IEnumerable<SavedPromissory>> AddPromissoryListAsync(IEnumerable<PromissoryDto> promissoryList)
        {
            var result = new List<SavedPromissory>();
            var existsPromissoryList = await _promissoryQueries.GetPromissoriesBasicInfoByNumbers(
                    promissoryList.Select(p => p.PromissoryNumber));

            result.AddRange(existsPromissoryList.Select(exists => new SavedPromissory
            {
                Id = exists.Id,
                Number = exists.Number
            }));

            var newPromissoryList = promissoryList
                .Where(input => !existsPromissoryList.Any(exist => input.PromissoryNumber == exist.Number));

            foreach (var promissory in newPromissoryList)
            {
                var promissoryAggregate = await CreatePromissoryAsync(promissory);
                var added = await _promissoryRepository.AddAsync(promissoryAggregate);
                result.Add(new SavedPromissory
                {
                    Id = added.Id,
                    Number = added.Number
                });
            }
            return result;
        }

        private async Task<Domain.Aggregates.Promissory.Promissory> CreatePromissoryAsync(PromissoryDto newPromissoryInput)
        {
            var addNewPromissoryParam = BuildNewPromissoryCommandParameter(newPromissoryInput);
            var promissory = await Domain.Aggregates.Promissory.Promissory.CreateAsync(addNewPromissoryParam);
            return promissory;
        }

        private AddNewPromissoryParameter BuildNewPromissoryCommandParameter(PromissoryDto input)
        {
            return new AddNewPromissoryParameter
            {
                Number = input.PromissoryNumber,
                PromissoryTypeId = input.PromissoryTypeId,
                PromissoryIssueDate = input.PromissoryIssueDate,
                EnforcePromissoryNumberIsUnique = _enforcePromissoryNumberIsUnique
            };
        }
    }
}
