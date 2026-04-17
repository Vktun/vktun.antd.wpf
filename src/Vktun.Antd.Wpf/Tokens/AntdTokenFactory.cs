using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using CoreTokenFactory = Vktun.Antd.AntdTokenFactory;

namespace Vktun.Antd.Wpf;

internal static class AntdTokenFactory
{
    public static ResourceDictionary Create(AntdThemeMode mode, AntdSeedToken seed)
    {
        var tokens = CoreTokenFactory.Create(mode, seed);
        var dictionary = new ResourceDictionary();

        AddColor(dictionary, AntdResourceKeys.ColorPrimary, tokens.ColorPrimary);
        AddColor(dictionary, AntdResourceKeys.ColorSuccess, tokens.ColorSuccess);
        AddColor(dictionary, AntdResourceKeys.ColorWarning, tokens.ColorWarning);
        AddColor(dictionary, AntdResourceKeys.ColorError, tokens.ColorError);

        AddBrush(dictionary, AntdResourceKeys.BrushPrimary, tokens.ColorPrimary);
        AddBrush(dictionary, AntdResourceKeys.BrushPrimaryHover, tokens.ColorPrimaryHover);
        AddBrush(dictionary, AntdResourceKeys.BrushPrimaryActive, tokens.ColorPrimaryActive);
        AddBrush(dictionary, AntdResourceKeys.BrushSuccess, tokens.ColorSuccess);
        AddBrush(dictionary, AntdResourceKeys.BrushWarning, tokens.ColorWarning);
        AddBrush(dictionary, AntdResourceKeys.BrushError, tokens.ColorError);
        AddBrush(dictionary, AntdResourceKeys.BrushText, tokens.ColorText);
        AddBrush(dictionary, AntdResourceKeys.BrushTextSecondary, tokens.ColorTextSecondary);
        AddBrush(dictionary, AntdResourceKeys.BrushTextTertiary, tokens.ColorTextTertiary);
        AddBrush(dictionary, AntdResourceKeys.BrushLink, tokens.ColorLink);
        AddBrush(dictionary, AntdResourceKeys.BrushBgBase, tokens.ColorBgBase);
        AddBrush(dictionary, AntdResourceKeys.BrushBgLayout, tokens.ColorBgLayout);
        AddBrush(dictionary, AntdResourceKeys.BrushBgContainer, tokens.ColorBgContainer);
        AddBrush(dictionary, AntdResourceKeys.BrushBgElevated, tokens.ColorBgElevated);
        AddBrush(dictionary, AntdResourceKeys.BrushBgSpotlight, tokens.ColorBgSpotlight);
        AddBrush(dictionary, AntdResourceKeys.BrushFillSecondary, tokens.ColorFillSecondary);
        AddBrush(dictionary, AntdResourceKeys.BrushFillTertiary, tokens.ColorFillTertiary);
        AddBrush(dictionary, AntdResourceKeys.BrushFillQuaternary, tokens.ColorFillQuaternary);
        AddBrush(dictionary, AntdResourceKeys.BrushBorder, tokens.ColorBorder);
        AddBrush(dictionary, AntdResourceKeys.BrushBorderSecondary, tokens.ColorBorderSecondary);
        AddBrush(dictionary, AntdResourceKeys.BrushFocusOutline, tokens.ColorFocusOutline);
        AddBrush(dictionary, AntdResourceKeys.BrushMask, tokens.ColorMask);
        AddBrush(dictionary, AntdResourceKeys.BrushTagDefaultBackground, tokens.ColorTagDefaultBackground);
        AddBrush(dictionary, AntdResourceKeys.BrushTagDefaultForeground, tokens.ColorTagDefaultForeground);
        AddBrush(dictionary, AntdResourceKeys.BrushBadgeBackground, tokens.ColorBadgeBackground);
        AddBrush(dictionary, AntdResourceKeys.BrushWhite, tokens.ColorWhite);
        AddBrush(dictionary, AntdResourceKeys.BrushTransparent, tokens.ColorTransparent);

        dictionary[AntdResourceKeys.FontSizeBase] = tokens.FontSizeBase;
        dictionary[AntdResourceKeys.FontSizeSmall] = tokens.FontSizeSmall;
        dictionary[AntdResourceKeys.FontSizeLarge] = tokens.FontSizeLarge;
        dictionary[AntdResourceKeys.ControlHeightSmall] = tokens.ControlHeightSmall;
        dictionary[AntdResourceKeys.ControlHeightMiddle] = tokens.ControlHeightMiddle;
        dictionary[AntdResourceKeys.ControlHeightLarge] = tokens.ControlHeightLarge;
        dictionary[AntdResourceKeys.BorderRadiusSmall] = new CornerRadius(tokens.BorderRadiusSmall);
        dictionary[AntdResourceKeys.BorderRadiusMiddle] = new CornerRadius(tokens.BorderRadiusMiddle);
        dictionary[AntdResourceKeys.BorderRadiusLarge] = new CornerRadius(tokens.BorderRadiusLarge);
        dictionary[AntdResourceKeys.PaddingXs] = tokens.PaddingXs;
        dictionary[AntdResourceKeys.PaddingSm] = tokens.PaddingSm;
        dictionary[AntdResourceKeys.PaddingMd] = tokens.PaddingMd;
        dictionary[AntdResourceKeys.PaddingLg] = tokens.PaddingLg;
        dictionary[AntdResourceKeys.ShadowCard] = CreateShadow(mode, 10d, 2d, 0.16d);
        dictionary[AntdResourceKeys.ShadowPopup] = CreateShadow(mode, 16d, 4d, 0.22d);
        dictionary[AntdResourceKeys.ShadowModal] = CreateShadow(mode, 26d, 8d, 0.28d);

        return dictionary;
    }

    private static void AddColor(ResourceDictionary dictionary, string key, AntdColor color)
    {
        dictionary[key] = AntdColorHelper.ToWpfColor(color);
    }

    private static void AddBrush(ResourceDictionary dictionary, string key, AntdColor color)
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
