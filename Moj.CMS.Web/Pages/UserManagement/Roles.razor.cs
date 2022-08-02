using Microsoft.AspNetCore.Components;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Roles.Commands;
using Moj.CMS.UserAccess.Application.Services.Roles.Queries;
using Moj.CMS.Web.Constants;
using MudBlazor;
using SSS.Components.RowActions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class Roles
    {
        public List<RoleDto> roleList = new List<RoleDto>();
        private RoleDto role = new RoleDto();
        private string searchString = "";
        private MudTable<RoleDto> table;
        [CascadingParameter]
        public bool Rtl { get; set; }

        public List<RowAction<RoleDto>> Actions { get; set; } = new List<RowAction<RoleDto>>();
        public IconStyle IconStyle = new IconStyle
        {
            Size = Size.Small,
            Variant = Variant.Text
        };

        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Roles.Title], href: Urls.UserAdministration.Roles.Href, icon: Urls.UserAdministration.Roles.Icon),
            });
            await GetRolesAsync();
            SetGridActions();
        }

        private void SetGridActions()
        {
            Actions = new List<RowAction<RoleDto>>
            {
                new RowAction<RoleDto>
                {
                    Name = Globallocalizer["Manage Permission"],
                    Icon = Icons.Filled.Settings,
                    Color = Color.Info,
                    Action =  role=> ManagePermissions(role.Id)
                },
                new RowAction<RoleDto>
                {
                    Name = Globallocalizer["Edit"],
                    Icon = Icons.Filled.Edit,
                    Color = Color.Secondary,
                    Action = async   role=> await InvokeModal(role.Id)
                },
                new RowAction<RoleDto>
                {
                    Name = Globallocalizer["Delete"],
                    Icon = Icons.Filled.Delete,
                    Color = Color.Error,
                    Action = async   role=> await Delete(role.Id)
                }
            };
        }

        private async Task GetRolesAsync()
        {
            var response = await UserAccessModule.ExecuteQueryAsync(new GetAllRolesQuery());
            if (response.Succeeded)
            {
                roleList = response.Data.ToList();
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }

        private async Task Delete(string id)
        {
            string deleteContent = Globallocalizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText),$"{deleteContent}" }
            };
            var options = new DialogOptions
            { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog =
                _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(Globallocalizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await UserAccessModule.ExecuteCommandAsync(new DeleteRoleCommand { RoleId = id });
                if (response.Succeeded)
                {
                    _snackBar.Add(localizer[response.Messages[0]], Severity.Success);
                }
                else
                {
                    foreach (var error in response.Errors)
                    {
                        _snackBar.Add(localizer[error], Severity.Error);
                    }
                }
                await Reset();
            }
        }

        private async Task InvokeModal(string id = null)
        {
            var parameters = new DialogParameters();
            if (id != null)
            {
                role = roleList.FirstOrDefault(c => c.Id == id);
                if (role != null)
                {
                    parameters.Add(nameof(RoleModal.RoleModel), new RoleDto
                    {
                        Id = role.Id,
                        Name = role.Name
                    });
                }
            }

            var options = new DialogOptions
            { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<RoleModal>(id == null ? Globallocalizer["Create"] : Globallocalizer["Edit"],
                parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            role = new RoleDto();
            await GetRolesAsync();
            StateHasChanged();
        }

        private bool Search(RoleDto role)
        {
            if (string.IsNullOrWhiteSpace(searchString)) return true;
            if (role.Name?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            return false;
        }

        private void ManagePermissions(string roleId)
        {
            _navigationManager.NavigateTo($"{Urls.UserAdministration.Roles.RolePermissions}{roleId}");
        }
    }
}
