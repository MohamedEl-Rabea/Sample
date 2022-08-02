using MudBlazor;

namespace SSS.Components.RowActions
{
    public class IconStyle
    {
        public Variant Variant { get; set; } = Variant.Text;
        public Size Size { get; set; } = Size.Small;
        public string Class { get; set; } = "mr-1 ml-1";
        public MenuButtonIcon MenuButtonIcon { get; set; } = new MenuButtonIcon();
    }
}