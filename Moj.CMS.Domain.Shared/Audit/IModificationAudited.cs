namespace Moj.CMS.Domain.Shared.Audit
{
    public interface IModificationAudited : IHasModificationTime
    {
        string LastModifierUserId { get; set; }
    }
}