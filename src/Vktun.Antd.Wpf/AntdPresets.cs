using System.Windows.Media;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides predefined theme presets and colors for Ant Design WPF.
/// </summary>
public static class AntdPresets
{
    /// <summary>
    /// Provides predefined color presets.
    /// </summary>
    public static class Colors
    {
        /// <summary>
        /// Ant Design default blue (#1677FF).
        /// </summary>
        public static Color AntdBlue => ParseColor("#1677FF");

        /// <summary>
        /// Aliyun orange (#FF6A00).
        /// </summary>
        public static Color AliyunOrange => ParseColor("#FF6A00");

        /// <summary>
        /// WeChat green (#07C160).
        /// </summary>
        public static Color WeChatGreen => ParseColor("#07C160");

        /// <summary>
        /// Geek blue (#2F54EB).
        /// </summary>
        public static Color GeekBlue => ParseColor("#2F54EB");

        /// <summary>
        /// Golden (#D48806).
        /// </summary>
        public static Color Golden => ParseColor("#D48806");

        /// <summary>
        /// Magenta (#EB2F96).
        /// </summary>
        public static Color Magenta => ParseColor("#EB2F96");

        /// <summary>
        /// Purple (#722ED1).
        /// </summary>
        public static Color Purple => ParseColor("#722ED1");

        /// <summary>
        /// Volcano (#FA541C).
        /// </summary>
        public static Color Volcano => ParseColor("#FA541C");

        /// <summary>
        /// Cyan (#13C2C2).
        /// </summary>
        public static Color Cyan => ParseColor("#13C2C2");

        /// <summary>
        /// Lime (#A0D911).
        /// </summary>
        public static Color Lime => ParseColor("#A0D911");

        /// <summary>
        /// Green success color (#52C41A).
        /// </summary>
        public static Color Success => ParseColor("#52C41A");

        /// <summary>
        /// Yellow warning color (#FAAD14).
        /// </summary>
        public static Color Warning => ParseColor("#FAAD14");

        /// <summary>
        /// Red error color (#FF4D4F).
        /// </summary>
        public static Color Error => ParseColor("#FF4D4F");
    }

    /// <summary>
    /// Gets the default seed token preset.
    /// </summary>
    public static AntdSeedToken Default { get; } = AntdSeedToken.Default;

    /// <summary>
    /// Gets a compact seed token preset with smaller control heights.
    /// </summary>
    public static AntdSeedToken Compact { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.AntdBlue,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 24d,
        ControlHeightMiddle = 32d,
        ControlHeightLarge = 40d,
        BorderRadius = 6d
    };

    /// <summary>
    /// Gets a round seed token preset with larger border radius.
    /// </summary>
    public static AntdSeedToken Round { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.AntdBlue,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 32d,
        ControlHeightMiddle = 40d,
        ControlHeightLarge = 48d,
        BorderRadius = 12d
    };

    /// <summary>
    /// Gets a sharp seed token preset with minimal border radius.
    /// </summary>
    public static AntdSeedToken Sharp { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.AntdBlue,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 32d,
        ControlHeightMiddle = 40d,
        ControlHeightLarge = 48d,
        BorderRadius = 2d
    };

    /// <summary>
    /// Gets Aliyun theme preset with orange primary color.
    /// </summary>
    public static AntdSeedToken Aliyun { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.AliyunOrange,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 32d,
        ControlHeightMiddle = 40d,
        ControlHeightLarge = 48d,
        BorderRadius = 8d
    };

    /// <summary>
    /// Gets WeChat theme preset with green primary color.
    /// </summary>
    public static AntdSeedToken WeChat { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.WeChatGreen,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 32d,
        ControlHeightMiddle = 40d,
        ControlHeightLarge = 48d,
        BorderRadius = 8d
    };

    /// <summary>
    /// Gets Geek theme preset with deep blue primary color.
    /// </summary>
    public static AntdSeedToken Geek { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.GeekBlue,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 32d,
        ControlHeightMiddle = 40d,
        ControlHeightLarge = 48d,
        BorderRadius = 8d
    };

    /// <summary>
    /// Gets Cyan theme preset with cyan primary color.
    /// </summary>
    public static AntdSeedToken Cyan { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.Cyan,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 32d,
        ControlHeightMiddle = 40d,
        ControlHeightLarge = 48d,
        BorderRadius = 8d
    };

    /// <summary>
    /// Gets Magenta theme preset with magenta primary color.
    /// </summary>
    public static AntdSeedToken Magenta { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.Magenta,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 32d,
        ControlHeightMiddle = 40d,
        ControlHeightLarge = 48d,
        BorderRadius = 8d
    };

    /// <summary>
    /// Gets Purple theme preset with purple primary color.
    /// </summary>
    public static AntdSeedToken Purple { get; } = new AntdSeedToken
    {
        PrimaryColor = Colors.Purple,
        SuccessColor = Colors.Success,
        WarningColor = Colors.Warning,
        ErrorColor = Colors.Error,
        FontSizeBase = 14d,
        ControlHeightSmall = 32d,
        ControlHeightMiddle = 40d,
        ControlHeightLarge = 48d,
        BorderRadius = 8d
    };

    private static Color ParseColor(string hex)
    {
        return (Color)ColorConverter.ConvertFromString(hex);
    }
}

/// <summary>
/// Represents a complete theme configuration with mode and seed token.
/// </summary>
public record AntdThemeConfiguration(AntdThemeMode Mode, AntdSeedToken Seed);

/// <summary>
/// Provides predefined complete theme configurations.
/// </summary>
public static class AntdThemePresets
{
    /// <summary>
    /// Gets the default light theme configuration.
    /// </summary>
    public static AntdThemeConfiguration LightDefault { get; } = new(AntdThemeMode.Light, AntdPresets.Default);

    /// <summary>
    /// Gets the default dark theme configuration.
    /// </summary>
    public static AntdThemeConfiguration DarkDefault { get; } = new(AntdThemeMode.Dark, AntdPresets.Default);

    /// <summary>
    /// Gets the compact light theme configuration.
    /// </summary>
    public static AntdThemeConfiguration CompactLight { get; } = new(AntdThemeMode.Compact, AntdPresets.Compact);

    /// <summary>
    /// Gets the Aliyun light theme configuration.
    /// </summary>
    public static AntdThemeConfiguration AliyunLight { get; } = new(AntdThemeMode.Light, AntdPresets.Aliyun);

    /// <summary>
    /// Gets the WeChat light theme configuration.
    /// </summary>
    public static AntdThemeConfiguration WeChatLight { get; } = new(AntdThemeMode.Light, AntdPresets.WeChat);

    /// <summary>
    /// Gets the Geek light theme configuration.
    /// </summary>
    public static AntdThemeConfiguration GeekLight { get; } = new(AntdThemeMode.Light, AntdPresets.Geek);

    /// <summary>
    /// Gets the Cyan light theme configuration.
    /// </summary>
    public static AntdThemeConfiguration CyanLight { get; } = new(AntdThemeMode.Light, AntdPresets.Cyan);
}