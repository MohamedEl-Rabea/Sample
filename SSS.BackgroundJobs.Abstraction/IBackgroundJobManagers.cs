using System;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace SSS.BackgroundJobs.Abstraction
{
    [ScopedService]
    public interface IBackgroundJobManager
    {
        Task<string> EnqueueAsync<TArgs>(TArgs args);
        Task<string> ScheduleAsync<TArgs>(TArgs args, DateTimeOffset enqueueAt);
        Task<string> ScheduleAsync<TArgs>(TArgs args, TimeSpan delay);
        Task<bool> DeleteJobAsync(string jobId);
        void AddOrUpdateRecurringJob(RecurringJob recurringJob);
        void RemoveRecurringIfExists(string recurringJobId);
    }
}
