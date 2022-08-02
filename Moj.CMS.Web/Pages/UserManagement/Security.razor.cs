using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Users.Commands;
using Moj.CMS.Web.Extensions;
using MudBlazor;

namespace Moj.CMS.Web.Pages.UserManagement
{

    public partial class Security  
    {
        private readonly ChangePasswordRequestDto passwordModel = new();
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        private async Task ChangePasswordAsync()
        {
            var state = await authenticationStateTask;
            var user = state.User;
            var UserId = user.GetUserId();
            var response = await UserAccessModule.ExecuteCommandAsync(new ChangePasswordCommand {ChangePasswordRequestDto=passwordModel,UserId=UserId });
            if (response.Succeeded)
            {
                _snackBar.Add(localizer["Password Changed!"], Severity.Success);
                passwordModel.Password = string.Empty;
                passwordModel.NewPassword = string.Empty;
                passwordModel.ConfirmNewPassword = string.Empty;
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }
    }
}