using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Validation;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Lookups.Area;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moj.CMS.Api.Controllers.CMS.Lookups
{
    public partial class LookupsController
    {
        [HttpPost("api/lookups/area/create")]
        [ProducesResponseType(typeof(Result<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateArea(IEnumerable<AreaDto> AreaList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddOrUpdateAreaCommand { AreaDtoList = AreaList, IsCreate = true });
            return Created("", response);
        }
        [HttpPost("api/lookups/area/update")]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> UpdateArea(IEnumerable<AreaDto> AreaList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddOrUpdateAreaCommand { AreaDtoList = AreaList, IsCreate = false });
            return Ok(response);
        }
    }
}
