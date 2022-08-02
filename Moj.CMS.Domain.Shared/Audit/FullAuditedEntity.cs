using System;
using Moj.CMS.Domain.Shared.Entities;

namespace Moj.CMS.Domain.Shared.Audit
{
    public abstract class FullAuditedEntity : FullAuditedEntity<int>, IEntity
    {

    }

    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited
    {
        public virtual bool IsDeleted { get; set; }

        public virtual string DeleterUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }
    }
}
