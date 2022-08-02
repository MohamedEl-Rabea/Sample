using MudBlazor;
using System;

namespace SSS.Components.RowActions
{
    public class RowAction<T>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public Color Color { get; set; } = Color.Default;
        public Func<T, bool> Visible { get; set; } = row => true;
        public Action<T> Action { get; set; }
    }
}
