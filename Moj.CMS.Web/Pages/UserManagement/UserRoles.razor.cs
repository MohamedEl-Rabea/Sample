using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Users.Commands;
using Moj.CMS.UserAccess.Application.Services.Users.Queries;
using Moj.CMS.Web.Constants;
using MudBlazor;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class UserRoles
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Description { get; set; }
        public List<UserRoleModel> UserRolesList { get; set; } = new();
        private UserRoleModel userRole = new();
        private string searchString = "";
        private bool _dense = true;
        private bool _striped = true;
        private bool _bordered = false;
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Users.Title], href: Urls.UserAdministration.Users.Href, icon: Urls.UserAdministration.Users.Icon),
            });

            var userId = Id;
            var result = await UserAccessModule.ExecuteQueryAsync(new GetUserQuery { UserId = userId });
            if (result.Succeeded)
            {
                var user = result.Data;
                if (user != null)
                {
                    Title = $"{localizer["User Roles"]} : {user.FirstName} {user.LastName}";
                    AddBreadItem(localizer["User Roles"], href: "", true);
                    Description = $"{localizer["Manage"]} {user.FirstName} {user.LastName}'s {localizer["Roles"]}";
                    var response = await UserAccessModule.ExecuteQueryAsync(new GetUserRolesQuery { UserId = user.Id });
                    UserRolesList = response.Data.UserRoles;
                }
            }
        }
        private async Task SaveAsync()
        {
            var request = new UpdateUserRolesRequestDto()
            {
                UserId = Id,
                UserRoles = UserRolesList
            };
            var result = await UserAccessModule.ExecuteCommandAsync(new UpdateUserRolesCommand { UpdateUserRolesRequestDto = request });
            if (result.Succeeded)
            {
                _snackBar.Add(localizer[result.Messages[0]], Severity.Success);
                _navigationManager.NavigateTo($"{Urls.UserAdministration.Users.Href}");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }
        private bool Search(UserRoleModel userRole)
        {
            if (string.IsNullOrWhiteSpace(searchString)) return true;
            if (userRole.RoleName?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}