using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Application.Integration.Models
{
    public class SadadInvoiceRequestLog : RequestLogBase
    {
        public string InvoiceReferenceId { get; set; }

        public void Success(string response)
        {
            if (Status != IntegrationRequestStatusEnum.Successed)
            {
                Status = IntegrationRequestStatusEnum.Successed;
                Response = response;
                ResponseTime = CLock.Now;
            }
        }

        public void Failed(string response)
        {
            Status = IntegrationRequestStatusEnum.Failed;
            Response = response;
            ResponseTime = CLock.Now;
        }

        public void InProgress()
        {
            if (Status != IntegrationRequestStatusEnum.Processing)
            {
                Status = IntegrationRequestStatusEnum.Processing;
                ProcessingTime = CLock.Now;
            }
        }
    }
}