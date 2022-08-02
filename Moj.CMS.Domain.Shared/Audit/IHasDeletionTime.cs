using System;

namespace Moj.CMS.Domain.Shared.Audit
{
    public interface IHasDeletionTime : ISoftDelete
    {
        DateTime? DeletionTime { get; set; }
    }
}