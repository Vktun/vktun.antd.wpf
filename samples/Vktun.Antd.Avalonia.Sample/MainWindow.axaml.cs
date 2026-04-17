using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using AntdAlert = Vktun.Antd.Avalonia.Alert;
using AntdButton = Vktun.Antd.Avalonia.Button;
using AntdCalendar = Vktun.Antd.Avalonia.Calendar;
using AntdComboBox = Vktun.Antd.Avalonia.ComboBox;
using AntdGrid = Vktun.Antd.Avalonia.Grid;
using AntdList = Vktun.Antd.Avalonia.List;
using AntdProgress = Vktun.Antd.Avalonia.Progress;
using AntdSlider = Vktun.Antd.Avalonia.Slider;
using AntdSwitch = Vktun.Antd.Avalonia.Switch;

namespace Vktun.Antd.Avalonia.Sample;

public partial class MainWindow : Window
{
    private readonly ResourceDictionary _sceneOverrides = new();
    private readonly List<ThemePreset> _themePresets = CreateThemePresets();
    private readonly List<CatalogNavigationItem> _navigationItems = CreateNavigationItems();

    public MainWindow()
    {
        InitializeComponent();

        if (Application.Current is not null &&
            !Application.Current.Resources.MergedDictionaries.Contains(_sceneOverrides))
        {
            Application.Current.Resources.MergedDictionaries.Add(_sceneOverrides);
        }

        ThemePresetSelector.ItemsSource = _themePresets;
        NavigationListBox.ItemsSource = _navigationItems;
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
            PageHost.Content = CreatePage(item.Key);
        }
    }

    private void ApplyPreset(ThemePreset preset)
    {
        if (Application.Current is null)
        {
            return;
        }

        AntdThemeManager.Current.Apply(Application.Current, preset.Mode, preset.Seed);
        _sceneOverrides.Clear();
        _sceneOverrides[AntdResourceKeys.BrushBgLayout] = Brush(preset.LayoutBackground);
        _sceneOverrides[AntdResourceKeys.BrushBgContainer] = Brush(preset.ContainerBackground);
        _sceneOverrides[AntdResourceKeys.BrushBorderSecondary] = Brush(preset.Border);

        ThemeHeroTitleTextBlock.Text = preset.Name;
        ThemeHeroDescriptionTextBlock.Text = preset.Description;
        ThemeModeTag.Content = preset.Mode == AntdThemeMode.Dark ? "Dark" : "Light";
        ThemePrimaryTag.Content = preset.Seed.PrimaryColor.ToHex();
        ThemeShapeTag.Content = $"{preset.Seed.BorderRadius:0}px";
    }

    private static Control CreatePage(string key)
    {
        return key switch
        {
            "overview" => CreateOverviewPage(),
            "general" => CreateGeneralPage(),
            "layout" => CreateLayoutPage(),
            "navigation" => CreateNavigationPage(),
            "data-entry" => CreateDataEntryPage(),
            "data-display" => CreateDataDisplayPage(),
            "feedback" => CreateFeedbackPage(),
            _ => CreateOtherPage(),
        };
    }

    private static Control CreateOverviewPage()
    {
        return Scroll("总览", "Avalonia 包已提供 WPF 侧同名控件外壳、主题 token 投影和服务入口。",
            Matrix("通用", "Button, IconButton, Typography, FloatButton"),
            Matrix("布局", "Divider, Grid, Space, Flex, Layout, Splitter"),
            Matrix("导航", "Breadcrumb, Dropdown, Menu, Pagination, Steps, Tabs"),
            Matrix("数据录入", "Input, PasswordInput, InputNumber, ComboBox, DatePicker, Checkbox, Radio, Switch, Slider, Form"),
            Matrix("数据展示", "Avatar, Badge, Calendar, Card, Collapse, Descriptions, Empty, List, Segmented, Statistic, Table, Tag, Timeline, Tooltip, Watermark"),
            Matrix("反馈", "Alert, Drawer, Message, Modal, Notification, Popconfirm, Progress, Result, Skeleton, Spin"));
    }

    private static Control CreateGeneralPage()
    {
        return Scroll("通用", "基础操作入口和文本语义。",
            Section("Button / Typography / FloatButton",
                new AntdButton { Content = "Primary", Type = AntdButtonType.Primary },
                new AntdButton { Content = "Dashed", Type = AntdButtonType.Dashed },
                new Typography { Text = "Typography secondary", Type = AntdTypographyType.Secondary },
                new FloatButton { Content = "+", Shape = AntdFloatButtonShape.Circle }));
    }

    private static Control CreateLayoutPage()
    {
        return Scroll("布局", "布局容器使用 Avalonia 原生 panel 能力，并保留 Ant Design 命名。",
            Section("Layout / Space / Flex / Divider",
                new Vktun.Antd.Avalonia.Layout { Content = "Layout shell" },
                new Space { Orientation = Orientation.Horizontal, Spacing = 8, Children = { new TextBlock { Text = "A" }, new TextBlock { Text = "B" } } },
                new Flex { Orientation = Orientation.Horizontal, Spacing = 8, Children = { new AntdButton { Content = "Left" }, new AntdButton { Content = "Right" } } },
                new Divider { Content = "Divider" },
                new AntdGrid { ColumnDefinitions = new ColumnDefinitions("*,*"), Children = { new TextBlock { Text = "Grid cell" } } },
                new Splitter()));
    }

    private static Control CreateNavigationPage()
    {
        var tabs = new Tabs
        {
            Items = new List<TabPane>
            {
                new() { Key = "one", Header = "One", Content = "First tab" },
                new() { Key = "two", Header = "Two", Content = "Second tab" },
            },
        };

        return Scroll("导航", "导航控件保留 WPF 语义 API。",
            Section("Breadcrumb / Dropdown / Menu / Pagination / Steps / Tabs",
                new Breadcrumb { ItemsSource = new[] { "Home", "Components", "Tabs" } },
                new Dropdown { Content = "Dropdown trigger", Overlay = "Overlay content" },
                new Menu { ItemsSource = new[] { "Overview", "General", "Feedback" } },
                new Pagination { Total = 320, PageSize = 10, CurrentPage = 3 },
                new Steps { ItemsSource = new[] { "Start", "Review", "Ship" } },
                tabs));
    }

    private static Control CreateDataEntryPage()
    {
        return Scroll("数据录入", "输入类控件映射 WPF 的 size/status/variant 语义。",
            Section("Inputs",
                new Input { Watermark = "Input", Prefix = "@", Suffix = ".com" },
                new PasswordInput { Watermark = "Password" },
                new InputNumber { Minimum = 0, Maximum = 10, Value = 6, Precision = 0 },
                new AntdComboBox { ItemsSource = new[] { "Ocean", "Aurora", "Nebula" }, SelectedIndex = 0 },
                new DatePicker(),
                new Checkbox { Content = "Checkbox", IsChecked = true },
                new Radio { Content = "Radio", IsChecked = true },
                new AntdSwitch { IsChecked = true },
                new AntdSlider { Minimum = 0, Maximum = 100, Value = 64 },
                new Form { Children = { new Input { Watermark = "Form item" } } }));
    }

    private static Control CreateDataDisplayPage()
    {
        return Scroll("数据展示", "展示类控件提供同名 Avalonia API。",
            Section("Display",
                new Avatar { Content = "A" },
                new Badge { Count = 8, Content = "Inbox" },
                new AntdCalendar { SelectedDate = DateTimeOffset.Now },
                new Card { Title = "Card", Content = "Card content" },
                new Collapse { ItemsSource = new[] { "Panel 1", "Panel 2" } },
                new Descriptions { ItemsSource = new[] { "Name: Vktun", "Platform: Avalonia" } },
                new Empty(),
                new AntdList { ItemsSource = new[] { "List item A", "List item B" } },
                new Segmented { ItemsSource = new[] { "Daily", "Weekly", "Monthly" } },
                new Statistic { Title = "Conversion", Value = 0.2734, ValueFormat = "{0:P1}" },
                new Table { ItemsSource = new[] { "Row 1", "Row 2" } },
                new Tag { Content = "Success", Color = AntdTagColor.Success },
                new Timeline { ItemsSource = new[] { "Created", "Reviewed", "Published" } },
                new Tooltip { Text = "Tooltip", Content = "Hover target" },
                new Watermark { Text = "Vktun", Content = "Watermark content" }));
    }

    private static Control CreateFeedbackPage()
    {
        return Scroll("反馈", "反馈类控件和 overlay 服务共享 Ant Design token。",
            Section("Feedback",
                new AntdAlert { Message = "Info alert", Description = "Avalonia alert surface" },
                new Drawer { Content = "Drawer content", IsOpen = true },
                new Popconfirm { Title = "Confirm?", Content = "Delete" },
                new AntdProgress { Value = 72 },
                new Result { Status = AntdResultStatus.Success, Title = "Success" },
                new Skeleton { Active = true },
                new Spin { Tip = "Loading" }));
    }

    private static Control CreateOtherPage()
    {
        return Scroll("其他", "ConfigProvider 在 Avalonia 中对应 AntdThemeResources 和 AntdThemeManager。",
            Matrix("主题入口", "<antd:AntdThemeResources Theme=\"Light\" />"),
            Matrix("运行时切换", "AntdThemeManager.Current.Apply(Application.Current, mode, seed)"));
    }

    private static ScrollViewer Scroll(string title, string description, params Control[] sections)
    {
        var stack = new StackPanel { Spacing = 14 };
        stack.Children.Add(new TextBlock { Text = title, FontSize = 28, FontWeight = FontWeight.SemiBold });
        stack.Children.Add(new TextBlock { Text = description, TextWrapping = TextWrapping.Wrap });

        foreach (var section in sections)
        {
            stack.Children.Add(section);
        }

        return new ScrollViewer { Content = stack };
    }

    private static Border Matrix(string title, string body)
    {
        return new Border
        {
            Padding = new Thickness(14),
            CornerRadius = new CornerRadius(12),
            Background = DynamicBrush(AntdResourceKeys.BrushBgContainer),
            BorderBrush = DynamicBrush(AntdResourceKeys.BrushBorderSecondary),
            BorderThickness = new Thickness(1),
            Child = new StackPanel
            {
                Spacing = 4,
                Children =
                {
                    new TextBlock { Text = title, FontWeight = FontWeight.SemiBold },
                    new TextBlock { Text = body, TextWrapping = TextWrapping.Wrap },
                },
            },
        };
    }

    private static Border Section(string title, params Control[] controls)
    {
        var wrap = new WrapPanel { ItemWidth = 220, ItemHeight = 48 };
        foreach (var control in controls)
        {
            control.Margin = new Thickness(0, 0, 12, 12);
            wrap.Children.Add(control);
        }

        return new Border
        {
            Padding = new Thickness(16),
            CornerRadius = new CornerRadius(14),
            Background = DynamicBrush(AntdResourceKeys.BrushBgContainer),
            BorderBrush = DynamicBrush(AntdResourceKeys.BrushBorderSecondary),
            BorderThickness = new Thickness(1),
            Child = new StackPanel
            {
                Spacing = 12,
                Children =
                {
                    new TextBlock { Text = title, FontWeight = FontWeight.SemiBold },
                    wrap,
                },
            },
        };
    }

    private static IBrush DynamicBrush(string key)
    {
        return new SolidColorBrush(Colors.Transparent);
    }

    private static SolidColorBrush Brush(string value)
    {
        return new SolidColorBrush(Color.Parse(value));
    }

    private static List<CatalogNavigationItem> CreateNavigationItems()
    {
        return
        [
            new("overview", "总览", "组件覆盖矩阵。"),
            new("general", "通用", "Button、Typography、FloatButton。"),
            new("layout", "布局", "Divider、Grid、Space、Flex、Layout、Splitter。"),
            new("navigation", "导航", "Breadcrumb、Dropdown、Menu、Pagination、Steps、Tabs。"),
            new("data-entry", "数据录入", "Input、ComboBox、DatePicker、Switch 等。"),
            new("data-display", "数据展示", "Card、Table、Tag、Timeline 等。"),
            new("feedback", "反馈", "Alert、Drawer、Progress、Modal 服务等。"),
            new("other", "其他", "主题资源和运行时切换。"),
        ];
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

    private sealed record CatalogNavigationItem(string Key, string Title, string Caption);

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
