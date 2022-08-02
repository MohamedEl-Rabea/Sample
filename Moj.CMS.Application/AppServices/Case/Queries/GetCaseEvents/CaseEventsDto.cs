using Moj.CMS.Shared.CustomAttributes;
using System;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents
{
    public class CaseEventsDto
    {
        [Exportable(Order = 1)]
        public DateTime Date { get; set; }
        public int OperationId { get; set; }
        [Exportable(Order = 2)]
        public string Operation { get; set; }
        [Exportable(Order = 3)]
        public string Details { get; set; }
        [Exportable(Order = 4)]
        public string UserName { get; set; }
    }
}
