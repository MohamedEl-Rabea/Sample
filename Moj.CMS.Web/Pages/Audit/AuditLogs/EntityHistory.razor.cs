using Microsoft.AspNetCore.Components;
using Moj.CMS.Shared.Filter;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Queries;
using Moj.CMS.Shared.Requests;
using MudBlazor;
using SSS.Components.RowActions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Audit.AuditLogs
{
    public partial class EntityHistory
    {
        [Inject]
        protected ILogsQueries LogsQueries { get; set; }

        private List<EntityHistoryDto> _pagedData;
        private EntityHistoryFilter _entityFilter = new();
        private MudTable<EntityHistoryDto> _table;
        private int _totalItems;
        private bool _loading = true;
        private PagedRequest<EntityHistoryDto> _request = new();
        public List<RowAction<EntityHistoryDto>> Actions { get; set; } = new List<RowAction<EntityHistoryDto>>();

        public IconStyle IconStyle = new IconStyle
        {
            Size = Size.Small,
            Variant = Variant.Text
        };

        protected override async Task OnInitializedAsync()
        {
            SetGridActions();
            await base.OnInitializedAsync();
        }

        private void SetGridActions()
        {
            Actions.Add(new RowAction<EntityHistoryDto>
            {
                Name = Globallocalizer["View"],
                Icon = Icons.Filled.TrackChanges,
                Color = Color.Info,
                Action = async history => await InvokeModal(history)
            });
        }

        private async Task<TableData<EntityHistoryDto>> ServerReload(TableState state)
        {
            await LoadData(state);
            return new TableData<EntityHistoryDto> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(TableState state)
        {
            _loading = true;
            SetRequestDetails(_request, state, _entityFilter);
            if (_request.StateHasChanged())
            {
                var response = await LogsQueries.GetEntitiesHistoryAsync(_request);
                if (response.Succeeded)
                {
                    _totalItems = response.TotalCount;
                    _pagedData = response.Data;
                    _loading = false;
                }
                else
                {
                    foreach (var error in response.Errors)
                    {
                        _snackBar.Add(localizer[error], Severity.Error);
                    }
                }
            }
        }

        private void OnFilter()
        {
            _table.ReloadServerData();
        }

        private void Clear()
        {
            _entityFilter.Clear();
            _table.ReloadServerData();
        }

        private async Task InvokeModal(EntityHistoryDto entity)
        {
            if (entity == null)
                return;

            var parameters = new DialogParameters();
            parameters.Add("EntityHistory", entity);

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                DisableBackdropClick = true,
                Position = DialogPosition.Center
            };

            await _dialogService.Show<EntityDetailsModal>("Modal", parameters, options).Result;
        }
    }
}
