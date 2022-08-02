using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Shared.Components
{
    public partial class AdvancedSearch
    {
        [Parameter] public bool Open { get; set; }
        [Parameter] public EventCallback<bool> OpenChanged { get; set; }
        [Parameter] public EventCallback OnClearFilters { get; set; }
        [Parameter] public EventCallback OnApplyFilters { get; set; }
        [Parameter] public RenderFragment Content { get; set; }

        [Parameter] public Anchor Anchor { get; set; } = CultureInfo.CurrentCulture.Name.ToLower().Contains("ar") ? MudBlazor.Anchor.End : MudBlazor.Anchor.Start;

        private async Task UpdateOpenValue()
        {
            Open = false;
            await OpenChanged.InvokeAsync(false);
        }
    }
}
