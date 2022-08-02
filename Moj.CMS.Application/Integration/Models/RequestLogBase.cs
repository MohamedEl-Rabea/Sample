using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Enums;
using System;

namespace Moj.CMS.Application.Integration.Models
{
    public abstract class RequestLogBase : AuditedEntity
    {
        public IntegrationRequestStatusEnum Status { get; set; }
        public string Request { get; set; }
        public DateTime ScheduledTime { get; set; }
        public DateTime? ProcessingTime { get; set; }
        public DateTime? ResponseTime { get; set; }
        public string Response { get; set; }

    }
}