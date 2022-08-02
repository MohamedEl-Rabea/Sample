using System.Threading.Tasks;

namespace SSS.BackgroundJobs.Abstraction
{
    public interface IAsyncBackgroundJob<TArgs> : IJob<TArgs>
    {
        Task ExecuteAsync(TArgs args);
    }
}
