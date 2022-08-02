using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class GetPartyBasicDetailsQuery : Query<Result<PartyListItemDto>>
    {
        public int PartyId { get; set; }
    }

    public class GetPartyBasicDetailsQueryHandler : IRequestHandler<GetPartyBasicDetailsQuery, Result<PartyListItemDto>>
    {
        private readonly IPartyQueries _partyQueries;

        public GetPartyBasicDetailsQueryHandler(IPartyQueries caseQueries)
        {
            _partyQueries = caseQueries;
        }

        public async Task<Result<PartyListItemDto>> Handle(GetPartyBasicDetailsQuery request, CancellationToken cancellationToken)
        {
            var data = await _partyQueries.GetPartyBasicDetailsAsync(request.PartyId);
            return Result<PartyListItemDto>.Success(data);
        }
    }
}
