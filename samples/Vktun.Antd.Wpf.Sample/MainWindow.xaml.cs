using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Vktun.Antd.Wpf;
using Vktun.Antd.Wpf.Sample.Pages;

namespace Vktun.Antd.Wpf.Sample;

public partial class MainWindow : Window
{
    private readonly ResourceDictionary _sceneOverrides = new();
    private readonly List<ThemePreset> _themePresets = CreateThemePresets();
    private readonly Dictionary<string, UserControl> _pageCache = new();
    private readonly List<CatalogNavigationItem> _navigationItems = CreateNavigationItems();

    public MainWindow()
    {
        InitializeComponent();

        if (Application.Current is not null &&
            !Application.Current.Resources.MergedDictionaries.Contains(_sceneOverrides))
        {
            Application.Current.Resources.MergedDictionaries.Add(_sceneOverrides);
        }

        InitializeCatalog();
    }

    private void InitializeCatalog()
    {
        ThemeCountTag.Content = $"{_themePresets.Count} 个预设";
        ThemePresetSelector.ItemsSource = _themePresets;
        NavigationListBox.ItemsSource = _navigationItems;

        var initialIndex = _themePresets.FindIndex(static preset => preset.Kind == ThemePresetKind.Illustration);
        ThemePresetSelector.SelectedIndex = initialIndex >= 0 ? initialIndex : 0;
        NavigationListBox.SelectedIndex = 0;
    }

