using Moj.CMS.Domain.Shared.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices.Party
{
    [TransientService]
    public interface IGetCasePartiesNumbers
    {
        Task<Dictionary<string, PartyRoleEnum>> GetAsync(string _caseNumber);
    }
}
