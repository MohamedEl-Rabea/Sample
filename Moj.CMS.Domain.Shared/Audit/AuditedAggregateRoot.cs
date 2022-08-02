using System;

namespace Moj.CMS.Domain.Shared.Audit
{
    public abstract class AuditedAggregateRoot : AuditedAggregateRoot<int>
    {

    }

    public abstract class AuditedAggregateRoot<TPrimaryKey> : CreationAuditedAggregateRoot<TPrimaryKey>
    {
        public DateTime? LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }

    }
}