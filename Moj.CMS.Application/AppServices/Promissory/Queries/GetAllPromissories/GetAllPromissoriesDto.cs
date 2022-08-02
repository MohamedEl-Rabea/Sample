using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Promissory.Queries
{
    public class GetAllPromissoriesDto : AuditedDto
    {
        [Exportable(Order = 1)]
        public string PromissoryNumber { get; set; }
        [Exportable(Order = 2)]
        public string PromissoryType { get; set; }

        [Exportable(Order = 3)]
        public DateTime PromissoryDate { get; set; }
        [Exportable(Order = 4)]
        public int NumberOfCases { get; set; }
        [Exportable(Order = 5)]
        public int NumberOfParties { get; set; }
        [Exportable(Order = 6)]
        public int NumberOfClaims { get; set; }

        public PromissoryTypeEnum TypeId { get; set; }
    }
}
