using Moj.CMS.Application.Integration.Models;
using Moj.CMS.Domain.Aggregates.VIban;
using Moj.CMS.Domain.ParameterObjects.VIban;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Integration.Contracts.AlAhli_B2B;
using SSS.BackgroundJobs.Abstraction;
using System;
using System.Threading.Tasks;

namespace Moj.CMS.Application.Jobs.CreateVIban
{
    //TODO: Jobs must use unit of work wrapper
    public class CreateVIbanBackgroundJob<TArgs> : IAsyncBackgroundJob<TArgs>
        where TArgs : CreateVIbanBackgroundJobArgs
    {
        private readonly IAlahliClient _alahliClient;
        private readonly IRepository<VIbanRequestLog> _vIbanRequestLogRepository;
        private readonly IVIbanRepository _vIbanRepository;

        public CreateVIbanBackgroundJob(IAlahliClient alahliClient, IRepository<VIbanRequestLog> vIbanRequestLogRepository, IVIbanRepository vIbanRepository)
        {
            _alahliClient = alahliClient;
            _vIbanRequestLogRepository = vIbanRequestLogRepository;
            _vIbanRepository = vIbanRepository;
        }

        public async Task ExecuteAsync(TArgs args)
        {
            var vIbanRequestLog = await SetRequestStatusToProcessingAsync(args.ReferenceNumber, args.ReferenceType);

            var result = await _alahliClient.CreateVIban(new VIbanCreationRequest
            {
                ParentAccount = args.ParentAccount,
                Alias = args.Alias,
                Cap = args.CAP
            });

            await SetResponseAsync(vIbanRequestLog, result);
            await CreateVIbanIfSuccessAsync(args, vIbanRequestLog.Status);
        }

        private async Task<VIbanRequestLog> SetRequestStatusToProcessingAsync(string referenceNumber, VIbanReferenceTypeEnum referenceType)
        {
            var vIbanRequestLog = await _vIbanRequestLogRepository.SingleAsync(v => v.ReferenceDetails.ReferenceType == referenceType
            && v.ReferenceDetails.ReferenceNumber == referenceNumber);
            vIbanRequestLog.InProgress();
            await _vIbanRequestLogRepository.SaveChangesAsync();
            return vIbanRequestLog;
        }

        private async Task SetResponseAsync(VIbanRequestLog vIbanRequestLog, string result)
        {
            if (!string.IsNullOrEmpty(result))
                vIbanRequestLog.Success(result);
            else
                vIbanRequestLog.Failed(result);

            await _vIbanRequestLogRepository.SaveChangesAsync();
        }

        private async Task CreateVIbanIfSuccessAsync(TArgs args, IntegrationRequestStatusEnum requestStatus)
        {
            if (requestStatus == IntegrationRequestStatusEnum.Successed)
            {
                var param = new CreateVIbanParam
                {
                    VIbanNumber = $"VIban Number-{DateTime.Now:yyyyMMddHHmmssffff}",
                    Alias = args.Alias,
                    CAP = args.CAP,
                    BankName = "Alahli",
                    ParentAccountNumber = args.ParentAccount,
                    ReferenceDetails = VIbanReferenceDetails.Create(args.ReferenceNumber, args.ReferenceType)
                };
                var vIban = VIban.Create(param);
                await _vIbanRepository.AddAsync(vIban);
                //TODO: assign VIban number to the reference object
                await _vIbanRequestLogRepository.SaveChangesAsync();
            }
        }
    }
}
