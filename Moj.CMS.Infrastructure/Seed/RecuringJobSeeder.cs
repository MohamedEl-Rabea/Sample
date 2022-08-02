using Moj.CMS.Shared.Infrastructure.Seed;
using SSS.BackgroundJobs.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cron = Hangfire.Cron;

namespace Moj.CMS.Infrastructure.Seed
{
    public class RecuringJobSeeder : IDatabaseSeeder
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public RecuringJobSeeder(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        private IEnumerable<RecurringJob> RecurringJobs => new List<RecurringJob>
        {
           new RecurringJob
           {
               JobType = typeof(job),
               CronExpression = Cron.Daily(),
               IsActive = true,
               JobId = typeof(job)
           }
        };

        public Task SeedAsync()
        {
            foreach (var job in RecurringJobs)
            {
                if (job.IsActive)
                    _backgroundJobManager.AddOrUpdateRecurringJob(job);
                else
                    _backgroundJobManager.RemoveRecurringIfExists(job.JobId);
            }
            return Task.CompletedTask;
        }
    }
}
