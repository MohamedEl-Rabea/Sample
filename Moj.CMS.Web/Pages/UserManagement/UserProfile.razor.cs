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
    public partial class UserProfile
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Description { get; set; }
        public bool Active { get; set; }
        private char FirstLetterOfName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Color AvatarButtonColor { get; set; } = Color.Error;
        public IEnumerable<string> Errors { get; set; }

        private async Task ToggleUserStatus()
        {
            var request = new ToggleUserStatusDto { ActivateUser = Active, UserId = Id };
            var result = await UserAccessModule.ExecuteCommandAsync(new ToggleUserStatusCommand { ToggleUserStatusDto = request });
            if (result.Succeeded)
            {
                _snackBar.Add(localizer["Updated User Status."], Severity.Success);
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
        [Parameter]
        public string ImageDataUrl { get; set; }
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.UserAdministration.Users.Title], href: Urls.UserAdministration.Users.Href, icon: Urls.UserAdministration.Users.Icon),
            });
            var result = await UserAccessModule.ExecuteQueryAsync(new GetUserQuery { UserId = Id });
            if (result.Succeeded)
            {
                var user = result.Data;
                if (user != null)
                {
                    FirstName = user.FirstName;
                    LastName = user.LastName;
                    Email = user.Email;
                    PhoneNumber = user.PhoneNumber;
                    Active = user.IsActive;
                    ImageDataUrl = user.ProfilePicture?.DocumentUrl;
                }
                Title = $"{localizer["Profile"]} :  {FirstName} {LastName}";
                AddBreadItem(localizer["Profile"], href: "", true);
                Description = Email;
                if (FirstName.Length > 0)
                {
                    FirstLetterOfName = FirstName[0];
                }
            }
        }
    }
}