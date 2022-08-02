using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Validation;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Lookups.Division;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moj.CMS.Api.Controllers.CMS.Lookups
{
    public partial class LookupsController
    {
        [HttpPost("api/lookups/division/create")]
        [ProducesResponseType(typeof(Result<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateDivision(IEnumerable<DivisionDto> divisionList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddOrUpdateDivisionCommand { DivisionDtoList = divisionList, IsCreate = true });
            return Created("", response);
        }

        [HttpPost("api/lookups/division/update")]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> UpdateDivision(IEnumerable<DivisionDto> divisionList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddOrUpdateDivisionCommand { DivisionDtoList = divisionList, IsCreate = false });
            return Ok(response);
        }
    }
}
