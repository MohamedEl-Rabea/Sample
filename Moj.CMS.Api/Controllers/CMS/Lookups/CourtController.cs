using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Validation;
using Moj.CMS.Application.AppServices.Court;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Shared.Constants.Permission;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moj.CMS.Api.Controllers.CMS.Lookups
{
    [Authorize(Permissions.Settings.Lookups)]
    public partial class LookupsController
    {
        [HttpPost("api/lookups/court/create")]
        [ProducesResponseType(typeof(Result<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateCourt(IEnumerable<CourtDto> courtList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddOrUpdateCourtCommand { CourtDtoList = courtList, IsCreate = true });
            return Created("", response);
        }

        [HttpPost("api/lookups/court/update")]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> UpdateCourt(IEnumerable<CourtDto> courtList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddOrUpdateCourtCommand { CourtDtoList = courtList, IsCreate = false });
            return Ok(response);
        }
    }
}
