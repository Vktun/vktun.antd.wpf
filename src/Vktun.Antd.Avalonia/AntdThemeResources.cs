using Avalonia;
using Avalonia.Controls;

namespace Vktun.Antd.Avalonia;

/// <summary>
/// A mergeable resource dictionary that exposes Vktun.Antd tokens as Avalonia resources.
/// </summary>
public sealed class AntdThemeResources : ResourceDictionary
{
    private AntdThemeMode _theme = AntdThemeMode.Light;
    private AntdSeedToken _seed = AntdSeedToken.Default;

    /// <summary>
    /// Initializes a new instance of the <see cref="AntdThemeResources"/> class.
    /// </summary>
    public AntdThemeResources()
    {
        ReloadTokens();
    }

    /// <summary>
    /// Gets or sets the active theme mode.
    /// </summary>
    public AntdThemeMode Theme
    {
        get => _theme;
        set
        {
            if (_theme == value)
            {
                return;
            }

            _theme = value;
            ReloadTokens();
        }
    }

    /// <summary>
    /// Gets or sets the seed token used to generate runtime resources.
    /// </summary>
    public AntdSeedToken Seed
    {
        get => _seed;
        set
        {
            _seed = value ?? AntdSeedToken.Default;
            ReloadTokens();
        }
    }

    private void ReloadTokens()
    {
        Clear();
        var tokens = AntdTokenFactory.Create(_theme, _seed);

        AddColor(AntdResourceKeys.ColorPrimary, tokens.ColorPrimary);
        AddColor(AntdResourceKeys.ColorSuccess, tokens.ColorSuccess);
        AddColor(AntdResourceKeys.ColorWarning, tokens.ColorWarning);
        AddColor(AntdResourceKeys.ColorError, tokens.ColorError);

        AddBrush(AntdResourceKeys.BrushPrimary, tokens.ColorPrimary);
        AddBrush(AntdResourceKeys.BrushPrimaryHover, tokens.ColorPrimaryHover);
        AddBrush(AntdResourceKeys.BrushPrimaryActive, tokens.ColorPrimaryActive);
        AddBrush(AntdResourceKeys.BrushSuccess, tokens.ColorSuccess);
        AddBrush(AntdResourceKeys.BrushWarning, tokens.ColorWarning);
        AddBrush(AntdResourceKeys.BrushError, tokens.ColorError);
        AddBrush(AntdResourceKeys.BrushText, tokens.ColorText);
        AddBrush(AntdResourceKeys.BrushTextSecondary, tokens.ColorTextSecondary);
        AddBrush(AntdResourceKeys.BrushTextTertiary, tokens.ColorTextTertiary);
        AddBrush(AntdResourceKeys.BrushLink, tokens.ColorLink);
        AddBrush(AntdResourceKeys.BrushBgBase, tokens.ColorBgBase);
        AddBrush(AntdResourceKeys.BrushBgLayout, tokens.ColorBgLayout);
        AddBrush(AntdResourceKeys.BrushBgContainer, tokens.ColorBgContainer);
        AddBrush(AntdResourceKeys.BrushBgElevated, tokens.ColorBgElevated);
        AddBrush(AntdResourceKeys.BrushBgSpotlight, tokens.ColorBgSpotlight);
        AddBrush(AntdResourceKeys.BrushFillSecondary, tokens.ColorFillSecondary);
        AddBrush(AntdResourceKeys.BrushFillTertiary, tokens.ColorFillTertiary);
        AddBrush(AntdResourceKeys.BrushFillQuaternary, tokens.ColorFillQuaternary);
        AddBrush(AntdResourceKeys.BrushBorder, tokens.ColorBorder);
        AddBrush(AntdResourceKeys.BrushBorderSecondary, tokens.ColorBorderSecondary);
        AddBrush(AntdResourceKeys.BrushFocusOutline, tokens.ColorFocusOutline);
        AddBrush(AntdResourceKeys.BrushMask, tokens.ColorMask);
        AddBrush(AntdResourceKeys.BrushTagDefaultBackground, tokens.ColorTagDefaultBackground);
        AddBrush(AntdResourceKeys.BrushTagDefaultForeground, tokens.ColorTagDefaultForeground);
        AddBrush(AntdResourceKeys.BrushBadgeBackground, tokens.ColorBadgeBackground);
        AddBrush(AntdResourceKeys.BrushWhite, tokens.ColorWhite);
        AddBrush(AntdResourceKeys.BrushTransparent, tokens.ColorTransparent);

        this[AntdResourceKeys.FontSizeBase] = tokens.FontSizeBase;
        this[AntdResourceKeys.FontSizeSmall] = tokens.FontSizeSmall;
        this[AntdResourceKeys.FontSizeLarge] = tokens.FontSizeLarge;
        this[AntdResourceKeys.ControlHeightSmall] = tokens.ControlHeightSmall;
        this[AntdResourceKeys.ControlHeightMiddle] = tokens.ControlHeightMiddle;
        this[AntdResourceKeys.ControlHeightLarge] = tokens.ControlHeightLarge;
        this[AntdResourceKeys.BorderRadiusSmall] = new CornerRadius(tokens.BorderRadiusSmall);
        this[AntdResourceKeys.BorderRadiusMiddle] = new CornerRadius(tokens.BorderRadiusMiddle);
        this[AntdResourceKeys.BorderRadiusLarge] = new CornerRadius(tokens.BorderRadiusLarge);
        this[AntdResourceKeys.PaddingXs] = tokens.PaddingXs;
        this[AntdResourceKeys.PaddingSm] = tokens.PaddingSm;
        this[AntdResourceKeys.PaddingMd] = tokens.PaddingMd;
        this[AntdResourceKeys.PaddingLg] = tokens.PaddingLg;
    }

    private void AddColor(string key, AntdColor color)
    {
        this[key] = AntdColorHelper.ToAvaloniaColor(color);
    }

    private void AddBrush(string key, AntdColor color)
    {
        this[key] = AntdColorHelper.Brush(color);
    }
}
