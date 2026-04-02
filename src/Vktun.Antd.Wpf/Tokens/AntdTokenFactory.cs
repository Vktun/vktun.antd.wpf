using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Vktun.Antd.Wpf;

internal static class AntdTokenFactory
{
    public static ResourceDictionary Create(AntdThemeMode mode, AntdSeedToken seed)
    {
        var alias = CreateAlias(mode, seed);
        var component = CreateComponent(seed, mode);
        var dictionary = new ResourceDictionary();

        AddColor(dictionary, AntdResourceKeys.ColorPrimary, seed.PrimaryColor);
        AddColor(dictionary, AntdResourceKeys.ColorSuccess, seed.SuccessColor);
        AddColor(dictionary, AntdResourceKeys.ColorWarning, seed.WarningColor);
        AddColor(dictionary, AntdResourceKeys.ColorError, seed.ErrorColor);

        AddBrush(dictionary, AntdResourceKeys.BrushPrimary, seed.PrimaryColor);
        AddBrush(dictionary, AntdResourceKeys.BrushPrimaryHover, AntdColorHelper.Blend(seed.PrimaryColor, Colors.White, mode == AntdThemeMode.Dark ? 0.12d : 0.2d));
        AddBrush(dictionary, AntdResourceKeys.BrushPrimaryActive, AntdColorHelper.Blend(seed.PrimaryColor, Colors.Black, mode == AntdThemeMode.Dark ? 0.18d : 0.12d));
        AddBrush(dictionary, AntdResourceKeys.BrushSuccess, seed.SuccessColor);
        AddBrush(dictionary, AntdResourceKeys.BrushWarning, seed.WarningColor);
        AddBrush(dictionary, AntdResourceKeys.BrushError, seed.ErrorColor);
        AddBrush(dictionary, AntdResourceKeys.BrushText, alias.ColorText);
        AddBrush(dictionary, AntdResourceKeys.BrushTextSecondary, alias.ColorTextSecondary);
        AddBrush(dictionary, AntdResourceKeys.BrushTextTertiary, alias.ColorTextTertiary);
        AddBrush(dictionary, AntdResourceKeys.BrushLink, seed.PrimaryColor);
        AddBrush(dictionary, AntdResourceKeys.BrushBgBase, alias.ColorBgBase);
        AddBrush(dictionary, AntdResourceKeys.BrushBgLayout, alias.ColorBgLayout);
        AddBrush(dictionary, AntdResourceKeys.BrushBgContainer, alias.ColorBgContainer);
        AddBrush(dictionary, AntdResourceKeys.BrushBgElevated, alias.ColorBgElevated);
        AddBrush(dictionary, AntdResourceKeys.BrushBgSpotlight, alias.ColorBgSpotlight);
        AddBrush(dictionary, AntdResourceKeys.BrushFillSecondary, alias.ColorFillSecondary);
        AddBrush(dictionary, AntdResourceKeys.BrushFillTertiary, alias.ColorFillTertiary);
        AddBrush(dictionary, AntdResourceKeys.BrushFillQuaternary, alias.ColorFillQuaternary);
        AddBrush(dictionary, AntdResourceKeys.BrushBorder, alias.ColorBorder);
        AddBrush(dictionary, AntdResourceKeys.BrushBorderSecondary, alias.ColorBorderSecondary);
        AddBrush(dictionary, AntdResourceKeys.BrushFocusOutline, alias.ColorFocusOutline);
        AddBrush(dictionary, AntdResourceKeys.BrushMask, alias.ColorMask);
        AddBrush(dictionary, AntdResourceKeys.BrushTagDefaultBackground, mode == AntdThemeMode.Dark ? AntdColorHelper.Blend(alias.ColorBgContainer, Colors.White, 0.08d) : AntdColorHelper.Blend(seed.PrimaryColor, Colors.White, 0.92d));
        AddBrush(dictionary, AntdResourceKeys.BrushTagDefaultForeground, mode == AntdThemeMode.Dark ? alias.ColorText : seed.PrimaryColor);
        AddBrush(dictionary, AntdResourceKeys.BrushBadgeBackground, seed.ErrorColor);
        AddBrush(dictionary, AntdResourceKeys.BrushWhite, Colors.White);
        AddBrush(dictionary, AntdResourceKeys.BrushTransparent, Colors.Transparent);

        dictionary[AntdResourceKeys.FontSizeBase] = seed.FontSizeBase;
        dictionary[AntdResourceKeys.FontSizeSmall] = component.FontSizeSmall;
        dictionary[AntdResourceKeys.FontSizeLarge] = component.FontSizeLarge;
        dictionary[AntdResourceKeys.ControlHeightSmall] = mode == AntdThemeMode.Compact ? seed.ControlHeightSmall - 4d : seed.ControlHeightSmall;
        dictionary[AntdResourceKeys.ControlHeightMiddle] = mode == AntdThemeMode.Compact ? seed.ControlHeightMiddle - 4d : seed.ControlHeightMiddle;
        dictionary[AntdResourceKeys.ControlHeightLarge] = mode == AntdThemeMode.Compact ? seed.ControlHeightLarge - 4d : seed.ControlHeightLarge;
        dictionary[AntdResourceKeys.BorderRadiusSmall] = new CornerRadius(component.BorderRadiusSmall);
        dictionary[AntdResourceKeys.BorderRadiusMiddle] = new CornerRadius(component.BorderRadiusMiddle);
        dictionary[AntdResourceKeys.BorderRadiusLarge] = new CornerRadius(component.BorderRadiusLarge);
        dictionary[AntdResourceKeys.PaddingXs] = component.PaddingXs;
        dictionary[AntdResourceKeys.PaddingSm] = component.PaddingSm;
        dictionary[AntdResourceKeys.PaddingMd] = component.PaddingMd;
        dictionary[AntdResourceKeys.PaddingLg] = component.PaddingLg;
        dictionary[AntdResourceKeys.ShadowCard] = CreateShadow(mode, 10d, 2d, 0.16d);
        dictionary[AntdResourceKeys.ShadowPopup] = CreateShadow(mode, 16d, 4d, 0.22d);
        dictionary[AntdResourceKeys.ShadowModal] = CreateShadow(mode, 26d, 8d, 0.28d);

        return dictionary;
    }

