namespace SSS.BackgroundJobs.Abstraction
{
    public interface IBackgroundJob<TArgs> : IJob<TArgs>
    {
        void Excecute(TArgs args);
    }
}
