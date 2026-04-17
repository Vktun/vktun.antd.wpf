using System;

namespace Vktun.Antd;

/// <summary>
/// Creates resolved theme tokens from a seed token and theme mode.
/// </summary>
public static class AntdTokenFactory
{
    private static readonly AntdColor Black = AntdColor.FromRgb(0, 0, 0);
    private static readonly AntdColor White = AntdColor.FromRgb(255, 255, 255);
    private static readonly AntdColor Transparent = AntdColor.FromArgb(0, 255, 255, 255);

    /// <summary>
    /// Creates a resolved token set.
    /// </summary>
    /// <param name="mode">The theme mode.</param>
    /// <param name="seed">The seed token.</param>
    /// <returns>The resolved token set.</returns>
    public static AntdTokenSet Create(AntdThemeMode mode, AntdSeedToken seed)
    {
        ArgumentNullException.ThrowIfNull(seed);

        var alias = CreateAlias(mode, seed);
        var component = CreateComponent(seed, mode);

        return new AntdTokenSet
        {
            ColorPrimary = seed.PrimaryColor,
            ColorPrimaryHover = AntdColorMath.Blend(seed.PrimaryColor, White, mode == AntdThemeMode.Dark ? 0.12d : 0.2d),
            ColorPrimaryActive = AntdColorMath.Blend(seed.PrimaryColor, Black, mode == AntdThemeMode.Dark ? 0.18d : 0.12d),
            ColorSuccess = seed.SuccessColor,
            ColorWarning = seed.WarningColor,
            ColorError = seed.ErrorColor,
            ColorText = alias.ColorText,
            ColorTextSecondary = alias.ColorTextSecondary,
            ColorTextTertiary = alias.ColorTextTertiary,
            ColorLink = seed.PrimaryColor,
            ColorBgBase = alias.ColorBgBase,
            ColorBgLayout = alias.ColorBgLayout,
            ColorBgContainer = alias.ColorBgContainer,
            ColorBgElevated = alias.ColorBgElevated,
            ColorBgSpotlight = alias.ColorBgSpotlight,
            ColorFillSecondary = alias.ColorFillSecondary,
            ColorFillTertiary = alias.ColorFillTertiary,
            ColorFillQuaternary = alias.ColorFillQuaternary,
            ColorBorder = alias.ColorBorder,
            ColorBorderSecondary = alias.ColorBorderSecondary,
            ColorFocusOutline = alias.ColorFocusOutline,
            ColorMask = alias.ColorMask,
            ColorTagDefaultBackground = mode == AntdThemeMode.Dark
                ? AntdColorMath.Blend(alias.ColorBgContainer, White, 0.08d)
                : AntdColorMath.Blend(seed.PrimaryColor, White, 0.92d),
            ColorTagDefaultForeground = mode == AntdThemeMode.Dark ? alias.ColorText : seed.PrimaryColor,
            ColorBadgeBackground = seed.ErrorColor,
            ColorWhite = White,
            ColorTransparent = Transparent,
            FontSizeBase = seed.FontSizeBase,
            FontSizeSmall = component.FontSizeSmall,
            FontSizeLarge = component.FontSizeLarge,
            ControlHeightSmall = mode == AntdThemeMode.Compact ? seed.ControlHeightSmall - 4d : seed.ControlHeightSmall,
            ControlHeightMiddle = mode == AntdThemeMode.Compact ? seed.ControlHeightMiddle - 4d : seed.ControlHeightMiddle,
            ControlHeightLarge = mode == AntdThemeMode.Compact ? seed.ControlHeightLarge - 4d : seed.ControlHeightLarge,
            BorderRadiusSmall = component.BorderRadiusSmall,
            BorderRadiusMiddle = component.BorderRadiusMiddle,
            BorderRadiusLarge = component.BorderRadiusLarge,
            PaddingXs = component.PaddingXs,
            PaddingSm = component.PaddingSm,
            PaddingMd = component.PaddingMd,
            PaddingLg = component.PaddingLg,
        };
    }

    private static AntdAliasToken CreateAlias(AntdThemeMode mode, AntdSeedToken seed)
    {
        return mode switch
        {
            AntdThemeMode.Dark => new AntdAliasToken(
                ColorText: AntdColor.Parse("#F3F5F7"),
                ColorTextSecondary: AntdColor.Parse("#C5CDD5"),
                ColorTextTertiary: AntdColor.Parse("#95A0AD"),
                ColorBgBase: AntdColor.Parse("#111A22"),
                ColorBgLayout: AntdColor.Parse("#0B1118"),
                ColorBgContainer: AntdColor.Parse("#16202B"),
                ColorBgElevated: AntdColor.Parse("#1C2936"),
                ColorBgSpotlight: AntdColorMath.Blend(seed.PrimaryColor, Black, 0.45d),
                ColorFillSecondary: AntdColor.Parse("#243241"),
                ColorFillTertiary: AntdColor.Parse("#21303E"),
                ColorFillQuaternary: AntdColor.Parse("#1B2733"),
                ColorBorder: AntdColor.Parse("#304050"),
                ColorBorderSecondary: AntdColor.Parse("#22303D"),
                ColorMask: AntdColorMath.WithOpacity(Black, 0.55d),
                ColorFocusOutline: AntdColorMath.Blend(seed.PrimaryColor, White, 0.18d)),
            _ => new AntdAliasToken(
                ColorText: AntdColor.Parse("#1F2329"),
                ColorTextSecondary: AntdColor.Parse("#5B6470"),
                ColorTextTertiary: AntdColor.Parse("#88919D"),
                ColorBgBase: White,
                ColorBgLayout: AntdColor.Parse("#F5F7FA"),
                ColorBgContainer: White,
                ColorBgElevated: White,
                ColorBgSpotlight: AntdColorMath.Blend(seed.PrimaryColor, White, 0.9d),
                ColorFillSecondary: AntdColor.Parse("#F0F4F8"),
                ColorFillTertiary: AntdColor.Parse("#E8EDF3"),
                ColorFillQuaternary: AntdColor.Parse("#DEE5EE"),
                ColorBorder: AntdColor.Parse("#D0D7E2"),
                ColorBorderSecondary: AntdColor.Parse("#E2E8F0"),
                ColorMask: AntdColorMath.WithOpacity(Black, 0.35d),
                ColorFocusOutline: AntdColorMath.Blend(seed.PrimaryColor, White, 0.4d)),
        };
    }

    private static AntdComponentToken CreateComponent(AntdSeedToken seed, AntdThemeMode mode)
    {
        var compactOffset = mode == AntdThemeMode.Compact ? 2d : 0d;
        return new AntdComponentToken(
            BorderRadiusSmall: seed.BorderRadius - 2d,
            BorderRadiusMiddle: seed.BorderRadius,
            BorderRadiusLarge: seed.BorderRadius + 4d,
            PaddingXs: 4d,
            PaddingSm: 8d - compactOffset,
            PaddingMd: 12d - compactOffset,
            PaddingLg: 16d - compactOffset,
            FontSizeSmall: seed.FontSizeBase - 1d,
            FontSizeLarge: seed.FontSizeBase + 2d);
    }
}
