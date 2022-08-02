using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.DTO;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim
{
    public class ClaimDto
    {
        public DebtTypeEnum DebtTypeId { get; set; }
        public DateTime ClaimDateTime { get; set; }
        public string ComplaintPartyNumber { get; set; }
        public bool IsJoint { get; set; }
        public MoneyDto BasicAmount { get; set; }
        public MoneyDto RequiredAmount { get; set; }
        public MoneyDto RemainingAmount { get; set; }
        public IEnumerable<ClaimDetailsDto> ClaimDetails { get; set; } = new List<ClaimDetailsDto>();
    }
}