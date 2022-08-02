using Moj.CMS.Shared.Requests;

namespace Moj.CMS.Application.AppServices.Promissory.Queries.GetAllPromissories
{
    public class PromissoriesPagedApiRequest : PagedRequest<GetAllPromissoriesDto>
    {
        public PromissoryFilter PromissoryFilter { get; set; }

        public override IFilter<GetAllPromissoriesDto> GetFilter()
        {
            return PromissoryFilter;
        }
    }
}
