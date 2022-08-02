using Moj.CMS.Domain.Shared.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Models.Currency
{
    public class Currency : FullAuditedEntity
    {
        [MaxLength(3)]
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal ExRate { get; set; }
        public bool IsLocal { get; set; }
    }
}
