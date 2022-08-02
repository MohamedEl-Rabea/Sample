using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Moj.CMS.Shared.Models.Mail;
using Moj.CMS.Shared.Services;
using Moj.CMS.Shared.Settings;
using MudBlazor;

namespace Moj.CMS.Web.Pages.Settings.Notifications
{
    public partial class EmailSettings
    {
        [Inject] protected ISettingsManager SettingsManager { get; set; }
        [Inject] protected ISettingsQueries SettingsQueries { get; set; }

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        public EmailSettingsDto EmailSettingsDto { get; set; } = new EmailSettingsDto();

        protected override async Task OnInitializedAsync()
        {
            var emailSettingsDto = await SettingsQueries.GetEmailSettingsAsync();
            EmailSettingsDto = emailSettingsDto ?? new EmailSettingsDto();
            await base.OnInitializedAsync();
        }

        protected async Task SaveAsync()
        {
            var response = await SettingsManager.SaveEmailSettingsAsync(EmailSettingsDto);
            if (response.Succeeded)
            {
                _snackBar.Add(localizer["Email settings updated successfully."], Severity.Success);
            }
            else
            {
                foreach (var error in response.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }

        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
    }
}
