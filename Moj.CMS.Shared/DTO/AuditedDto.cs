using System;

namespace Moj.CMS.Shared.DTO
{
    public abstract class AuditedDto
    {
        public bool IsUpdate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
