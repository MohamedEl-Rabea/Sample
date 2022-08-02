using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;

namespace Moj.CMS.Shared.Infrastructure
{
    public abstract class PagedQuery<T> : Query<PagedResult<T>>
    {
        public PagedRequest<T> PagedRequest { get; set; }
    }
}
