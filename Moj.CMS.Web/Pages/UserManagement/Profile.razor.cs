using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Moj.CMS.Shared.Enums;
using Moj.CMS.Shared.Models;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Users.Commands;
using Moj.CMS.UserAccess.Application.Services.Users.Queries;
using Moj.CMS.Web.Extensions;
using Moj.CMS.Web.Middlewares;
using MudBlazor;
using SSS.Components.FileUpload;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class Profile
    {
        private char FirstLetterOfName { get; set; }

        private readonly UpdateProfileRequestDto profileModel = new UpdateProfileRequestDto();
        public string UserId { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        private async Task UpdateProfileAsync()
        {
            var response = await UserAccessModule.ExecuteCommandAsync(new UpdateProfileCommand { UserId = UserId, UpdateProfileRequestDto = profileModel });
            if (response.Succeeded)
            {
                var refreshKey = Guid.NewGuid();
                BlazorCookieLoginMiddleware.Refreshs[refreshKey] = UserId;
                _snackBar.Add(localizer["Your Profile has been updated. Please Login to Continue."], Severity.Success);
                _navigationManager.NavigateTo($"/?refreshKey={refreshKey}", true);
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await authenticationStateTask;
            var user = state.User;
            UserId = user.GetUserId();
            var userInfo = await UserAccessModule.ExecuteQueryAsync(new GetUserQuery { UserId = UserId });
            profileModel.Email = user.GetEmail();
            profileModel.FirstName = user.GetFirstName();
            profileModel.LastName = userInfo?.Data?.LastName;
            profileModel.PhoneNumber = userInfo?.Data?.PhoneNumber; //user.GetPhoneNumber();
            profileModel.ProfilePicture = userInfo?.Data?.ProfilePicture;
            if (profileModel.FirstName.Length > 0)
            {
                FirstLetterOfName = profileModel.FirstName[0];
            }
        }

        private void OnProfilePictureSelect(UploadFileInfo fileInfo)
        {
            profileModel.ProfilePicture = new DocumentDto
            {
                DocumentId = profileModel.ProfilePicture?.DocumentId,
                DocumentUrl = fileInfo.URL,
                UploadRequest = new UploadRequest
                {
                    Data = fileInfo.Data,
                    Extension = fileInfo.Extension,
                    UploadType = UploadType.Profile
                }
            };
        }

        private void DeleteImage()
        {
            profileModel.ProfilePicture = new DocumentDto
            {
                DocumentId = profileModel.ProfilePicture?.DocumentId,
                DocumentUrl = string.Empty,
                UploadRequest = null
            };
        }
    }
}