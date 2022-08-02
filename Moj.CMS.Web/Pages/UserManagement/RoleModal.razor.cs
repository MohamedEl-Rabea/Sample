using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Roles.Commands;
using MudBlazor;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class RoleModal
    {
        [Parameter]
        public RoleDto RoleModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        public void Cancel()
        {
            MudDialog.Cancel();
        }
        private async Task SaveAsync()
        {
            var response = await UserAccessModule.ExecuteCommandAsync(new AddRoleCommand {RoleDto=RoleModel });
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
        }
    }
}