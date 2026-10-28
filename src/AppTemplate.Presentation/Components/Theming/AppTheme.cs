using MudBlazor;

namespace AppTemplate.Presentation.Components.Theming;

internal static class AppTheme
{
    public static MudTheme Create()
    {
        const string surface = "#FFFFFF";
        const string ink = "#243247";

        return new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = "#345BD6",
                Success = "#28745B",
                Error = "#B43F4B",
                Background = "#F5F7FA",
                Surface = surface,
                AppbarBackground = surface,
                AppbarText = ink,
                TextPrimary = ink,
                TextSecondary = "#637187",
                Divider = "#E5EAF1"
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily = ["Avenir Next", "Avenir", "Segoe UI", "sans-serif"]
                }
            },
            LayoutProperties = new LayoutProperties { DefaultBorderRadius = "10px" }
        };
    }
}
