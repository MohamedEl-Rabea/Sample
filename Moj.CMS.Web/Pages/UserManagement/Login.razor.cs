using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Services.Interfaces;
using Moj.CMS.Web.Middlewares;
using MudBlazor;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public partial class Login
    {
        private LoginDto _loginDto = new LoginDto();

        [Inject]
        public IAuthenticationAppService AuthenticationAppService { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            if (authenticationState.User.Identity.IsAuthenticated)
                _navigationManager.NavigateTo("/");
        }

        /// <summary>
        /// Upon login form submitting, signalR request sent to the server, hence we cannot write the authentication cookies
        /// into the response header, you can find more details about this issue here https://github.com/dotnet/aspnetcore/issues/13601
        /// </summary>
        /// <returns></returns>
        private async Task SubmitAsync()
        {
            //Check if the user valid to login, just check without actual login
            var result = await AuthenticationAppService.Login(_loginDto);
            if (result.Succeeded)
            {
                //Writing login details into the custom middleware to be reachable in our response handling
                Guid key = Guid.NewGuid();
                BlazorCookieLoginMiddleware.Logins[key] = new LoginDto
                { Email = _loginDto.Email, Password = _loginDto.Password, UserName = result.Data };

                _navigationManager.NavigateTo($"/login?key={key}", true);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
        }

        private void FillAdministratorCredentials()
        {
            _loginDto.Email = "admin@cms.com";
            _loginDto.Password = "123Pa$$word!";
        }
    }
}