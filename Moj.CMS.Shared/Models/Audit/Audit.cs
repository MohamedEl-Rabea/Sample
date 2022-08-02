using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Shared.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Moj.CMS.Shared.Models.Audit
{
    [IgnoreAuditing]
    public class Audit : CreationAuditedEntity
    {
        public Guid RequestId { get; set; }
        public string RequestName { get; set; }
        [MaxLength(256)]
        public string UserId { get; set; }

        [MaxLength(256)]
        public string Type { get; set; }

        [MaxLength(256)]
        public string EntityName { get; set; }

        public DateTime DateTime { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        public string AffectedColumns { get; set; }

        [MaxLength(256)]
        public string EntityId { get; set; }
    }
}