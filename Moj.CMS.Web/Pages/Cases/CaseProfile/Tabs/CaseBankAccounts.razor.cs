using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseBankAccount;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CaseBankAccounts
    {
        [Parameter] public int CaseId { get; set; }
        private bool _Loading = true;
        public IEnumerable<CaseBankAccountDto> caseBankAccountDtos;
        private Dictionary<string, MudTable<CaseBankAccountDto>> mytables = new Dictionary<string, MudTable<CaseBankAccountDto>>();

        private MudTable<CaseBankAccountDto> table = new MudTable<CaseBankAccountDto>();
        public IEnumerable<IGrouping<string, CaseBankAccountDto>> BankAccountsGrouping = new List<IGrouping<string, CaseBankAccountDto>>();

        protected override async Task OnInitializedAsync()
        {
            await GetCaseBankAccounts();
            await base.OnInitializedAsync();
        }

        private async Task GetCaseBankAccounts()
        {
            _Loading = true;
            var result = await CmsModule.ExecuteQueryAsync(new GetCaseBankAccountQuery { CaseId = CaseId });
            if (result.Succeeded)
            {
                caseBankAccountDtos = result.Data ?? new List<CaseBankAccountDto>();
                BankAccountsGrouping = caseBankAccountDtos.GroupBy(b => b.BankName);
                _Loading = false;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }
    }
}