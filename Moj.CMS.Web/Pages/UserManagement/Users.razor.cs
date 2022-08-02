using Microsoft.JSInterop;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Users.Queries;
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
    public partial class Users
    {
        public List<UserDto> userList = new List<UserDto>();
        private MudTable<UserDto> table;
        private UserDto user = new UserDto();
        private string searchString = "";
        public List<RowAction<UserDto>> Actions { get; set; } = new List<RowAction<UserDto>>();
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
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Users.Title], href: Urls.UserAdministration.Users.Href, icon: Urls.UserAdministration.Users.Icon),
            });
            await GetUsersAsync();
            SetGridActions();
        }

        private void SetGridActions()
        {
            Actions = new List<RowAction<UserDto>>
            {
                new RowAction<UserDto>
                {
                    Name = Globallocalizer["View Profile"],
                    Icon = Icons.Outlined.RemoveRedEye,
                    Color = Color.Info,
                    Action = user => ViewProfile(user.Id)
                },
                new RowAction<UserDto>
                {
                    Name = Globallocalizer["Manage Roles"],
                    Icon = Icons.Outlined.ManageAccounts,
                    Color = Color.Primary,
                    Action = user => ManageRoles(user.Id)
                }
            };
        }

        private async Task GetUsersAsync()
        {
            var response = await UserAccessModule.ExecuteQueryAsync(new GetAllUsersQuery());
            if (response.Succeeded)
            {
                userList = response.Data.ToList();

            }
            else
            {
                foreach (var error in response.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }
        private bool Search(UserDto user)
        {
            if (string.IsNullOrWhiteSpace(searchString)) return true;
            if (user.FirstName?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.LastName?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.Email?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.PhoneNumber?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.UserName?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
        private async Task ExportToExcel()
        {
            var result = await UserAccessModule.ExecuteQueryAsync(new ExportUsersToExcelQuery());
            var fileInfo = result.Data;
            await _jsRuntime.InvokeAsync<List<UserDto>>("saveAsExcel", $"{localizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }

        public async Task GenerateExcel()
        {
            await ExportToExcel();
        }
        private async Task InvokeModal()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<RegisterUserModal>(localizer["Register New User"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetUsersAsync();
            }
        }

        private void ViewProfile(string userId)
        {
            _navigationManager.NavigateTo($"{Urls.UserAdministration.Users.ProfileHref}{userId}");
        }

        private void ManageRoles(string userId)
        {
            _navigationManager.NavigateTo($"{Urls.UserAdministration.Users.UserRolesHref}{userId}");
        }
    }
}