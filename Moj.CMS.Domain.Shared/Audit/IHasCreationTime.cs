using System;

namespace Moj.CMS.Domain.Shared.Audit
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}