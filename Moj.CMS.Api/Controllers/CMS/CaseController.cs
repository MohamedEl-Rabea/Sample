using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Validation;
using Moj.CMS.Application.AppServices.Case.Commands.AddCase;
using Moj.CMS.Application.AppServices.Case.Commands.AddCaseParty;
using Moj.CMS.Application.AppServices.Case.Commands.UpdateCaseCourtDetailsCommand;
using Moj.CMS.Application.AppServices.Case.Queries.GetAllCases;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Shared.Constants.Permission;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moj.CMS.Api.Controllers.CMS
{
    [Route("api/[controller]/")]
    public class CaseController : BaseController
    {
        [HttpPost("")]
        [Authorize(Permissions.Cases.Create)]
        [ProducesResponseType(typeof(IResult<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> Post(IEnumerable<CaseFullDetailsDto> newCaseInputList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddCaseListCommand { NewCaseInputList = newCaseInputList });
            return Created("", response);
        }


        [HttpPost("[action]")]
        [Authorize(Permissions.Cases.ChangeCourtDetails)]
        [ProducesResponseType(typeof(IResult<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Move(UpdateCaseCourtDetailsDto inputDto)
        {
            var response = await CmsModule.ExecuteCommandAsync(new UpdateCaseCourtDetailsCommand { UpdateCaseCourtDetailsDto = inputDto });
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(Permissions.Cases.View)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get(CasesPagedApiRequest request)
        {
            var response = await CmsModule.ExecuteQueryAsync(new GetAllCasesApiQuery { Request = request });
            return Ok(response);
        }

        #region Case Parties

        [HttpPost("party")]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Post(IEnumerable<AddCasePartiesDto> addCasePartiesDtoList)
        {
            var result = await CmsModule.ExecuteCommandAsync(new AddCasePartiesCommand { AddCasePartiesDtoList = addCasePartiesDtoList });
            return Created("", result);
        }
        #endregion
    }
}
