using System.Threading.Tasks;
using Moj.CMS.Shared.Models.Mail;
using Moj.CMS.Shared.Models.SMS;
using Moj.CMS.Shared.Wrapper;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Services
{
    [TransientService]
    public interface ISettingsManager
    {
        Task<IResult> SaveEmailSettingsAsync(EmailSettingsDto emailSettingsDto);
        Task<IResult> SaveSmsSettingsAsync(SmsSettingsDto smsSettingsDto);
    }
}
