using Moj.CMS.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.Dtos
{
    public class CasePartyRolesInfo
    {
        public string PartyNumber { get; set; }
        public PartyRoleEnum PartyRole { get; set; }
    }
}
