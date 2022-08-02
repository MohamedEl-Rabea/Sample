using AutoMapper;
using Moj.CMS.Application.Integration.Models;
using Moj.CMS.Domain.Aggregates.SadadInvoice;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Integration.Contracts.ThirdParties.Tahseel;
using Moj.CMS.Integration.Contracts.ThirdParties.Tahseel.Dto;
using Newtonsoft.Json;
using SSS.BackgroundJobs.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.Jobs.CreateSadadInvoice
{
    public class CreateSadadInvoiceBackgroundJob<TArgs> : IAsyncBackgroundJob<TArgs>
        where TArgs : CreateSadadInvoiceBackgroundJobArgs
    {
        private readonly ITahseelClient _tahseelClient;
        private readonly IRepository<SadadInvoiceRequestLog> _sadadInvoiceRequestLogRepository;
        private readonly ISadadInvoiceRepository _sadadInvoiceRepository;
        private readonly IMapper _mapper;

        public CreateSadadInvoiceBackgroundJob(ITahseelClient tahseelClient, IRepository<SadadInvoiceRequestLog> sadadInvoiceRequestLogRepository,
            ISadadInvoiceRepository sadadInvoiceRepository,
            IMapper mapper)
        {
            _tahseelClient = tahseelClient;
            _sadadInvoiceRequestLogRepository = sadadInvoiceRequestLogRepository;
            _sadadInvoiceRepository = sadadInvoiceRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TArgs args)
        {
            var sadadInvoiceRequestLog = await SetRequestStatusToProcessingAsync(args);
            var createInvoiceRequest = _mapper.Map<IEnumerable<SadadInvoiceCreationRequest>>(args.CreateSadadInvoiceJobInputs);
            var result = await _tahseelClient.CreateSadadInvoiceAsync(createInvoiceRequest);
            await SetResponseAsync(sadadInvoiceRequestLog, result);
            await CreateSadadInvoiceIfSuccessAsync(result, sadadInvoiceRequestLog.Status);
        }

        private async Task<SadadInvoiceRequestLog> SetRequestStatusToProcessingAsync(TArgs args)
        {
            var invoiceReferenceId =
                string.Join(",", args.CreateSadadInvoiceJobInputs.Select(s => s.InvoiceReferenceId));

            var sadadInvoiceRequestLog = await _sadadInvoiceRequestLogRepository
                .SingleAsync(v => v.InvoiceReferenceId == invoiceReferenceId);
            sadadInvoiceRequestLog.InProgress();
            await _sadadInvoiceRequestLogRepository.SaveChangesAsync();
            return sadadInvoiceRequestLog;
        }

        private async Task SetResponseAsync(SadadInvoiceRequestLog vIbanRequestLog, SadadInvoiceCreationResponse result)
        {
            var serializedResult = JsonConvert.SerializeObject(result);
            if (result != null)
                vIbanRequestLog.Success(serializedResult);
            else
                vIbanRequestLog.Failed(serializedResult);

            await _sadadInvoiceRequestLogRepository.SaveChangesAsync();
        }

        private async Task CreateSadadInvoiceIfSuccessAsync(SadadInvoiceCreationResponse sadadInvoiceCreationResponse, IntegrationRequestStatusEnum requestStatus)
        {
            if (requestStatus == IntegrationRequestStatusEnum.Successed)
            {
                var createdInvoicesReferences =
                    sadadInvoiceCreationResponse.CreatedSadadInvoices.Select(s => s.ReferenceNumber);

                var sadadInvoices = (await _sadadInvoiceRepository.GetAllAsync(p =>
                    createdInvoicesReferences.Contains(p.Id.ToString()))).ToList();

                sadadInvoices.ForEach(s =>
                {
                    var createdInvoiceNumber = sadadInvoiceCreationResponse.CreatedSadadInvoices
                        .First(c => c.ReferenceNumber == s.Id.ToString()).InvoiceNumber;
                    s.Final(createdInvoiceNumber);
                });
                await _sadadInvoiceRequestLogRepository.SaveChangesAsync();
            }
        }
    }
}
