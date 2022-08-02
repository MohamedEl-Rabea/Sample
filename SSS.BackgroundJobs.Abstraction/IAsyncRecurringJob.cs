using System.Threading.Tasks;

namespace SSS.BackgroundJobs.Abstraction
{
    public interface IAsyncRecurringJob
    {
        Task ExcecuteAsync();
    }
}
