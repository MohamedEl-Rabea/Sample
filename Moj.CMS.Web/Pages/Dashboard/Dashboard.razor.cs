using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Moj.CMS.Application.AppServices.Case.Queries.GetDashboardData;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Web.Constants;
using MudBlazor;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Dashboard
{
    public class DataItem
    {
        public string Resource { get; set; }
        public double Amount { get; set; }
    }

    public partial class Dashboard
    {
        [Inject] public IStringLocalizer<Dashboard> Localizer { get; set; }
        public DashboardDto Data { get; set; } = new DashboardDto();
        private bool _loading = true;
        private DataItem[] CashResources { get; set; }
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[ Urls.Administration.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.Administration.Dashboard.Title], href: Urls.Administration.Dashboard.Href, icon: Urls.Administration.Dashboard.Icon),
            });

            _loading = true;
            CashResources = new[]
            {
                new DataItem { Resource = Localizer["Central Bank"], Amount = 234000 },
                new DataItem { Resource = Localizer["Sama"], Amount = 284000 },
                new DataItem { Resource = Localizer["Sadad"], Amount = 274000 },
                new DataItem { Resource = Localizer["Mazad"], Amount = 294000 }
            };
            var response = await CmsModule.ExecuteQueryAsync(new GetDashboardDataQuery());
            if (response.Succeeded)
            {
                Data = response.Data;
                Data.Summary.Paid = new MoneyDto { Value = 1985135, CurrencyIso = "SAR" };
                Data.Summary.UnPaid = new MoneyDto { Value = 789531, CurrencyIso = "SAR" };
                Data.Summary.TechnicalProblems = 12;
                _loading = false;
            }
        }

        protected string FormatCurrencies(object value)
        {
            return $"{value}    ";
        }

    }
}