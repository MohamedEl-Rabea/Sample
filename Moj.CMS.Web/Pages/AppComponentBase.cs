using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Moj.CMS.Application.Interfaces;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Requests;
using Moj.CMS.UserAccess.Application.Interfaces;
using Moj.CMS.Web.Shared.Services;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Web.Pages
{
    public class AppComponentBase : ComponentBase
    {
        [Inject]
        public IJSRuntime _jsRuntime { get; set; }

        [Inject]
        public ICmsModule CmsModule { get; set; }

        [Inject]
        public IUserAccessModule UserAccessModule { get; set; }

        [Inject]
        protected IStringLocalizer<CMSLocalizer> localizer { get; set; }

        [Inject]
        protected ICourtQueries _courtQueries { get; set;}

        [Inject]
        protected ISharedState SharedState { get; set; }

        protected void SetRequestDetails<T>(PagedRequest<T> pagedRequest, TableState state, dynamic filter)
        {
            pagedRequest.PageSize = state.PageSize;
            pagedRequest.PageNumber = state.Page;
            pagedRequest.Filter = filter;
            pagedRequest.Sort = state.SortDirection == MudBlazor.SortDirection.None
                 ? Enumerable.Empty<Sort>()
                 : new List<Sort>
                 {
                    new Sort
                    {
                        Field = state.SortLabel,
                        Direction= state.SortDirection == MudBlazor.SortDirection.Ascending
                        ? CMS.Shared.Requests.SortDirection.asc
                        : CMS.Shared.Requests.SortDirection.desc,
                    }
                 };
        }

        protected void AddBreadItem(string title, string href = "", bool disabled = false, string Icon = null)
        {
            SharedState.BreadcrumbItems.Add(new BreadcrumbItem(title, href, disabled, Icon));
        }

        protected void InitializeBreadcrumps(IEnumerable<BreadcrumbItem> newItems)
        {
            SharedState.BreadcrumbItems.Clear();
            foreach (var item in newItems)
            {
                AddBreadItem(item.Text, item.Href, item.Disabled, item.Icon);
            }
        }

        public async void Print()
        {
            await _jsRuntime.InvokeVoidAsync("Print");
        }
    }
}