    private void ThemePresetSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemePresetSelector.SelectedItem is not ThemePreset preset)
        {
            return;
        }

        ApplyPreset(preset);
    }

    private void NavigationListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (NavigationListBox.SelectedItem is not CatalogNavigationItem item)
        {
            return;
        }

        NavigateTo(item.Key);
    }

    private void ApplyPreset(ThemePreset preset)
    {
        if (Application.Current is null)
        {
            return;
        }

        AntdThemeManager.Current.Apply(Application.Current, preset.Mode, preset.Seed);
        ApplySceneOverrides(preset);

        ThemeHeroTitleTextBlock.Text = preset.Name;
        ThemeHeroDescriptionTextBlock.Text = preset.Description;
        ThemeModeTag.Content = preset.ModeText;
        ThemePrimaryTag.Content = preset.Seed.PrimaryColor.ToHex();
        ThemeShapeTag.Content = $"{preset.Seed.BorderRadius:0}px 圆角";
        ThemeFooterTextBlock.Text = preset.FooterHint;

        WindowBackdrop.Background = CreateLinearBrush(preset.ShellStart, preset.ShellEnd);
        PatternOverlay.Fill = CreatePatternBrush(preset.PatternKind, preset.PatternInk, preset.PatternAccent);
        PatternOverlay.Opacity = preset.PatternOpacity;
        TopLeftGlow.Fill = CreateRadialBrush(preset.GlowPrimary);
        BottomRightGlow.Fill = CreateRadialBrush(preset.GlowSecondary);

        ThemeRail.Background = CreateSolidBrush(preset.SidebarBackground);
        ThemeRail.BorderBrush = CreateSolidBrush(preset.SidebarBorder);
        ThemeRail.BorderThickness = new Thickness(preset.HardOutline ? 3d : 2d);
        ThemeRail.Effect = CreateShadowEffect(preset.SidebarBorder, preset.Mode == AntdThemeMode.Dark ? 18d : 14d, 3d, preset.Mode == AntdThemeMode.Dark ? 0.24d : 0.12d);

        PreviewShell.Background = CreateLinearBrush(preset.PreviewStart, preset.PreviewEnd);
        PreviewShell.BorderBrush = CreateSolidBrush(preset.PreviewBorder);
        PreviewShell.BorderThickness = new Thickness(preset.HardOutline ? 4d : 3d);
        PreviewShell.Effect = CreateShadowEffect(preset.PreviewBorder, preset.Mode == AntdThemeMode.Dark ? 26d : 22d, 6d, preset.Mode == AntdThemeMode.Dark ? 0.28d : 0.16d);

    }

    private void NavigateTo(string key)
    {
        if (!_pageCache.TryGetValue(key, out var page))
        {
            page = CreatePage(key);
            _pageCache[key] = page;
        }

        PageHost.Content = page;
    }

    private static List<CatalogNavigationItem> CreateNavigationItems()
    {
        return
        [
            new("overview", "总览", "组件覆盖矩阵、映射规则和本轮新增状态。"),
            new("general", "通用", "Button、FloatButton、Typography。"),
            new("layout", "布局", "Divider、Grid、Space、Flex、Layout、Splitter。"),
            new("navigation", "导航", "Breadcrumb、Dropdown、Menu、Pagination、Steps、Tabs。"),
            new("data-entry", "数据录入", "Input、Form、DatePicker、InputNumber、Slider 等。"),
            new("data-display", "数据展示", "Card、Table、Calendar、Segmented、Statistic 等。"),
            new("feedback", "反馈", "Alert、Drawer、Progress、Message、Modal、Notification。"),
            new("other", "其他", "ConfigProvider 在 WPF 中的映射方式。"),
        ];
    }

    private static UserControl CreatePage(string key)
    {
        return key switch
        {
            "overview" => new OverviewPage(),
            "general" => new GeneralPage(),
            "layout" => new LayoutPage(),
            "navigation" => new NavigationPage(),
            "data-entry" => new DataEntryPage(),
            "data-display" => new DataDisplayPage(),
            "feedback" => new FeedbackPage(),
            "other" => new OtherPage(),
            _ => new OverviewPage(),
        };
    }

    private void ApplySceneOverrides(ThemePreset preset)
    {
        _sceneOverrides.Clear();

        switch (preset.SceneKind)
        {
            case ThemeSceneKind.Illustration:
                SetBrushOverride(AntdResourceKeys.BrushText, ParseColor("#2F2A28"));
                SetBrushOverride(AntdResourceKeys.BrushTextSecondary, ParseColor("#615651"));
                SetBrushOverride(AntdResourceKeys.BrushTextTertiary, ParseColor("#988C85"));
                SetBrushOverride(AntdResourceKeys.BrushBgContainer, ParseColor("#FFFDFC"));
                SetBrushOverride(AntdResourceKeys.BrushBgElevated, ParseColor("#FFF8F8"));
                SetBrushOverride(AntdResourceKeys.BrushBgSpotlight, Mix(ToWpfColor(preset.Seed.PrimaryColor), Colors.White, 0.84d));
                SetBrushOverride(AntdResourceKeys.BrushFillSecondary, ParseColor("#E8F2FF"));
                SetBrushOverride(AntdResourceKeys.BrushFillTertiary, ParseColor("#DDEBFA"));
                SetBrushOverride(AntdResourceKeys.BrushFillQuaternary, ParseColor("#F7EFE9"));
                SetBrushOverride(AntdResourceKeys.BrushBorder, ParseColor("#363130"));
                SetBrushOverride(AntdResourceKeys.BrushBorderSecondary, ParseColor("#45403D"));
                SetBrushOverride(AntdResourceKeys.BrushFocusOutline, Mix(ToWpfColor(preset.Seed.PrimaryColor), Colors.White, 0.35d));
                SetBrushOverride(AntdResourceKeys.BrushTagDefaultBackground, ParseColor("#F0F6FF"));
                SetBrushOverride(AntdResourceKeys.BrushTagDefaultForeground, ParseColor("#2F2A28"));
                SetEffectOverride(AntdResourceKeys.ShadowCard, ParseColor("#171212"), 12d, 2d, 0.18d);
                SetEffectOverride(AntdResourceKeys.ShadowPopup, ParseColor("#171212"), 20d, 4d, 0.22d);
                SetEffectOverride(AntdResourceKeys.ShadowModal, ParseColor("#171212"), 28d, 8d, 0.26d);
                break;
            case ThemeSceneKind.Cyber:
                SetBrushOverride(AntdResourceKeys.BrushText, ParseColor("#E7F5E5"));
                SetBrushOverride(AntdResourceKeys.BrushTextSecondary, ParseColor("#A7B9A3"));
                SetBrushOverride(AntdResourceKeys.BrushTextTertiary, ParseColor("#6D816A"));
                SetBrushOverride(AntdResourceKeys.BrushBgBase, ParseColor("#050805"));
                SetBrushOverride(AntdResourceKeys.BrushBgContainer, ParseColor("#0B0E0B"));
                SetBrushOverride(AntdResourceKeys.BrushBgElevated, ParseColor("#111411"));
                SetBrushOverride(AntdResourceKeys.BrushBgSpotlight, ParseColor("#163319"));
                SetBrushOverride(AntdResourceKeys.BrushFillSecondary, ParseColor("#131B13"));
                SetBrushOverride(AntdResourceKeys.BrushFillTertiary, ParseColor("#182318"));
                SetBrushOverride(AntdResourceKeys.BrushFillQuaternary, ParseColor("#1D261D"));
                SetBrushOverride(AntdResourceKeys.BrushBorder, ParseColor("#49FF43"));
                SetBrushOverride(AntdResourceKeys.BrushBorderSecondary, ParseColor("#2A8F2D"));
                SetBrushOverride(AntdResourceKeys.BrushFocusOutline, ParseColor("#6AFF61"));
                SetBrushOverride(AntdResourceKeys.BrushTagDefaultBackground, ParseColor("#142214"));
                SetBrushOverride(AntdResourceKeys.BrushTagDefaultForeground, ParseColor("#D8EFD4"));
                SetEffectOverride(AntdResourceKeys.ShadowCard, ParseColor("#37FF2E"), 14d, 0d, 0.34d);
                SetEffectOverride(AntdResourceKeys.ShadowPopup, ParseColor("#37FF2E"), 24d, 0d, 0.44d);
                SetEffectOverride(AntdResourceKeys.ShadowModal, ParseColor("#37FF2E"), 32d, 0d, 0.52d);
                break;
            case ThemeSceneKind.Neutral:
                SetBrushOverride(AntdResourceKeys.BrushText, ParseColor("#18181B"));
                SetBrushOverride(AntdResourceKeys.BrushTextSecondary, ParseColor("#52525B"));
                SetBrushOverride(AntdResourceKeys.BrushTextTertiary, ParseColor("#8D8D97"));
                SetBrushOverride(AntdResourceKeys.BrushBgContainer, ParseColor("#FFFFFF"));
                SetBrushOverride(AntdResourceKeys.BrushBgElevated, ParseColor("#FCFCFC"));
                SetBrushOverride(AntdResourceKeys.BrushBgSpotlight, ParseColor("#F4F4F5"));
                SetBrushOverride(AntdResourceKeys.BrushFillSecondary, ParseColor("#F4F4F5"));
                SetBrushOverride(AntdResourceKeys.BrushFillTertiary, ParseColor("#ECECEE"));
                SetBrushOverride(AntdResourceKeys.BrushFillQuaternary, ParseColor("#E7E7EB"));
                SetBrushOverride(AntdResourceKeys.BrushBorder, ParseColor("#27272A"));
                SetBrushOverride(AntdResourceKeys.BrushBorderSecondary, ParseColor("#3F3F46"));
                SetBrushOverride(AntdResourceKeys.BrushFocusOutline, ParseColor("#A1A1AA"));
                SetEffectOverride(AntdResourceKeys.ShadowCard, ParseColor("#151515"), 10d, 2d, 0.10d);
                SetEffectOverride(AntdResourceKeys.ShadowPopup, ParseColor("#151515"), 18d, 4d, 0.14d);
                SetEffectOverride(AntdResourceKeys.ShadowModal, ParseColor("#151515"), 26d, 8d, 0.18d);
                break;
            case ThemeSceneKind.Glass:
                SetBrushOverride(AntdResourceKeys.BrushText, ParseColor("#183347"));
                SetBrushOverride(AntdResourceKeys.BrushTextSecondary, ParseColor("#4E6B81"));
                SetBrushOverride(AntdResourceKeys.BrushTextTertiary, ParseColor("#87A1B2"));
                SetBrushOverride(AntdResourceKeys.BrushBgContainer, ParseColor("#F9FDFF"));
                SetBrushOverride(AntdResourceKeys.BrushBgElevated, ParseColor("#F4FBFF"));
                SetBrushOverride(AntdResourceKeys.BrushBgSpotlight, Mix(ToWpfColor(preset.Seed.PrimaryColor), Colors.White, 0.9d));
                SetBrushOverride(AntdResourceKeys.BrushFillSecondary, ParseColor("#EEF8FF"));
                SetBrushOverride(AntdResourceKeys.BrushFillTertiary, ParseColor("#E1F1FC"));
                SetBrushOverride(AntdResourceKeys.BrushFillQuaternary, ParseColor("#D7EBFA"));
                SetBrushOverride(AntdResourceKeys.BrushBorder, ParseColor("#8BC5EA"));
                SetBrushOverride(AntdResourceKeys.BrushBorderSecondary, ParseColor("#B9DDF2"));
                SetEffectOverride(AntdResourceKeys.ShadowCard, ParseColor("#7EC9FF"), 16d, 2d, 0.16d);
                SetEffectOverride(AntdResourceKeys.ShadowPopup, ParseColor("#7EC9FF"), 24d, 4d, 0.22d);
                SetEffectOverride(AntdResourceKeys.ShadowModal, ParseColor("#7EC9FF"), 32d, 8d, 0.28d);
                break;
            case ThemeSceneKind.DarkConsole:
                SetBrushOverride(AntdResourceKeys.BrushText, ParseColor("#ECF4FF"));
                SetBrushOverride(AntdResourceKeys.BrushTextSecondary, ParseColor("#B6C6D8"));
                SetBrushOverride(AntdResourceKeys.BrushTextTertiary, ParseColor("#7E91A8"));
                SetBrushOverride(AntdResourceKeys.BrushBgBase, ParseColor("#0B1016"));
                SetBrushOverride(AntdResourceKeys.BrushBgContainer, ParseColor("#121A24"));
                SetBrushOverride(AntdResourceKeys.BrushBgElevated, ParseColor("#17212D"));
                SetBrushOverride(AntdResourceKeys.BrushBgSpotlight, ParseColor("#1A3650"));
                SetBrushOverride(AntdResourceKeys.BrushFillSecondary, ParseColor("#1B2633"));
                SetBrushOverride(AntdResourceKeys.BrushFillTertiary, ParseColor("#223041"));
                SetBrushOverride(AntdResourceKeys.BrushFillQuaternary, ParseColor("#27384A"));
                SetBrushOverride(AntdResourceKeys.BrushBorder, ParseColor("#426178"));
                SetBrushOverride(AntdResourceKeys.BrushBorderSecondary, ParseColor("#324A61"));
                SetEffectOverride(AntdResourceKeys.ShadowCard, ParseColor("#000000"), 14d, 2d, 0.24d);
                SetEffectOverride(AntdResourceKeys.ShadowPopup, ParseColor("#000000"), 24d, 4d, 0.30d);
                SetEffectOverride(AntdResourceKeys.ShadowModal, ParseColor("#000000"), 32d, 8d, 0.36d);
                break;
            case ThemeSceneKind.Material:
                SetBrushOverride(AntdResourceKeys.BrushBgSpotlight, Mix(ToWpfColor(preset.Seed.PrimaryColor), Colors.White, 0.9d));
                SetBrushOverride(AntdResourceKeys.BrushFillSecondary, ParseColor("#F2F0FF"));
                SetBrushOverride(AntdResourceKeys.BrushFillTertiary, ParseColor("#E8E4FF"));
                SetBrushOverride(AntdResourceKeys.BrushBorder, ParseColor("#7B7FEA"));
                SetBrushOverride(AntdResourceKeys.BrushBorderSecondary, ParseColor("#BEC3FF"));
                SetEffectOverride(AntdResourceKeys.ShadowPopup, ParseColor("#5B6CFF"), 22d, 4d, 0.18d);
                break;
            case ThemeSceneKind.Cartoon:
                SetBrushOverride(AntdResourceKeys.BrushText, ParseColor("#332626"));
                SetBrushOverride(AntdResourceKeys.BrushTextSecondary, ParseColor("#6C5656"));
                SetBrushOverride(AntdResourceKeys.BrushBgContainer, ParseColor("#FFFDF8"));
                SetBrushOverride(AntdResourceKeys.BrushBgElevated, ParseColor("#FFF8F2"));
                SetBrushOverride(AntdResourceKeys.BrushBgSpotlight, ParseColor("#FFF0D8"));
                SetBrushOverride(AntdResourceKeys.BrushFillSecondary, ParseColor("#FFF2DE"));
                SetBrushOverride(AntdResourceKeys.BrushFillTertiary, ParseColor("#FFE8CD"));
                SetBrushOverride(AntdResourceKeys.BrushFillQuaternary, ParseColor("#FFE1BE"));
                SetBrushOverride(AntdResourceKeys.BrushBorder, ParseColor("#6A5146"));
                SetBrushOverride(AntdResourceKeys.BrushBorderSecondary, ParseColor("#8A6E62"));
                SetEffectOverride(AntdResourceKeys.ShadowPopup, ParseColor("#FFB36A"), 20d, 4d, 0.16d);
                break;
        }
    }

    private void SetBrushOverride(string key, Color color)
    {
        _sceneOverrides[key] = CreateSolidBrush(color);
    }

    private void SetEffectOverride(string key, Color color, double blur, double depth, double opacity)
    {
        _sceneOverrides[key] = CreateShadowEffect(color, blur, depth, opacity);
    }

    private static Brush CreatePatternBrush(ThemePatternKind patternKind, Color ink, Color accent)
    {
        return patternKind switch
        {
            ThemePatternKind.Illustration => CreateIllustrationPatternBrush(ink, accent),
            ThemePatternKind.Matrix => CreateMatrixPatternBrush(ink),
            ThemePatternKind.Grid => CreateGridPatternBrush(ink, accent),
            _ => Brushes.Transparent,
        };
    }

    private static Brush CreateIllustrationPatternBrush(Color ink, Color accent)
    {
        var tile = new Canvas { Width = 260, Height = 220, Background = Brushes.Transparent };
        tile.Children.Add(CreateNotebook(16, 18, 18, ink, 0.34d));
        tile.Children.Add(CreateNotebook(176, 36, -12, Mix(ink, accent, 0.45d), 0.28d));
        tile.Children.Add(CreateStar(112, 82, accent, 0.45d));
        tile.Children.Add(CreatePlane(20, 152, ink, 0.3d));
        tile.Children.Add(CreateCloud(166, 154, ink, 0.24d));
        tile.Children.Add(CreatePencil(102, 12, accent, 0.26d));
        tile.Children.Add(CreatePencil(204, 142, ink, 0.2d));
        return CreateTiledVisualBrush(tile);
    }

    private static Brush CreateMatrixPatternBrush(Color ink)
    {
        var tile = new Canvas { Width = 180, Height = 180, Background = Brushes.Transparent };
        var rows = new[]
        {
            "010101",
            "101010",
            "001101",
            "110010",
            "011001",
            "100111",
        };

        for (var row = 0; row < rows.Length; row++)
        {
            for (var column = 0; column < rows[row].Length; column++)
            {
                var glyph = new TextBlock
                {
                    Text = rows[row][column].ToString(),
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = column % 2 == 0 ? 32 : 26,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = CreateSolidBrush(WithOpacity(ink, 0.18d + ((row + column) % 3) * 0.1d)),
                };

                Canvas.SetLeft(glyph, 18 + column * 26);
                Canvas.SetTop(glyph, 8 + row * 28);
                tile.Children.Add(glyph);
            }
        }

        return CreateTiledVisualBrush(tile);
    }

    private static Brush CreateGridPatternBrush(Color ink, Color accent)
    {
        var tile = new Canvas { Width = 160, Height = 160, Background = Brushes.Transparent };
        for (var offset = 0; offset <= 160; offset += 40)
        {
            tile.Children.Add(new Line
            {
                X1 = offset,
                Y1 = 0,
                X2 = offset,
                Y2 = 160,
                Stroke = CreateSolidBrush(WithOpacity(ink, 0.16d)),
                StrokeThickness = 1,
            });
            tile.Children.Add(new Line
            {
                X1 = 0,
                Y1 = offset,
                X2 = 160,
                Y2 = offset,
                Stroke = CreateSolidBrush(WithOpacity(ink, 0.16d)),
                StrokeThickness = 1,
            });
        }

        var dot = new Ellipse
        {
            Width = 16,
            Height = 16,
            Fill = CreateSolidBrush(WithOpacity(accent, 0.28d)),
        };
        Canvas.SetLeft(dot, 112);
        Canvas.SetTop(dot, 108);
        tile.Children.Add(dot);

        return CreateTiledVisualBrush(tile);
    }

    private static Brush CreateTiledVisualBrush(FrameworkElement visual)
    {
        return new VisualBrush(visual)
        {
            Stretch = Stretch.None,
            TileMode = TileMode.Tile,
            Viewbox = new Rect(0, 0, visual.Width, visual.Height),
            ViewboxUnits = BrushMappingMode.Absolute,
            Viewport = new Rect(0, 0, visual.Width, visual.Height),
            ViewportUnits = BrushMappingMode.Absolute,
            AlignmentX = AlignmentX.Left,
            AlignmentY = AlignmentY.Top,
        };
    }

    private static FrameworkElement CreateNotebook(double left, double top, double angle, Color stroke, double opacity)
    {
        var frame = new Border
        {
            Width = 72,
            Height = 88,
            CornerRadius = new CornerRadius(14),
            BorderBrush = CreateSolidBrush(WithOpacity(stroke, opacity)),
            BorderThickness = new Thickness(3),
            Background = Brushes.Transparent,
            RenderTransformOrigin = new Point(0.5, 0.5),
            RenderTransform = new RotateTransform(angle),
        };

        var details = new Canvas();
        details.Children.Add(new Line { X1 = 22, Y1 = 0, X2 = 22, Y2 = 88, Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)), StrokeThickness = 2 });
        details.Children.Add(new Line { X1 = 32, Y1 = 22, X2 = 56, Y2 = 16, Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)), StrokeThickness = 2 });
        details.Children.Add(new Line { X1 = 32, Y1 = 34, X2 = 58, Y2 = 28, Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)), StrokeThickness = 2 });
        details.Children.Add(new Line { X1 = 32, Y1 = 46, X2 = 58, Y2 = 40, Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)), StrokeThickness = 2 });
        frame.Child = details;

        Canvas.SetLeft(frame, left);
        Canvas.SetTop(frame, top);
        return frame;
    }

    private static FrameworkElement CreatePlane(double left, double top, Color stroke, double opacity)
    {
        var plane = new Polygon
        {
            Points = new PointCollection { new(0, 22), new(72, 0), new(24, 50), new(18, 28) },
            Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)),
            StrokeThickness = 3,
            Fill = Brushes.Transparent,
        };
        Canvas.SetLeft(plane, left);
        Canvas.SetTop(plane, top);
        return plane;
    }

    private static FrameworkElement CreatePencil(double left, double top, Color stroke, double opacity)
    {
        var grid = new Grid
        {
            Width = 84,
            Height = 18,
            RenderTransformOrigin = new Point(0.5, 0.5),
            RenderTransform = new RotateTransform(-18),
        };
        grid.Children.Add(new Border
        {
            CornerRadius = new CornerRadius(9),
            BorderBrush = CreateSolidBrush(WithOpacity(stroke, opacity)),
            BorderThickness = new Thickness(3),
            Background = Brushes.Transparent,
        });
        grid.Children.Add(new Line
        {
            X1 = 60,
            Y1 = 0,
            X2 = 60,
            Y2 = 18,
            Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)),
            StrokeThickness = 2,
        });
        Canvas.SetLeft(grid, left);
        Canvas.SetTop(grid, top);
        return grid;
    }

    private static FrameworkElement CreateStar(double left, double top, Color fill, double opacity)
    {
        var star = new Polygon
        {
            Points = new PointCollection
            {
                new(12, 0), new(16, 8), new(26, 10), new(18, 17), new(20, 28), new(12, 22), new(4, 28), new(6, 17), new(-2, 10), new(8, 8),
            },
            Fill = CreateSolidBrush(WithOpacity(fill, opacity)),
            Stroke = CreateSolidBrush(WithOpacity(fill, opacity)),
            StrokeThickness = 2,
        };
        Canvas.SetLeft(star, left);
        Canvas.SetTop(star, top);
        return star;
    }

    private static FrameworkElement CreateCloud(double left, double top, Color stroke, double opacity)
    {
        var canvas = new Canvas { Width = 70, Height = 40 };
        canvas.Children.Add(new Ellipse { Width = 28, Height = 20, Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)), StrokeThickness = 3, Fill = Brushes.Transparent });
        canvas.Children.Add(new Ellipse { Width = 26, Height = 18, Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)), StrokeThickness = 3, Fill = Brushes.Transparent });
        canvas.Children.Add(new Ellipse { Width = 24, Height = 16, Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)), StrokeThickness = 3, Fill = Brushes.Transparent });
        Canvas.SetLeft(canvas.Children[0], 4);
        Canvas.SetTop(canvas.Children[0], 14);
        Canvas.SetLeft(canvas.Children[1], 22);
        Canvas.SetTop(canvas.Children[1], 6);
        Canvas.SetLeft(canvas.Children[2], 42);
        Canvas.SetTop(canvas.Children[2], 14);
        canvas.Children.Add(new Line { X1 = 10, Y1 = 28, X2 = 56, Y2 = 28, Stroke = CreateSolidBrush(WithOpacity(stroke, opacity)), StrokeThickness = 3 });
        Canvas.SetLeft(canvas, left);
        Canvas.SetTop(canvas, top);
        return canvas;
    }

    private static List<ThemePreset> CreateThemePresets()
    {
        return
        [
            new ThemePreset(ThemePresetKind.Default, ThemeSceneKind.Neutral, ThemePatternKind.Grid, "默认风格", "平衡、清爽，作为主题基线。", "更克制的边框和整洁的留白，适合验证默认主题下的稳定观感。", "先看这里，确认控件基础布局和弹层样式是正确的。", "浅色", AntdThemeMode.Light, CreateSeed("#1677FF", "#52C41A", "#FAAD14", "#FF4D4F", 10d, 14d, 40d), "#F6FBFF", "#EFF5FF", "#B9DAFF", "#D8EBFF", "#F5FBFF", "#B8D4EF", "#FFFDFC", "#F8FBFF", "#D0D9E4", "#D0D9E4", "#D0D7E2", "#A8D3FF", 0.12d, false),
            new ThemePreset(ThemePresetKind.Dark, ThemeSceneKind.DarkConsole, ThemePatternKind.None, "暗黑风格", "深色控制台，强调信息密度。", "冷色深背景与更明确的边框对比让输入与弹层在暗色下保持可读。", "重点看深色下 DatePicker 和 ComboBox 打开后的文字对比。", "深色", AntdThemeMode.Dark, CreateSeed("#2F88FF", "#49AA19", "#D89614", "#FF4D4F", 10d, 14d, 40d), "#0A1018", "#111926", "#173B60", "#14263F", "#101723", "#33495D", "#131B24", "#0F151D", "#33495D", "#33495D", "#4A6985", "#8BBFFF", 0d, false),
            new ThemePreset(ThemePresetKind.Mui, ThemeSceneKind.Material, ThemePatternKind.Grid, "类 MUI 风格", "柔和紫蓝与更圆润的层次。", "更像设计系统演示板，适合检查高圆角和轻阴影下的弹层关系。", "观察填充色和选中态是否在大圆角下仍足够清晰。", "浅色", AntdThemeMode.Light, CreateSeed("#5B6CFF", "#2E7D32", "#ED6C02", "#D32F2F", 14d, 14d, 40d), "#F8F8FF", "#F2F1FF", "#C5CBFF", "#E0D9FF", "#F5F3FF", "#C6CCFF", "#FFFFFF", "#FAF8FF", "#D8D1FF", "#D8D1FF", "#CBC4FF", "#B4C6FF", 0.14d, false),
            new ThemePreset(ThemePresetKind.Shadcn, ThemeSceneKind.Neutral, ThemePatternKind.None, "类 shadcn 风格", "中性色与硬朗分隔线。", "弱化大面积色块，把重点放在边界、文本和结构上。", "适合检查输入框与容器边界是否干净利落。", "浅色", AntdThemeMode.Light, CreateSeed("#18181B", "#16A34A", "#CA8A04", "#DC2626", 12d, 14d, 38d), "#FAFAFA", "#F6F6F7", "#D6D6DA", "#E7E7EA", "#FCFCFC", "#2A2A2E", "#FFFFFF", "#FCFCFC", "#2A2A2E", "#2A2A2E", "#3F3F46", "#C8C8D2", 0d, true),
            new ThemePreset(ThemePresetKind.Cartoon, ThemeSceneKind.Cartoon, ThemePatternKind.Illustration, "卡通风格", "更活泼的暖色和轻松的图形。", "适合产品展示或教育场景，按钮和面板会更像卡片贴纸。", "观察暖色主题下的高亮态是否仍然清楚。", "浅色", AntdThemeMode.Light, CreateSeed("#FF9A3D", "#6BD968", "#FFC84C", "#FF6464", 16d, 15d, 42d), "#FFF7EC", "#FFF2F8", "#FFD4A6", "#FFD5EA", "#FFF9F2", "#E7B889", "#FFFDFC", "#FFF7F1", "#E3BC96", "#E3BC96", "#C88D57", "#FFCB77", 0.18d, true),
            new ThemePreset(ThemePresetKind.Illustration, ThemeSceneKind.Illustration, ThemePatternKind.Illustration, "插画风格", "深描边、柔和底色和贴纸感背景。", "参考插画式控制面板，强调主题辨识度与更强的轮廓语言。", "重点检查浅色主题下的深描边、弹层外壳与日历内部是否统一。", "浅色", AntdThemeMode.Light, CreateSeed("#3F86FF", "#62C63B", "#FFB020", "#FF6767", 16d, 15d, 42d), "#FCFAF2", "#F9EFF5", "#D7E7FF", "#F6D7E4", "#FCFAF2", "#373130", "#FFF8F7", "#FFF3F5", "#373130", "#373130", "#383230", "#F7D68B", 0.24d, true),
            new ThemePreset(ThemePresetKind.Bootstrap, ThemeSceneKind.Neutral, ThemePatternKind.Grid, "类 Bootstrap 拟物化风格", "更明确的容器边界与蓝灰层次。", "适合后台表单页，容器和控件之间会有更清楚的边界关系。", "关注列表、分页和按钮边框是否稳定统一。", "浅色", AntdThemeMode.Light, CreateSeed("#0D6EFD", "#198754", "#FFC107", "#DC3545", 8d, 14d, 40d), "#F3F7FC", "#ECF2F8", "#C2D7F5", "#DEEAF7", "#F7FBFF", "#7D9AB7", "#FFFFFF", "#F7FAFD", "#A5BDD7", "#A5BDD7", "#6E86A0", "#8AB4F8", 0.12d, false),
            new ThemePreset(ThemePresetKind.Glass, ThemeSceneKind.Glass, ThemePatternKind.None, "玻璃风格", "轻透高光与更柔和的蓝调。", "适合检查高亮背景、卡片阴影和弹层浮起感。", "打开 DatePicker 可重点观察弹层边界和内容衬托。", "浅色", AntdThemeMode.Light, CreateSeed("#2F8CFF", "#34C759", "#FF9F0A", "#FF453A", 20d, 14d, 42d), "#EEF7FF", "#F9FCFF", "#B7E4FF", "#E1F2FF", "#F7FCFF", "#B9DFF4", "#FCFFFF", "#F2FAFF", "#B9DFF4", "#B9DFF4", "#8BC5EA", "#88D7FF", 0d, false),
            new ThemePreset(ThemePresetKind.Geek, ThemeSceneKind.Cyber, ThemePatternKind.Matrix, "极客风格", "霓虹绿边框与黑色控制台。", "参考极客与矩阵风格，强调发光描边、终端背景和高对比输入框。", "重点检查深色弹层、绿色强调色和聚焦态是否足够统一。", "深色", AntdThemeMode.Dark, CreateSeed("#49FF43", "#49FF43", "#FADB14", "#FF5D5D", 8d, 14d, 38d), "#050805", "#090C09", "#103D11", "#102910", "#070A07", "#49FF43", "#0B0E0B", "#070907", "#49FF43", "#49FF43", "#49FF43", "#49FF43", 0.52d, true),
        ];
    }

    private static AntdSeedToken CreateSeed(string primary, string success, string warning, string error, double borderRadius, double fontSizeBase, double controlHeightMiddle)
    {
        return new AntdSeedToken
        {
            PrimaryColor = AntdColor.Parse(primary),
            SuccessColor = AntdColor.Parse(success),
            WarningColor = AntdColor.Parse(warning),
            ErrorColor = AntdColor.Parse(error),
            BorderRadius = borderRadius,
            FontSizeBase = fontSizeBase,
            ControlHeightSmall = Math.Max(28d, controlHeightMiddle - 8d),
            ControlHeightMiddle = controlHeightMiddle,
            ControlHeightLarge = controlHeightMiddle + 8d,
        };
    }

    private static SolidColorBrush CreateSolidBrush(Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze();
        return brush;
    }

    private static LinearGradientBrush CreateLinearBrush(Color start, Color end)
    {
        var brush = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 1),
        };
        brush.GradientStops.Add(new GradientStop(start, 0));
        brush.GradientStops.Add(new GradientStop(end, 1));
        brush.Freeze();
        return brush;
    }

    private static RadialGradientBrush CreateRadialBrush(Color color)
    {
        var outer = Color.FromArgb(0, color.R, color.G, color.B);
        var brush = new RadialGradientBrush
        {
            Center = new Point(0.5, 0.5),
            GradientOrigin = new Point(0.5, 0.5),
            RadiusX = 0.5,
            RadiusY = 0.5,
        };
        brush.GradientStops.Add(new GradientStop(color, 0));
        brush.GradientStops.Add(new GradientStop(outer, 1));
        brush.Freeze();
        return brush;
    }

    private static DropShadowEffect CreateShadowEffect(Color color, double blur, double depth, double opacity)
    {
        var effect = new DropShadowEffect
        {
            Color = color,
            BlurRadius = blur,
            ShadowDepth = depth,
            Direction = 270d,
            Opacity = opacity,
        };
        effect.Freeze();
        return effect;
    }

    private static Color ParseColor(string value)
    {
        return (Color)ColorConverter.ConvertFromString(value)!;
    }

    private static string ToHexRgb(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    private static Color ToWpfColor(AntdColor color)
    {
        return Color.FromArgb(color.A, color.R, color.G, color.B);
    }

    private static Color Mix(Color source, Color target, double targetWeight)
    {
        var sourceWeight = 1d - targetWeight;
        return Color.FromArgb(
            255,
            (byte)Math.Clamp(source.R * sourceWeight + target.R * targetWeight, 0d, 255d),
            (byte)Math.Clamp(source.G * sourceWeight + target.G * targetWeight, 0d, 255d),
            (byte)Math.Clamp(source.B * sourceWeight + target.B * targetWeight, 0d, 255d));
    }

    private static Color WithOpacity(Color color, double opacity)
    {
        return Color.FromArgb((byte)Math.Clamp(opacity * 255d, 0d, 255d), color.R, color.G, color.B);
    }

    private enum ThemePresetKind
    {
        Default,
        Dark,
        Mui,
        Shadcn,
        Cartoon,
        Illustration,
        Bootstrap,
        Glass,
        Geek,
    }

    private enum ThemeSceneKind
    {
        Neutral,
        DarkConsole,
        Material,
        Cartoon,
        Illustration,
        Glass,
        Cyber,
    }

    private enum ThemePatternKind
    {
        None,
        Grid,
        Illustration,
        Matrix,
    }

    private sealed class ThemePreset(
        ThemePresetKind kind,
        ThemeSceneKind sceneKind,
        ThemePatternKind patternKind,
        string name,
        string caption,
        string description,
        string footerHint,
        string modeText,
        AntdThemeMode mode,
        AntdSeedToken seed,
        string shellStart,
        string shellEnd,
        string glowPrimary,
        string glowSecondary,
        string sidebarBackground,
        string sidebarBorder,
        string previewStart,
        string previewEnd,
        string previewBorder,
        string surfaceBorder,
        string patternInk,
        string patternAccent,
        double patternOpacity,
        bool hardOutline)
    {
        public ThemePresetKind Kind { get; } = kind;
        public ThemeSceneKind SceneKind { get; } = sceneKind;
        public ThemePatternKind PatternKind { get; } = patternKind;
        public string Name { get; } = name;
        public string Caption { get; } = caption;
        public string Description { get; } = description;
        public string FooterHint { get; } = footerHint;
        public string ModeText { get; } = modeText;
        public AntdThemeMode Mode { get; } = mode;
        public AntdSeedToken Seed { get; } = seed;
        public Color ShellStart { get; } = ParseColor(shellStart);
        public Color ShellEnd { get; } = ParseColor(shellEnd);
        public Color GlowPrimary { get; } = ParseColor(glowPrimary);
        public Color GlowSecondary { get; } = ParseColor(glowSecondary);
        public Color SidebarBackground { get; } = ParseColor(sidebarBackground);
        public Color SidebarBorder { get; } = ParseColor(sidebarBorder);
        public Color PreviewStart { get; } = ParseColor(previewStart);
        public Color PreviewEnd { get; } = ParseColor(previewEnd);
        public Color PreviewBorder { get; } = ParseColor(previewBorder);
        public Color SurfaceBorder { get; } = ParseColor(surfaceBorder);
        public Color PatternInk { get; } = ParseColor(patternInk);
        public Color PatternAccent { get; } = ParseColor(patternAccent);
        public double PatternOpacity { get; } = patternOpacity;
        public bool HardOutline { get; } = hardOutline;
    }

    private sealed record CatalogNavigationItem(string Key, string Title, string Caption);
}
