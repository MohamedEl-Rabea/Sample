namespace Moj.CMS.Domain.Shared.Audit
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}