using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Validation;
using Moj.CMS.Application.AppServices.Case.Commands.ClaimIncreaseDicrease;
using Moj.CMS.Application.AppServices.Claims.Commands.ClaimIncreaseDicrease;
using Moj.CMS.Application.AppServices.Claims.Commands.CloseClaim;
using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Application.AppServices.Claims.Commands.CreateNewDebt;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Shared.Constants.Permission;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moj.CMS.Api.Controllers.CMS
{
    [Route("api/case/claim/")]
    public class ClaimsController : BaseController
    {
        public ClaimsController()
        {

        }

        [HttpPost("[action]")]
        [Authorize(Permissions.Claims.Create)]
        [ProducesResponseType(typeof(IResult<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> Create(IEnumerable<CreateClaimDto> claimDtoList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new CreateClaimCommand { ClaimDtoList = claimDtoList });
            return Created("", response);
        }

        [HttpPost("debt/create")]
        [Authorize(Permissions.Claims.AddNewDebt)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateNewDebt(IEnumerable<CreateNewDebtDto> DebtDto)
        {
            var response = await CmsModule.ExecuteCommandAsync(new CreateNewDebtCommand { CreateNewDebtDtoList = DebtDto });
            return Created("", response);
        }

        [HttpPost("[action]")]
        [Authorize(Permissions.Claims.IncrementOrDecrement)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> Effect(ClaimEffectInputDto claimEffectInput)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddClaimEffectCommand { ClaimEffectInput = claimEffectInput });
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(Permissions.Claims.Close)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> Close(CloseClaimDto closeClaimDto)
        {
            var response = await CmsModule.ExecuteCommandAsync(new CloseClaimCommand { CloseClaimDto = closeClaimDto });
            return Ok(response);
        }
    }
}
