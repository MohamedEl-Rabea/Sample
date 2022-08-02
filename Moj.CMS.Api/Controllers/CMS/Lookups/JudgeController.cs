﻿using Microsoft.AspNetCore.Mvc;
using Moj.CMS.Api.Validation;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Lookups.Judge;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moj.CMS.Api.Controllers.CMS.Lookups
{
    public partial class LookupsController : BaseController
    {
        [HttpPost("api/lookups/judge/create")]
        [ProducesResponseType(typeof(Result<ResourceCreatedDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateJudge(IEnumerable<JudgeDto> judgeList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddOrUpdateJudgeCommand { JudgeDtoList = judgeList, IsCreate = true });
            return Created("", response);
        }

        [HttpPost("api/lookups/judge/update")]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetailsBase), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> UpdateJudge(IEnumerable<JudgeDto> judgeList)
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddOrUpdateJudgeCommand { JudgeDtoList = judgeList, IsCreate = false });
            return Ok(response);
        }
    }
}
