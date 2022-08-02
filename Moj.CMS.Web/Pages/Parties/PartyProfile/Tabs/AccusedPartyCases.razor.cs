using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Party.Queries;
using MudBlazor;
using System.Collections.Generic;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class AccusedPartyCases
    {
        [Parameter] public IEnumerable<PartyCaseListItemDto> AccusedPartyCasesList { get; set; } = new List<PartyCaseListItemDto>();
        private MudTable<PartyCaseListItemDto> table;
    }
}
