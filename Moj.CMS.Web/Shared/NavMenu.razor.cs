using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moj.CMS.Web.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Shared
{
    public partial class NavMenu
    {
        [CascadingParameter]
        public bool Rtl { get; set; }
        public string CurrentHref { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentHref = "/" + _navigationManager.ToBaseRelativePath(_navigationManager.Uri).ToLower();
            SetExpansion(CurrentHref);
            await base.OnInitializedAsync();
        }

        private void SetExpansion(string url)
        {
            Urls.Administration.IsExpanded = IsExpanded(Urls.Administration.SectionUrls, url);
            Urls.Settings.IsExpanded = IsExpanded(Urls.Settings.SectionUrls, url);
            Urls.General.IsExpanded = IsExpanded(Urls.General.SectionUrls, url);
            Urls.UserAdministration.IsExpanded = IsExpanded(Urls.UserAdministration.SectionUrls, url);
        }

        private bool IsExpanded(string[] sectionUrls, string currentUrl)
        {
            return sectionUrls.Any(u => u == "/" ? currentUrl == u : currentUrl.ToLower().StartsWith(u.ToLower()));
        }

        private async Task NavLinkClicked(string href, bool openInNewTab)
        {
            if (openInNewTab)
                await _jsRuntime.InvokeAsync<object>("open", href, "_blank");
            else
            {
                _navigationManager.NavigateTo(href);
                CurrentHref = href;
            }

            SetExpansion(href);
        }
    }
}
