using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Interfaces;

namespace Moj.CMS.Api.Controllers.UserAccess
{
    [Route("api/authentication/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult> Token(TokenRequestDto model)
        {
            var response = await _tokenService.GenerateAsync(model);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequestDto model)
        {
            var response = await _tokenService.GetRefreshTokenAsync(model);
            return Ok(response);
        }
    }
}
