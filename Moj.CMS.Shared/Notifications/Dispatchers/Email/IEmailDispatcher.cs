using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Notifications.Dispatchers.Email
{
    [ScopedService]
    public interface IEmailDispatcher
    {
        Task DispatchAsync(EmailInput emailInput);
    }
}
