using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace SSS.Components.RowActions
{
    public partial class SSSActionsTd<T>
    {
        [Parameter] public T RowContext { get; set; }
        [Parameter] public IconStyle IconStyle { get; set; } = new IconStyle();
        [Parameter] public List<RowAction<T>> Actions { get; set; } = new List<RowAction<T>>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
