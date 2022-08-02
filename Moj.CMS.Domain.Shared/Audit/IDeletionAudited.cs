namespace Moj.CMS.Domain.Shared.Audit
{
    public interface IDeletionAudited : IHasDeletionTime
    {
        string DeleterUserId { get; set; }
    }
}