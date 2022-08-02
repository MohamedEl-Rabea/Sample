using Moj.CMS.Shared.Requests;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetAllCases
{
    public class CasesPagedApiRequest : PagedRequest<CaseListItemDto>
    {
        public CaseFilter CaseFilter { get; set; }

        public override IFilter<CaseListItemDto> GetFilter()
        {
            return CaseFilter;
        }
    }
}
