using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Notifications.Dispatchers.SMS
{
    [ScopedService]
    public interface ISMSDispatcher
    {
        Task DispatchAsync(SMSInput emailInput);
    }
}
