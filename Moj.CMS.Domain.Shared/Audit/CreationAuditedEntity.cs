using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Domain.Shared.Audit
{
    public abstract class CreationAuditedEntity : CreationAuditedEntity<int>, IEntity
    {

    }

    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
        public virtual DateTime CreationTime { get; set; }

        public virtual string CreatorUserId { get; set; }

        protected CreationAuditedEntity()
        {
            CreationTime = CLock.Now;
        }
    }
}