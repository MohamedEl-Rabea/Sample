using System.Threading.Tasks;

namespace Moj.CMS.Domain.Shared.Entities
{
    public interface IBusinessRuleBase
    {
        string Message { get; }
    }

    public interface IBusinessRule : IBusinessRuleBase
    {
        bool IsBroken();
    }

    public interface IAsyncBusinessRule : IBusinessRuleBase
    {
        Task<bool> IsBrokenAsync();
    }
}
