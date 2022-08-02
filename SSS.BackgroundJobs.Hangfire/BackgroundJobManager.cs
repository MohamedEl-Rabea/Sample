using Hangfire;
using System;
using System.Threading.Tasks;
using SSS.BackgroundJobs.Abstraction;
using RecurringJob = Hangfire.RecurringJob;

namespace SSS.BackgroundJobs.Hangfire
{
    public class BackgroundJobManager : IBackgroundJobManager
    {
        public Task<bool> DeleteJobAsync(string jobId)
        {
            throw new NotImplementedException();
        }

        public Task<string> EnqueueAsync<TArgs>(TArgs args)
        {
            return Task.FromResult(BackgroundJob.Enqueue<HangfireJobExcecutionAdapter<TArgs>>(
                adapter => adapter.ExecuteAsync(args)));
        }

        public  Task<string> ScheduleAsync<TArgs>(TArgs args, DateTimeOffset enqueueAt)
        {
            return Task.FromResult(BackgroundJob.Schedule<HangfireJobExcecutionAdapter<TArgs>>(
               adapter => adapter.ExecuteAsync(args),enqueueAt));
        }

        public Task<string> ScheduleAsync<TArgs>(TArgs args, TimeSpan delay)
        {
            return Task.FromResult(BackgroundJob.Schedule<HangfireJobExcecutionAdapter<TArgs>>(
               adapter => adapter.ExecuteAsync(args), delay));
        }

        public void AddOrUpdateRecurringJob(Abstraction.RecurringJob recurringJob)
        {
            RecurringJob.AddOrUpdate<HangfireJobExcecutionAdapter<object>>(recurringJob.JobId
                , adapter => adapter.ExcetueRecurringAsync(recurringJob.JobType)
                , recurringJob.CronExpression);
        }

        public void RemoveRecurringIfExists(string recurringJobId)
        {
            RecurringJob.RemoveIfExists(recurringJobId);
        }
    }
}
