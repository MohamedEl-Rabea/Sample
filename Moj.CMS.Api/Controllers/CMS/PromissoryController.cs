using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Validation;
using Moj.CMS.Application.AppServices.Promissory.Commands.AddCasePromissory;
using Moj.CMS.Application.AppServices.Promissory.Queries.GetAllPromissories;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Shared.Constants.Permission;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moj.CMS.Api.Controllers.CMS
{
    [Route("api/case/[controller]/[action]")]
    public class PromissoryController : BaseController
    {
        [HttpPost]
        [Authorize(Permissions.Promissories.Create)]
        [ProducesResponseType(typeof(IResult<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> Create(IEnumerable<AddCasePromissoryDto> casePromissoryDtoList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddCasePromissoryCommand { CasePromissoryList = casePromissoryDtoList });
            return Created("", response);
        }

        [HttpPost]
        [Authorize(Permissions.Cases.View)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get(PromissoriesPagedApiRequest request)
        {
            var response = await CmsModule.ExecuteQueryAsync(new GetAllPromissoriesApiQuery { Request = request });
            return Ok(response);
        }
    }
}
