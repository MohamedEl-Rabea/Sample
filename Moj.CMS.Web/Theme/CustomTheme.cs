using MudBlazor;

namespace Moj.CMS.Web.Theme
{
    public class GreenTheme : MudTheme
    {
        public GreenTheme()
        {
            Palette = new Palette
            {
                Info = "#157C6A",
                DrawerIcon = "#c1a73c",
                Primary = "#f1c250",
                Tertiary = "#8ebead",
                Secondary = "#479cc8",
                Background = Colors.Grey.Lighten5,
                AppbarBackground = Colors.Blue.Darken1,
                DrawerBackground = "#FFF",
                DrawerText = "rgba(0,0,0, 0.7)",
                Success = "#06d79c"
            };

            LayoutProperties = new LayoutProperties();
            Typography = new Typography();
            Shadows = new Shadow();
            ZIndex = new ZIndex();
        }

    }
}