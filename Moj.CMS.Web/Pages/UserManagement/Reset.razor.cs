using Microsoft.AspNetCore.WebUtilities;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Users.Commands;
using MudBlazor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Moj.CMS.UserAccess.Application.DTO;
using MudBlazor;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class Reset
    {
        private readonly ResetPasswordDto resetPasswordModel = new();
        protected override void OnInitialized()
        {
            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("Token", out var param))
            {
                var queryToken = param.First();
                resetPasswordModel.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(queryToken));
            }
        }
        private async Task SubmitAsync()
        {
            if (!string.IsNullOrEmpty(resetPasswordModel.Token))
            {
                var result = await  UserAccessModule.ExecuteCommandAsync(new ResetPasswordCommand { ResetPasswordDto = resetPasswordModel });
                if (result.Succeeded)
                {
                    _snackBar.Add(localizer[result.Messages[0]], Severity.Success);
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _snackBar.Add(localizer[error], Severity.Error);
                    }
                }
            }
            else
            {
                _snackBar.Add(localizer["Token Not Found!"], Severity.Error);
            }
        }
        private bool PasswordVisibility;
        private InputType PasswordInput = InputType.Password;
        private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        private void TogglePasswordVisibility()
        {
            if (PasswordVisibility)
            {
                PasswordVisibility = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                PasswordVisibility = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}