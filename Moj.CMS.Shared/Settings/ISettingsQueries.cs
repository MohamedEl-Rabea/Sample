using System.Threading.Tasks;
using Moj.CMS.Shared.Models.Mail;
using Moj.CMS.Shared.Models.SMS;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Settings
{
    [ScopedService]
    public interface ISettingsQueries
    {
        Task<EmailSettingsDto> GetEmailSettingsAsync();
        Task<SmsSettingsDto> GetSMSSettingsAsync();
    }
}
