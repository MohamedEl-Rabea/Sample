namespace SSS.BackgroundJobs.Abstraction
{
    public class JobExecutionContext {
        public object Job { get; set; }
        public object JobArgs { get; set; }
    }
}
