using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SSS.Components.RowActions
{
    public partial class SSSRowActions<T>
    {
        public Direction Direction { get; set; }
        [Parameter] public T RowContext { get; set; }
        [Parameter] public IconStyle IconStyle { get; set; } = new IconStyle();
        [Parameter] public List<RowAction<T>> Actions { get; set; } = new List<RowAction<T>>();

        protected IEnumerable<RowAction<T>> FlatActions { get; set; }
        protected IEnumerable<RowAction<T>> ContextMenuActions { get; set; }

        protected override void OnInitialized()
        {
            Direction = Thread.CurrentThread.CurrentCulture.Name.ToLower().Contains("en")
                ? MudBlazor.Direction.Right
                : MudBlazor.Direction.Left;

            var visibleActions = Actions.Where(a => a.Visible(RowContext)).ToList();
            FlatActions = visibleActions.Count > 3 ? visibleActions.Take(2) : visibleActions;
            ContextMenuActions = visibleActions.Count > 3 ? visibleActions.Skip(2) : new List<RowAction<T>>();

            base.OnInitialized();
        }
    }
}
