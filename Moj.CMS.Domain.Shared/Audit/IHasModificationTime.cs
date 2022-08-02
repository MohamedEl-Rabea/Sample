using System;

namespace Moj.CMS.Domain.Shared.Audit
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}