    private static AntdAliasToken CreateAlias(AntdThemeMode mode, AntdSeedToken seed)
    {
        return mode switch
        {
            AntdThemeMode.Dark => new AntdAliasToken(
                ColorText: (Color)ColorConverter.ConvertFromString("#F3F5F7"),
                ColorTextSecondary: (Color)ColorConverter.ConvertFromString("#C5CDD5"),
                ColorTextTertiary: (Color)ColorConverter.ConvertFromString("#95A0AD"),
                ColorBgBase: (Color)ColorConverter.ConvertFromString("#111A22"),
                ColorBgLayout: (Color)ColorConverter.ConvertFromString("#0B1118"),
                ColorBgContainer: (Color)ColorConverter.ConvertFromString("#16202B"),
                ColorBgElevated: (Color)ColorConverter.ConvertFromString("#1C2936"),
                ColorBgSpotlight: AntdColorHelper.Blend(seed.PrimaryColor, Colors.Black, 0.45d),
                ColorFillSecondary: (Color)ColorConverter.ConvertFromString("#243241"),
                ColorFillTertiary: (Color)ColorConverter.ConvertFromString("#21303E"),
                ColorFillQuaternary: (Color)ColorConverter.ConvertFromString("#1B2733"),
                ColorBorder: (Color)ColorConverter.ConvertFromString("#304050"),
                ColorBorderSecondary: (Color)ColorConverter.ConvertFromString("#22303D"),
                ColorMask: AntdColorHelper.WithOpacity(Colors.Black, 0.55d),
                ColorFocusOutline: AntdColorHelper.Blend(seed.PrimaryColor, Colors.White, 0.18d)),
            _ => new AntdAliasToken(
                ColorText: (Color)ColorConverter.ConvertFromString("#1F2329"),
                ColorTextSecondary: (Color)ColorConverter.ConvertFromString("#5B6470"),
                ColorTextTertiary: (Color)ColorConverter.ConvertFromString("#88919D"),
                ColorBgBase: Colors.White,
                ColorBgLayout: (Color)ColorConverter.ConvertFromString("#F5F7FA"),
                ColorBgContainer: Colors.White,
                ColorBgElevated: Colors.White,
                ColorBgSpotlight: AntdColorHelper.Blend(seed.PrimaryColor, Colors.White, 0.9d),
                ColorFillSecondary: (Color)ColorConverter.ConvertFromString("#F0F4F8"),
                ColorFillTertiary: (Color)ColorConverter.ConvertFromString("#E8EDF3"),
                ColorFillQuaternary: (Color)ColorConverter.ConvertFromString("#DEE5EE"),
                ColorBorder: (Color)ColorConverter.ConvertFromString("#D0D7E2"),
                ColorBorderSecondary: (Color)ColorConverter.ConvertFromString("#E2E8F0"),
                ColorMask: AntdColorHelper.WithOpacity(Colors.Black, 0.35d),
                ColorFocusOutline: AntdColorHelper.Blend(seed.PrimaryColor, Colors.White, 0.4d)),
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

    private static void AddColor(ResourceDictionary dictionary, string key, Color color)
    {
        dictionary[key] = color;
    }

    private static void AddBrush(ResourceDictionary dictionary, string key, Color color)
    {
        dictionary[key] = AntdColorHelper.Brush(color);
    }

    private static DropShadowEffect CreateShadow(AntdThemeMode mode, double blurRadius, double depth, double opacity)
    {
        var effect = new DropShadowEffect
        {
            BlurRadius = blurRadius,
            Direction = 270d,
            ShadowDepth = depth,
            Opacity = mode == AntdThemeMode.Dark ? opacity * 0.8d : opacity,
            Color = Colors.Black,
        };

        effect.Freeze();
        return effect;
    }
}

