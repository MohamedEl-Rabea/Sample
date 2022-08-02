using Microsoft.AspNetCore.Components;
using Moj.CMS.Shared.Constants.Application;
using Moj.CMS.Shared.Enums;
using Moj.CMS.Shared.Filters;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Queries;
using Moj.CMS.Shared.Requests;
using MudBlazor;
using SSS.Components.RowActions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Audit.AuditLogs
{
    public partial class LogEvents
    {
        [Inject]
        protected ILogsQueries LogsQueries { get; set; }

        private List<Log> _pagedData;
        private LogFilter LogFilter = new();
        private MudTable<Log> _table;
        private int _totalItems;
        private bool _loading = true;
        private PagedRequest<Log> _request = new();
        public List<RowAction<Log>> Actions { get; set; } = new List<RowAction<Log>>();
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
            Actions.AddRange(new[]
            {
                new RowAction<Log>
                {
                    Name = Globallocalizer["View Details"],
                    Icon = Icons.Filled.Preview,
                    Color = Color.Info,
                    Action = async log => await InvokeDetailsModal(log)
                },
                new RowAction<Log>
                {
                    Name = Globallocalizer["View Changes"],
                    Icon = Icons.Filled.TrackChanges,
                    Color = Color.Info,
                    Action = async log => await InvokeChangesModal(log.RequestId),
                    Visible = log => log.Status == ErrorStatus.Success && log.RequestType== RequestType.Command
                }
            });
        }

        private async Task<TableData<Log>> ServerReload(TableState state)
        {
            await LoadData(state);
            return new TableData<Log> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(TableState state)
        {
            _loading = true;
            SetRequestDetails(_request, state, LogFilter);
            if (_request.StateHasChanged())
            {
                var response = await LogsQueries.GetAllAsync(_request);
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
            LogFilter.Clear();
            _table.ReloadServerData();
        }

        private async Task InvokeDetailsModal(Log log)
        {
            if (log == null)
                return;

            var parameters = new DialogParameters();
            parameters.Add("LogDetails", log);

            var options = new DialogOptions()
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                DisableBackdropClick = true,
                Position = DialogPosition.Center
            };

            await _dialogService.Show<LogDetailsModal>("Modal", parameters, options).Result;
        }

        private async Task InvokeChangesModal(string requestId)
        {
            var parameters = new DialogParameters();
            parameters.Add("RequestId", requestId);

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
