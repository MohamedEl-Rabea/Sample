using Microsoft.AspNetCore.Components;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Roles.Commands;
using Moj.CMS.UserAccess.Application.Services.Roles.Queries;
using Moj.CMS.Web.Constants;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class RolePermissions
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Description { get; set; }
        public PermissionResponseDto model { get; set; }
        private string searchString = "";
        List<PermissionDto> SelectedItems = new();

        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Roles.Title], href: Urls.UserAdministration.Roles.Href, icon: Urls.UserAdministration.Roles.Icon),
            });
            var result = await UserAccessModule.ExecuteQueryAsync(new GetAllPermissionsQuery() { RoleId = Id });
            if (result.Succeeded)
            {
                model = result.Data;
                var selected = result.Data.Permissions.Where(p => p.Selected).ToList();
                SelectedItems = selected;
                if (model != null)
                {
                    Description = $"{localizer["Manage Permissions for"]} : {model.RoleName}";
                    AddBreadItem(localizer["Manage Permissions for"], href: "", disabled: true);
                }
            }
        }
        private async Task SaveAsync()
        {
            var selectedValues = new List<PermissionDto>();
            foreach (var item in SelectedItems)
            {
                if (string.IsNullOrEmpty(item.ParentCode))
                {
                    selectedValues.Add(item);
                    selectedValues.RemoveAll(p => p.ParentCode == item.Code);
                }
                else
                {
                    if (!selectedValues.Any(p => p.Code == item.ParentCode))
                        selectedValues.Add(item);
                }
            }
            var permissionRequestDto = new PermissionRequestDto()
            {
                RoleId = model.RoleId,
                RolePermissions = selectedValues.Select(c => c.Code).ToList()
            };
            var result = await UserAccessModule.ExecuteCommandAsync(new UpdatePermissionsCommand { PermissionRequestDto = permissionRequestDto });
            if (result.Succeeded)
            {
                _snackBar.Add(localizer[result.Messages[0]], Severity.Success);
                _navigationManager.NavigateTo($"{Urls.UserAdministration.Roles.Href}");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }
    }
}