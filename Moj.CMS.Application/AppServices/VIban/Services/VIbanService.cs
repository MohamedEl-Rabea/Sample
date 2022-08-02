using Moj.CMS.Application.AppServices.VIban.Dtos;
using Moj.CMS.Application.Integration.Models;
using Moj.CMS.Application.Jobs.CreateVIban;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Integration.Contracts.AlAhli_B2B;
using Newtonsoft.Json;
using SSS.BackgroundJobs.Abstraction;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.VIban.Services
{
    public class VIbanService : IVIbanService
    {
        private readonly IRepository<VIbanRequestLog> _vIbanRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public VIbanService(IRepository<VIbanRequestLog> vIbanRepository,
            IBackgroundJobManager backgroundJobManager)
        {
            _vIbanRepository = vIbanRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task<string> CreateVIbanAsync(CreateVIbanDto createVIbanDto)
        {
            await InsertVIbanRequestLogAsync(createVIbanDto);
            return await _backgroundJobManager.EnqueueAsync(new CreateVIbanBackgroundJobArgs
            {
                ReferenceType = createVIbanDto.ReferenceDetails.ReferenceType,
                ReferenceNumber = createVIbanDto.ReferenceDetails.ReferenceNumber,
                Alias = createVIbanDto.Alias,
                ParentAccount = createVIbanDto.ParentAccount,
                CAP = createVIbanDto.CAP
            });
        }

        private async Task<VIbanRequestLog> InsertVIbanRequestLogAsync(CreateVIbanDto createVIbanDto)
        {
            var request = new VIbanCreationRequest
            {
                ParentAccount = createVIbanDto.ParentAccount,
                Alias = createVIbanDto.Alias,
                Cap = createVIbanDto.CAP
            };
            var serializedRequest = JsonConvert.SerializeObject(request);

            var vIbanRequestLog = new VIbanRequestLog
            {
                Status = IntegrationRequestStatusEnum.Scheduled,
                ScheduledTime = CLock.Now,
                ReferenceDetails = createVIbanDto.ReferenceDetails,
                Request = serializedRequest
            };

            return await _vIbanRepository.InsertAsync(vIbanRequestLog);
        }
    }
}
