using MediatR;
using Moj.CMS.Application.AppServices.CaseHistory.Services;
using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.ParameterObjects.CaseHistory;
using Moj.CMS.Domain.Shared.Events;
using Moj.CMS.Domain.Shared.Values;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.CaseHistory.Handlers
{
    public abstract class CaseChangedDomainEventHandlerBase<TDomainEvent> : DomainEventHandler<TDomainEvent>
        where TDomainEvent : CaseChangedDomainEventBase
    {
        private readonly ICaseHistoryService _caseHistoryService;

        public CaseChangedDomainEventHandlerBase(ICaseHistoryService caseHistoryService)
        {
            _caseHistoryService = caseHistoryService;
        }

        public abstract Task<LocalizedText> GetDetailsAsync(TDomainEvent domainEvent);

        public override async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            await _caseHistoryService.AddCaseLogEntryAsync(new CreateCaseHistoryParam
            {
                CaseNumber = notification.CaseNumber,
                Operation = notification.Operation,
                OperationDateTime = notification.OperationDateTime,
                Details = await GetDetailsAsync(notification)
            });
        }
    }
}
