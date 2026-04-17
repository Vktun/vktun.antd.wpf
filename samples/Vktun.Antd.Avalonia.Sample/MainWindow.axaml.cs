using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Vktun.Antd.Avalonia.Sample.Catalog;

namespace Vktun.Antd.Avalonia.Sample;

public partial class MainWindow : Window
{
    private readonly List<ThemePreset> _themePresets = CreateThemePresets();

    public MainWindow()
    {
        InitializeComponent();

        ThemePresetSelector.ItemsSource = _themePresets;
        NavigationListBox.ItemsSource = CatalogControlCatalog.NavigationItems;
        ThemePresetSelector.SelectedIndex = 0;
        NavigationListBox.SelectedIndex = 0;
    }

    private void ThemePresetSelector_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ThemePresetSelector.SelectedItem is ThemePreset preset)
        {
            ApplyPreset(preset);
        }
    }

    private void NavigationListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (NavigationListBox.SelectedItem is CatalogNavigationItem item)
        {
            PageHost.Content = CatalogControlCatalog.CreatePage(item.Key);
        }
    }

    private void ApplyPreset(ThemePreset preset)
    {
        if (Application.Current is null)
        {
            return;
        }

        AntdThemeManager.Current.Apply(Application.Current, preset.Mode, preset.Seed);
        Application.Current.Resources[AntdResourceKeys.BrushBgLayout] = Brush(preset.LayoutBackground);
        Application.Current.Resources[AntdResourceKeys.BrushBgContainer] = Brush(preset.ContainerBackground);
        Application.Current.Resources[AntdResourceKeys.BrushBorderSecondary] = Brush(preset.Border);

        ThemeHeroTitleTextBlock.Text = preset.Name;
        ThemeHeroDescriptionTextBlock.Text = preset.Description;
        ThemeModeTag.Content = preset.Mode == AntdThemeMode.Dark ? "Dark" : "Light";
        ThemePrimaryTag.Content = preset.Seed.PrimaryColor.ToHex();
        ThemeShapeTag.Content = $"{preset.Seed.BorderRadius:0}px";

        RefreshCurrentPage();
    }

    private void RefreshCurrentPage()
    {
        if (NavigationListBox.SelectedItem is CatalogNavigationItem item)
        {
            PageHost.Content = CatalogControlCatalog.CreatePage(item.Key);
        }
    }

    private static SolidColorBrush Brush(string value)
    {
        return new SolidColorBrush(Color.Parse(value));
    }

    private static List<ThemePreset> CreateThemePresets()
    {
        return
        [
            new("默认风格", "平衡清爽", "平衡、清爽，作为主题基线。", AntdThemeMode.Light, Seed("#1677FF", "#52C41A", "#FAAD14", "#FF4D4F", 10), "#F6FBFF", "#FFFFFF", "#D0D9E4"),
            new("暗黑风格", "深色控制台", "深色背景与明确边界，检查暗色可读性。", AntdThemeMode.Dark, Seed("#2F88FF", "#49AA19", "#D89614", "#FF4D4F", 10), "#0A1018", "#121A24", "#33495D"),
            new("类 MUI 风格", "柔和紫蓝", "圆润层次和轻量边界。", AntdThemeMode.Light, Seed("#5B6CFF", "#2E7D32", "#ED6C02", "#D32F2F", 14), "#F8F8FF", "#FFFFFF", "#D8D1FF"),
            new("类 shadcn 风格", "中性色", "弱化色块，强调结构与文本。", AntdThemeMode.Light, Seed("#18181B", "#16A34A", "#CA8A04", "#DC2626", 12), "#FAFAFA", "#FFFFFF", "#3F3F46"),
            new("卡通风格", "暖色贴纸感", "更活泼的暖色和圆角。", AntdThemeMode.Light, Seed("#FF9A3D", "#6BD968", "#FFC84C", "#FF6464", 16), "#FFF7EC", "#FFFDFC", "#E3BC96"),
            new("插画风格", "深描边", "深描边、柔和底色和贴纸感背景。", AntdThemeMode.Light, Seed("#3F86FF", "#62C63B", "#FFB020", "#FF6767", 16), "#FCFAF2", "#FFF8F7", "#373130"),
            new("类 Bootstrap 风格", "蓝灰层次", "明确容器边界和后台表单感。", AntdThemeMode.Light, Seed("#0D6EFD", "#198754", "#FFC107", "#DC3545", 8), "#F3F7FC", "#FFFFFF", "#A5BDD7"),
            new("玻璃风格", "轻透蓝调", "轻透高光和柔和蓝调。", AntdThemeMode.Light, Seed("#2F8CFF", "#34C759", "#FF9F0A", "#FF453A", 20), "#EEF7FF", "#FCFFFF", "#B9DFF4"),
            new("极客风格", "霓虹控制台", "霓虹绿边框与黑色控制台。", AntdThemeMode.Dark, Seed("#49FF43", "#49FF43", "#FADB14", "#FF5D5D", 8), "#050805", "#0B0E0B", "#49FF43"),
        ];
    }

    private static AntdSeedToken Seed(string primary, string success, string warning, string error, double radius)
    {
        return new AntdSeedToken
        {
            PrimaryColor = AntdColor.Parse(primary),
            SuccessColor = AntdColor.Parse(success),
            WarningColor = AntdColor.Parse(warning),
            ErrorColor = AntdColor.Parse(error),
            BorderRadius = radius,
        };
    }

    private sealed record ThemePreset(
        string Name,
        string Caption,
        string Description,
        AntdThemeMode Mode,
        AntdSeedToken Seed,
        string LayoutBackground,
        string ContainerBackground,
        string Border);
}
