using System.Threading.Tasks;

namespace SSS.BackgroundJobs.Abstraction
{
    public interface IRecurringJob
    {
        Task Excecute();
    }
}