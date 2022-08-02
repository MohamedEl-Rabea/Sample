using MudBlazor;

namespace SSS.Components.RowActions
{
    public class MenuButtonIcon
    {
        public string Icon { get; set; } = Icons.Material.Filled.MoreVert;
        public Color Color { get; set; } = Color.Default;
        public bool Dense { get; set; } = true;
        public string Name { get; set; }
    }
}