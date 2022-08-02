using System.Threading.Tasks;
using Moj.CMS.UserAccess.Application.DTO;
using MudBlazor;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class Forgot
    {
        private readonly ForgotPasswordRequestDto emailModel = new();
        private async Task SubmitAsync()
        {
            //var result = await _userService.ForgotPasswordAsync(emailModel);
            //if (result.Succeeded)
            //{
            //    _snackBar.Add(localizer["Done!"], Severity.Success);
            //    _navigationManager.NavigateTo("/");
            //}
            //else
            //{
            //    foreach (var message in result.Messages)
            //    {
            //        _snackBar.Add(localizer[message], Severity.Error);
            //    }
            //}
        }
    }
}