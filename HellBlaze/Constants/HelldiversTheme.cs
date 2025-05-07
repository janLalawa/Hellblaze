using MudBlazor;

namespace HellBlaze.Constants;

public static class HelldiversTheme
{
    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = Colors.PrimaryYellow,
            PrimaryDarken = Colors.SecondaryYellow,
            Secondary = Colors.Danger,
            SecondaryDarken = Colors.DangerDark,
            Tertiary = Colors.PrimaryBlue,

            AppbarBackground = Colors.DarkBlue,
            Background = Colors.Dark,
            Surface = Colors.DarkBlue,
            DrawerBackground = Colors.DarkBlue,
            DrawerText = Colors.White,

            Success = Colors.Success,
            Error = Colors.Danger,

            Black = Colors.Black,
            White = Colors.White,
            TextPrimary = Colors.White,
            TextSecondary = Colors.Gray,
            Dark = Colors.DarkBlue
        }
    };

    // Core Helldivers 2 colors
    public static class Colors
    {
        // Base colors
        public const string Dark = "#0a0e12";
        public const string Black = "#222323";
        public const string White = "#ffffff";
        public const string Gray = "#adb0bb";
        public const string DarkBlue = "#0a1219";
        public const string MediumGray = "#686868";

        // Brand colors
        public const string PrimaryYellow = "#E6D75E";
        public const string PrimaryBlue = "#0092a6";
        public const string PrimaryBlueBright = "#30b8d3";
        public const string SecondaryYellow = "#ffb400";
        public const string SecondaryOrange = "#ba6d00";

        // Functional colors
        public const string Success = "#8FF799";
        public const string Danger = "#E15F58";
        public const string DangerDark = "#450F13";

        // Player colors
        public const string Player1 = "#ECA256";
        public const string Player2 = "#8EABF9";
        public const string Player3 = "#E593F9";
        public const string Player4 = "#8DD466";
    }
}