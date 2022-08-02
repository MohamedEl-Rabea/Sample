using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Domain.Shared.Audit
{
    public abstract class CreationAuditedAggregateRoot : CreationAuditedAggregateRoot<int>
    {

    }
    public abstract class CreationAuditedAggregateRoot<TPrimaryKey> : AggregateRoot<TPrimaryKey>, ICreationAudited
    {
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }

        protected CreationAuditedAggregateRoot()
        {
            CreationTime = CLock.Now;
        }
    }
}
