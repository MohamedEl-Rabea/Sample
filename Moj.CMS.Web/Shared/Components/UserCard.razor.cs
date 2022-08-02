using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Moj.CMS.UserAccess.Application.Services.Users.Queries;
using Moj.CMS.Web.Extensions;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Shared.Components
{
    public partial class UserCard
    {
        [Parameter] public string Class { get; set; }
        private string DisplayName { get; set; }
        private string Email { get; set; }
        private char FirstLetterOfName { get; set; }
        public string ImageDataUrl { get; set; }


        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await authenticationStateTask;
            var user = state.User;
            var userId = user.GetUserId();
            var userInfo = (await UserAccessModule.ExecuteQueryAsync(new GetUserQuery { UserId = userId })).Data;
            if (userInfo != null)
            {
                Email = "M.Mock";
                DisplayName = $"{userInfo.FirstName} {userInfo.LastName}";
                ImageDataUrl = userInfo.ProfilePicture?.DocumentUrl;
            }
        }
    }
}