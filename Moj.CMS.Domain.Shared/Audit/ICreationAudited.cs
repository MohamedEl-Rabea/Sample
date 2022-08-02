namespace Moj.CMS.Domain.Shared.Audit
{
    public interface ICreationAudited : IHasCreationTime
    {
        string CreatorUserId { get; set; }
    }
}