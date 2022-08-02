using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CreateNewDebt
{
    public class CreateNewDebtDto
    {
        public string ClaimNumber { get; set; }
        public ICollection<ClaimDetailsDto> ClaimDetailsList { get; set; }
    }
}
