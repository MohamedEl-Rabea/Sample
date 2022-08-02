using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;
using Moj.CMS.UserAccess.Application.Services.Interfaces;
using System.Threading.Tasks;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Wrapper;

namespace Moj.CMS.UserAccess.Application.Services.Implementation
{ 
    public class AuthenticationAppService : IAuthenticationAppService
    {
        private readonly UserManager<CMSUser> _userManager;
        private readonly SignInManager<CMSUser> _signInManager;
        private readonly IStringLocalizer<UsersLocalizer> _localizer;


        public AuthenticationAppService(UserManager<CMSUser> userManager, SignInManager<CMSUser> signInManager, IStringLocalizer<UsersLocalizer> userLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = userLocalizer;
        }

        /// <summary>
        /// This method check only if the user signable or not, without writing cookies into response headers.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<IResult<string>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Result<string>.Fail(_localizer["Invalid login user name / password"]);
        
            if (!user.IsActive)
                return Result<string>.Fail(_localizer["User Not Active. Please contact the administrator."]);
            
            var result =
            await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
            if (result.Succeeded)
                return Result<string>.Success(data: user.UserName);

            return Result<string>.Fail(_localizer["Invalid login user name / password"]);
        }
    }
}
