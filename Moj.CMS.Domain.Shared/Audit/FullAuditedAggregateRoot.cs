using System;

namespace Moj.CMS.Domain.Shared.Audit
{
    public abstract class FullAuditedAggregateRoot : FullAuditedAggregateRoot<int>
    {

    }


    /// <summary>
    /// This class can be used to simplify implementing <see cref="IFullAudited"/> for aggregate roots.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class FullAuditedAggregateRoot<TPrimaryKey> : AuditedAggregateRoot<TPrimaryKey>, IFullAudited
    {
        public virtual bool IsDeleted { get; set; }

        public virtual string DeleterUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }
    }
}