using System;

namespace SSS.BackgroundJobs.Abstraction
{
    public class BackgroundJobExecutionException : Exception
    {
        public string JobType { get; set; }
        public object JobArgs { get; set; }
        public BackgroundJobExecutionException()
        {
        }

        public BackgroundJobExecutionException(string message)
            : base(message)
        {
        }

        public BackgroundJobExecutionException(string message, Exception inner)
            : base(message, inner)
        {
        }
        public BackgroundJobExecutionException(string message, Exception inner, string jobType, object jobArgs)
           : base(message, inner)
        {
            JobType = jobType;
            JobArgs = jobArgs;
        }

    }
}
