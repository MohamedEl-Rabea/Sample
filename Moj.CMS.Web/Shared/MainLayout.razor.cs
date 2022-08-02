using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Moj.CMS.Web.Shared.Services;
using Moj.CMS.Web.Theme;
using MudBlazor;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Shared
{
    public partial class MainLayout
    {
        [Inject]
        protected ISharedState SharedState { get; set; }

        private List<BreadcrumbItem> BreadcrumbItems => SharedState.BreadcrumbItems;

        private MudTheme _currentTheme = new GreenTheme();
        private bool _drawerOpen = true;

        private bool _rightToLeft;

        private void Logout()
        {
            string logoutConfirmationText = Globallocalizer["Do you really want to logout?"];
            string logoutText = Globallocalizer["Logout"];
            var parameters = new DialogParameters();
            parameters.Add("ContentText", logoutConfirmationText);
            parameters.Add("ButtonText", logoutText);
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Dialogs.Logout>("Logout", parameters, options);
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task DarkMode()
        {
            _currentTheme = new GreenTheme();
        }

        protected override Task OnInitializedAsync()
        {
            _rightToLeft = Thread.CurrentThread.CurrentCulture.Name.ToLower().Contains("ar");
            return base.OnInitializedAsync();
        }
    }
}
