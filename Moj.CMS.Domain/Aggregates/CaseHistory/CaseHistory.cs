using Moj.CMS.Domain.ParameterObjects.CaseHistory;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.CaseHistory
{
    public class CaseHistory : FullAuditedAggregateRoot, IAggregateRoot
    {
        public string CaseNumber { get; private set; }
        public CaseOperationEnum Operation { get; private set; }
        public DateTime OperationDateTime { get; private set; }
        public LocalizedText Details { get; private set; }

        public static Task<CaseHistory> CreateHistoryRecordAsync(CreateCaseHistoryParam param)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.CaseNumber, nameof(param.CaseNumber));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.Details, nameof(param.Details));
            Guard.AssertArgumentNotNull(param.Operation, nameof(param.Operation));

            var caseHistory = new CaseHistory
            {
                CaseNumber = param.CaseNumber,
                Operation = param.Operation,
                OperationDateTime = param.OperationDateTime,
                Details = param.Details
            };
            return Task.FromResult(caseHistory);
        }
    }
}
