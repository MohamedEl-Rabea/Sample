using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Validation;
using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Shared.Constants.Permission;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moj.CMS.Api.Controllers.CMS
{
    [Route("api/[controller]/")]
    public class PartyController : BaseController
    {
        [HttpPost("[action]")]
        [Authorize(Permissions.Parties.Create)]
        [ProducesResponseType(typeof(IResult<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Create(IEnumerable<PartyDto> parties)
        {
            var result = await CmsModule.ExecuteCommandAsync(new AddPartiesCommand { Parties = parties });
            return Created("", result);
        }

        [HttpPost("[action]")]
        [Authorize(Permissions.Parties.View)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get(PartiesPagedApiRequest request)
        {
            var response = await CmsModule.ExecuteQueryAsync(new GetAllPartiesApiQuery { Request = request });
            return Ok(response);
        }
    }
}
