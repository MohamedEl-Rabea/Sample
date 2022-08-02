using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices
{
    [TransientService]
    public interface IEnforceJudgeIsExists
    {
        Task<bool> IsExistAsync(string judgeCode);
    }
}
