using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace SSS.BackgroundJobs.Abstraction
{
    [TransientService]
    public interface IJob<TArgs>
    {
    }
}
