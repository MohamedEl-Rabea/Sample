using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Users.Commands;
using MudBlazor;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class RegisterUserModal  
    {
        private  RegisterDto registerUserModel = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        private void Cancel()
        {
            MudDialog.Cancel();
        }
        private async Task SubmitAsync()
        {
            var response = await CmsModule.ExecuteCommandAsync(new AddUserCommand{ RegisterDto= registerUserModel});
            if (response.Succeeded)
            {
                _snackBar.Add(localizer[response.Messages[0]], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
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