using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using SSS.BackgroundJobs.Abstraction;

namespace SSS.BackgroundJobs.Hangfire
{

    public class HangfireJobExcecutionAdapter<TArgs>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IJobExecuter _jobExecuter;

        public HangfireJobExcecutionAdapter(IServiceProvider serviceProvider, IJobExecuter jobExecuter)
        {
            _serviceProvider = serviceProvider;
            _jobExecuter = jobExecuter;
        }

        public async Task ExecuteAsync(TArgs args)
        {
            using var scope = _serviceProvider.CreateScope();
            var job = scope.ServiceProvider.GetService<IJob<TArgs>>();
            var jobExecutionContext = new JobExecutionContext
            {
                Job = job,
                JobArgs = args
            };
            await _jobExecuter.ExecuteAsync(jobExecutionContext);
        }

        public async Task ExcetueRecurringAsync(Type jobType)
        {
            var job = _serviceProvider.GetService(jobType);

            var jobExecutionContext = new JobExecutionContext
            {
                Job = job,
            };
            await _jobExecuter.ExecuteRecurringAsync(jobExecutionContext);
        }
    }
}
