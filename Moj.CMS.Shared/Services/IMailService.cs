using System.Threading.Tasks;
using Moj.CMS.Shared.Models.Mail;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Services
{
    [TransientService]
    public interface IMailService
    {
        Task SendAsync(MailRequestDto request);
    }
}