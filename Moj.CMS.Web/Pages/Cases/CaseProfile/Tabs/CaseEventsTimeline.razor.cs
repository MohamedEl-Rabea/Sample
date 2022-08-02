using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents;
using Moj.CMS.Domain.Shared.Enums;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CaseEventsTimeline
    {
        [Parameter] public IEnumerable<CaseEventsDto> CaseEvents { get; set; }

        private IEnumerable<CaseEventsTimelineDto> _caseEventsTimelineDto => CaseEvents
            .Select(evnt => new CaseEventsTimelineDto
            {
                Date = evnt.Date,
                Operation = evnt.Operation,
                Color = GetOperationColor(evnt.OperationId),
            }).ToList();

        private Color GetOperationColor(int operationId)
        {
            Color color;
            if (OperationColors.TryGetValue((CaseOperationEnum)operationId, out color))
                return color;

            return Color.Default;
        }

        private IDictionary<CaseOperationEnum, Color> OperationColors => new Dictionary<CaseOperationEnum, Color>
        {
            { CaseOperationEnum.CreateCase, Color.Tertiary },
            { CaseOperationEnum.CreateSadadInvoice, Color.Tertiary },
            { CaseOperationEnum.CreateVIban, Color.Tertiary },
            { CaseOperationEnum.AddParty, Color.Secondary },
            { CaseOperationEnum.AddClaim, Color.Secondary },
            { CaseOperationEnum.AddAccusedPartiesToClaim, Color.Secondary },
            { CaseOperationEnum.AddPromissory, Color.Secondary },
            { CaseOperationEnum.EditClaim, Color.Primary },
            { CaseOperationEnum.TerminateCase, Color.Error}
        };
    }

    public class CaseEventsTimelineDto
    {
        public DateTime Date { get; set; }
        public string Operation { get; set; }
        public Color Color { get; set; }
    }
}

