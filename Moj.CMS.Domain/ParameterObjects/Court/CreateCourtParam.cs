using Moj.CMS.Domain.Aggregates.Court.Entities;
using Moj.CMS.Domain.Aggregates.Court.ValueObjects;
using System.Collections.Generic;

namespace Moj.CMS.Domain.ParameterObjects.Court
{
    public class CourtInfoParam
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string AreaCode { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<CourtBankAccount> BanckAccounts { get; set; }
        public IEnumerable<Division> Divisions { get; set; }
    }
}