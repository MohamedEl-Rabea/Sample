using Moj.CMS.Shared.Requests;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartiesPagedApiRequest : PagedRequest<PartyListItemDto>
    {
        public PartyFilter PartyFilter { get; set; }
        public override IFilter<PartyListItemDto> GetFilter()
        {
            return PartyFilter;
        }
    }
}
