using AutoMapper;
using Moj.CMS.Application.AppServices.SadadInvoice.Dtos;
using Moj.CMS.Application.Integration.Models;
using Moj.CMS.Application.Jobs.CreateSadadInvoice;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Integration.Contracts.ThirdParties.Tahseel.Dto;
using Moj.CMS.Shared.Interfaces;
using Newtonsoft.Json;
using SSS.BackgroundJobs.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.SadadInvoice.Services
{
    public class SadadInvoiceService : ISadadInvoiceService
    {
        private readonly IRepository<SadadInvoiceRequestLog> _sadadInvoiceRepository;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public SadadInvoiceService(IRepository<SadadInvoiceRequestLog> sadadInvoiceRepository, IMapper mapper,
            IBackgroundJobManager backgroundJobManager)
        {
            _sadadInvoiceRepository = sadadInvoiceRepository;
            _mapper = mapper;
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task CreateSadadInvoiceAsync(IEnumerable<CreateSadadInvoiceDto> createSadadInvoiceDtoList)
        {
            await InsertSadadInvoiceRequestLogAsync(createSadadInvoiceDtoList);

            var sadadInvoiceBackgroundJobInputs =
                _mapper.Map<IEnumerable<CreateSadadInvoiceJobInput>>(createSadadInvoiceDtoList).ToList();

            var sadadInvoiceBackgroundJobArgs = new CreateSadadInvoiceBackgroundJobArgs
            {
                CreateSadadInvoiceJobInputs = sadadInvoiceBackgroundJobInputs
            };
            await _backgroundJobManager.EnqueueAsync(sadadInvoiceBackgroundJobArgs);
        }

        private async Task InsertSadadInvoiceRequestLogAsync(IEnumerable<CreateSadadInvoiceDto> createSadadInvoiceDto)
        {
            var request = _mapper.Map<IEnumerable<SadadInvoiceCreationRequest>>(createSadadInvoiceDto);
            var serializedRequest = JsonConvert.SerializeObject(request);

            var sadadInvoiceRequestLog = new SadadInvoiceRequestLog
            {
                Status = IntegrationRequestStatusEnum.Scheduled,
                ScheduledTime = CLock.Now,
                InvoiceReferenceId = string.Join(",", createSadadInvoiceDto.Select(s => s.InvoiceReferenceId)),
                Request = serializedRequest
            };
            await _sadadInvoiceRepository.InsertAsync(sadadInvoiceRequestLog);
        }
    }
}