using Moj.CMS.Domain.ParameterObjects.CaseHistory;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.AppServices.CaseHistory.Services
{
    [ScopedService]
    public interface ICaseHistoryService
    {
        Task AddCaseLogEntryAsync(CreateCaseHistoryParam caseHistoryParam);
    }
}
