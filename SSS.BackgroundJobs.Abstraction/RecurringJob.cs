using System;

namespace SSS.BackgroundJobs.Abstraction
{
    public class RecurringJob
    {
        public string JobId { get; set; }
        public Type JobType { get; set; }
        public string CronExpression { get; set; }
        public bool IsActive { get; set; }
    }
}
