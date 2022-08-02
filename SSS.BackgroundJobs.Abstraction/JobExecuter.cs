using System;
using System.Threading.Tasks;

namespace SSS.BackgroundJobs.Abstraction
{
    public class JobExecuter : IJobExecuter
    {
        public async Task ExecuteAsync(JobExecutionContext jobExecutionContext)
        {
            await ExecuteJobAsync(jobExecutionContext, nameof(IAsyncBackgroundJob<object>.ExecuteAsync), nameof(IBackgroundJob<object>.Excecute), isRecurring: false);
        }

        public async Task ExecuteRecurringAsync(JobExecutionContext jobExecutionContext)
        {
            await ExecuteJobAsync(jobExecutionContext, nameof(IAsyncRecurringJob.ExcecuteAsync), nameof(IRecurringJob.Excecute), isRecurring: true);
        }

        private async Task ExecuteJobAsync(JobExecutionContext jobExecutionContext, string executeAsyncMethodName, string executeMethodName, bool isRecurring)
        {
            if (jobExecutionContext == null || jobExecutionContext.Job == null)
                throw new Exception("jobExecutionContext or jobExecutionContext.Job can't be null");

            var jobType = jobExecutionContext.Job.GetType();
            var executionsMethod = jobType.GetMethod(executeAsyncMethodName) ?? jobType.GetMethod(executeMethodName);
            if (executionsMethod == null)
                throw new Exception("this type not implement" + (isRecurring ? "IAsyncRecurringJob" : "IAsyncBackgroundJob" + "or")
                    + (isRecurring ? "IRecurringJob" : "IBackgroundJob"));

            try
            {
                var arguments = isRecurring ? new object[0] : new[] { jobExecutionContext.JobArgs };
                if (executionsMethod.Name == executeAsyncMethodName)
                {
                    await (Task)executionsMethod.Invoke(jobExecutionContext.Job, arguments);
                }
                else
                {
                    executionsMethod.Invoke(jobExecutionContext.Job, arguments);
                }
            }
            catch (Exception ex)
            {
                throw new BackgroundJobExecutionException($"Error in backrgound job with type {jobType?.Name} and args {jobExecutionContext?.JobArgs} see the inner exception fot more details", ex)
                {
                    JobType = jobType?.Name,
                    JobArgs = jobExecutionContext?.JobArgs
                };
            }
        }
    }
}
