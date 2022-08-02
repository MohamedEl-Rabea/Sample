using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace SSS.BackgroundJobs.Abstraction
{
    [ScopedService]
    public interface IJobExecuter
    {
        Task ExecuteAsync(JobExecutionContext jobExecutionContext);
        Task ExecuteRecurringAsync(JobExecutionContext jobExecutionContext);
    }
}
