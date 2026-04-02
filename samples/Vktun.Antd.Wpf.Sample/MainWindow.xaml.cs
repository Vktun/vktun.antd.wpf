using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vktun.Antd.Wpf;

namespace Vktun.Antd.Wpf.Sample;

public partial class MainWindow : Window
{
    private readonly IMessageService _messageService = new MessageService();
    private readonly INotificationService _notificationService = new NotificationService();
    private readonly IModalService _modalService = new ModalService();
    private readonly List<ThemePreset> _themePresets = CreateThemePresets();

    public MainWindow()
    {
        InitializeComponent();
        InitializeThemeGallery();
    }

    private void InitializeThemeGallery()
    {
        ThemeCountTag.Content = $"{_themePresets.Count} 套主题";
        ThemePresetListBox.ItemsSource = _themePresets;
        ThemePresetListBox.SelectedIndex = 0;
    }

    private void ThemePresetListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!IsLoaded || ThemePresetListBox.SelectedItem is not ThemePreset preset)
        {
            return;
        }

        ApplyPreset(preset);
    }

    private void ApplyPreset(ThemePreset preset)
    {
        if (Application.Current is null)
        {
            return;
        }

        AntdThemeManager.Current.Apply(Application.Current, preset.Mode, preset.Seed);

        ThemeHeroTitleTextBlock.Text = preset.Name;
        ThemeHeroDescriptionTextBlock.Text = preset.Description;
        ThemeModeTag.Content = preset.ModeText;
        ThemePrimaryTag.Content = ToHexRgb(preset.Seed.PrimaryColor);
        ThemeShapeTag.Content = $"{preset.Seed.BorderRadius:0}px 圆角";
        ThemeFooterTextBlock.Text = preset.FooterHint;

        WindowBackdrop.Background = CreateLinearBrush(preset.ShellStart, preset.ShellEnd);
        TopLeftGlow.Fill = CreateRadialBrush(preset.GlowPrimary);
        BottomRightGlow.Fill = CreateRadialBrush(preset.GlowSecondary);
        ThemeRail.Background = CreateSolidBrush(preset.SidebarBackground);
        ThemeRail.BorderBrush = CreateSolidBrush(preset.SidebarBorder);
        PreviewShell.Background = CreateLinearBrush(preset.PreviewStart, preset.PreviewEnd);
        PreviewShell.BorderBrush = CreateSolidBrush(preset.PreviewBorder);
        ModalPreviewCard.Background = CreateSolidBrush(preset.SurfaceBackground);
        ModalPreviewCard.BorderBrush = CreateSolidBrush(preset.SurfaceBorder);
        InfoBannerBorder.BorderBrush = CreateSolidBrush(preset.SurfaceBorder);
    }

    private void ShowSuccessMessageButton_OnClick(object sender, RoutedEventArgs e)
    {
        _messageService.Show(this, "登录成功，欢迎回来。", MessageKind.Success);
    }

    private void ShowWarningMessageButton_OnClick(object sender, RoutedEventArgs e)
    {
        _messageService.Show(this, "部分控件仍在 Beta 状态。", MessageKind.Warning);
    }

    private void ShowNotificationButton_OnClick(object sender, RoutedEventArgs e)
    {
        _notificationService.Show(this, new NotificationRequest
        {
            Title = "部署完成",
            Description = "示例项目已切换到新的主题预设。",
            Kind = MessageKind.Info,
        });
    }

    private async void ShowModalButton_OnClick(object sender, RoutedEventArgs e)
    {
        var result = await _modalService.ShowAsync(this, new ModalRequest
        {
            Title = "确认切换配置",
            Content = new TextBlock
            {
                Text = "这个弹窗通过遮罩宿主注入到当前窗口，不依赖第三方窗口框架。",
                TextWrapping = TextWrapping.Wrap,
                Width = 320,
            },
            OkText = "确认",
            CancelText = "取消",
        });

        _messageService.Show(this, result == true ? "你选择了确认。" : "你取消了操作。", result == true ? MessageKind.Success : MessageKind.Info);
    }

    private static List<ThemePreset> CreateThemePresets()
    {
        return
        [
            new ThemePreset(
                name: "默认风格",
                caption: "Ant Design 原生语义与留白平衡。",
                description: "默认 Ant Design 语义色、柔和背景与通用圆角，适合作为所有业务页面的基线主题。",
                footerHint: "以默认风格为基础，可以继续朝行业感、品牌感或氛围感扩展。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#1677FF", "#52C41A", "#FAAD14", "#FF4D4F", 10d, 14d, 40d),
                shellStart: "#F6FBFF",
                shellEnd: "#EFF5FF",
                glowPrimary: "#A7D3FF",
                glowSecondary: "#CFE7FF",
                sidebarBackground: "#DDFBFEFF",
                sidebarBorder: "#A8D3FF",
                previewStart: "#FFFFFFFF",
                previewEnd: "#FFF9FEFF",
                previewBorder: "#D7E8FF",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#D7E8FF"),
            new ThemePreset(
                name: "暗黑风格",
                caption: "高对比深色容器，适合控制台与监控屏。",
                description: "深色容器与冷蓝高光能稳定支撑长时间阅读，同时保留 Ant Design 的品牌识别。",
                footerHint: "如果继续走深色路线，可以再叠加霓虹、石墨或工业化的表面材质。",
                modeText: "Dark",
                mode: AntdThemeMode.Dark,
                seed: CreateSeed("#1677FF", "#49AA19", "#D89614", "#FF4D4F", 10d, 14d, 40d),
                shellStart: "#0A1018",
                shellEnd: "#111B26",
                glowPrimary: "#17345B",
                glowSecondary: "#14263F",
                sidebarBackground: "#AA111924",
                sidebarBorder: "#31445A",
                previewStart: "#181F27",
                previewEnd: "#131A22",
                previewBorder: "#303E4E",
                surfaceBackground: "#221F1F1F",
                surfaceBorder: "#303E4E"),
            new ThemePreset(
                name: "紧凑风格",
                caption: "更密集的控件尺寸，适合信息密集型后台。",
                description: "在保持视觉秩序的前提下压缩控件高度和留白，适合表单与管理控制台。",
                footerHint: "紧凑并不等于拥挤，建议继续搭配更明确的分组和更克制的装饰。",
                modeText: "Compact",
                mode: AntdThemeMode.Compact,
                seed: CreateSeed("#1668DC", "#389E0D", "#D48806", "#D9363E", 8d, 13d, 36d),
                shellStart: "#F7F9FC",
                shellEnd: "#EEF3FA",
                glowPrimary: "#C2D8FF",
                glowSecondary: "#E2EEFF",
                sidebarBackground: "#DDFBFCFF",
                sidebarBorder: "#C8D7EA",
                previewStart: "#FFFFFFFF",
                previewEnd: "#FFF7FAFC",
                previewBorder: "#D9E3F0",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#D9E3F0"),
            new ThemePreset(
                name: "类 MUI 风格",
                caption: "偏紫蓝品牌色与柔和阴影，接近 Material 感。",
                description: "以偏冷紫蓝替代标准品牌蓝，搭配更细腻的层次和圆角，形成熟悉的 Material 气质。",
                footerHint: "如果想更接近 MUI，可以再叠加更系统化的阴影层级和信息密度控制。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#5B6CFF", "#2E7D32", "#ED6C02", "#D32F2F", 14d, 14d, 40d),
                shellStart: "#F8F9FF",
                shellEnd: "#F1F3FF",
                glowPrimary: "#C0C8FF",
                glowSecondary: "#D9DFFF",
                sidebarBackground: "#E6EEF9FF",
                sidebarBorder: "#B8C6FF",
                previewStart: "#FFFFFFFF",
                previewEnd: "#FFF9FAFF",
                previewBorder: "#CDD6FF",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#CDD6FF"),
            new ThemePreset(
                name: "类 shadcn 风格",
                caption: "黑白灰主导，强调克制和质感。",
                description: "去掉大面积品牌色后，界面把重心转向排版、边界和局部高亮，更适合工具型产品。",
                footerHint: "继续沿着 shadcn 路线时，建议同步收紧颜色种类和圆角尺度。",
                modeText: "Dark",
                mode: AntdThemeMode.Dark,
                seed: CreateSeed("#F8FAFC", "#22C55E", "#F59E0B", "#EF4444", 12d, 14d, 40d),
                shellStart: "#09090B",
                shellEnd: "#121216",
                glowPrimary: "#1F2937",
                glowSecondary: "#111827",
                sidebarBackground: "#B3131316",
                sidebarBorder: "#3A3A44",
                previewStart: "#18181B",
                previewEnd: "#111114",
                previewBorder: "#35353E",
                surfaceBackground: "#FF18181B",
                surfaceBorder: "#35353E"),
            new ThemePreset(
                name: "卡通风格",
                caption: "高饱和主色与更大圆角，交互更轻松。",
                description: "提高主色和成功色的饱和度，再用更圆润的控件比例，界面会呈现更轻快友好的情绪。",
                footerHint: "卡通风格适合继续叠加更粗的边界、更活泼的插图和更高亮度的背景。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#4F8CFF", "#52C41A", "#FAAD14", "#FF5B6A", 18d, 15d, 44d),
                shellStart: "#FFFBEF",
                shellEnd: "#FFF3E2",
                glowPrimary: "#FFE1A6",
                glowSecondary: "#FFD3B1",
                sidebarBackground: "#E6FFF9F0",
                sidebarBorder: "#FFCF74",
                previewStart: "#FFFFFBF5",
                previewEnd: "#FFFCEFFF",
                previewBorder: "#F0B56A",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#F0B56A"),
            new ThemePreset(
                name: "插画风格",
                caption: "暖色背景与手作配色，适合内容型产品。",
                description: "轻暖底色搭配手绘感的青绿与黄橙，会比标准企业后台更像一块有氛围的展示画布。",
                footerHint: "如果后面要做插画主题，建议再加背景图层和更明显的轮廓线。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#70B7FF", "#67C23A", "#F6C357", "#FF7875", 16d, 14d, 42d),
                shellStart: "#FFFDF6",
                shellEnd: "#FFF8EC",
                glowPrimary: "#FFE5A3",
                glowSecondary: "#D4F2E2",
                sidebarBackground: "#E6FFF9F5",
                sidebarBorder: "#E7CAA6",
                previewStart: "#FFFFFCF8",
                previewEnd: "#FFFDF6FF",
                previewBorder: "#D8B58A",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#D8B58A"),
            new ThemePreset(
                name: "类 Bootstrap 拟物化风格",
                caption: "直给的蓝色按钮、较实边框与较小圆角。",
                description: "减少柔和渐变与氛围层，强化按钮实体感和边框存在感，更像传统后台系统。",
                footerHint: "如果要继续向 Bootstrap 靠近，可以继续降低圆角并增强边框明度差。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#0D6EFD", "#198754", "#FFC107", "#DC3545", 6d, 14d, 38d),
                shellStart: "#F7F8FB",
                shellEnd: "#EEF1F6",
                glowPrimary: "#D5E4FF",
                glowSecondary: "#E5ECF7",
                sidebarBackground: "#EAF3FBFF",
                sidebarBorder: "#B7C8DE",
                previewStart: "#FFFFFFFF",
                previewEnd: "#FFF8FAFD",
                previewBorder: "#C7D3E2",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#AAB9CC"),
            new ThemePreset(
                name: "玻璃风格",
                caption: "半透明层次与冰感高光，适合品牌展示页。",
                description: "通过更轻的背景层和偏冷色高光强化玻璃感，让内容像浮在背景之上的卡片。",
                footerHint: "玻璃风格后续最值得补的是噪点、折射和更克制的文字密度。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#2F8CFF", "#34C759", "#FF9F0A", "#FF453A", 20d, 14d, 42d),
                shellStart: "#EEF7FF",
                shellEnd: "#F7FBFF",
                glowPrimary: "#B8E6FF",
                glowSecondary: "#DCEFFF",
                sidebarBackground: "#A6FFFFFF",
                sidebarBorder: "#C6E6FF",
                previewStart: "#C0FFFFFF",
                previewEnd: "#A6F3F9FF",
                previewBorder: "#D4ECFF",
                surfaceBackground: "#D9FFFFFF",
                surfaceBorder: "#D4ECFF"),
            new ThemePreset(
                name: "极客风格",
                caption: "黑底荧光绿，突出赛博控制台气质。",
                description: "高对比霓虹绿和纯黑背景能快速建立技术感，非常适合实验性或安全类产品展示。",
                footerHint: "极客风格可以继续向扫描线、矩阵纹理和等宽字体方向扩展。",
                modeText: "Dark",
                mode: AntdThemeMode.Dark,
                seed: CreateSeed("#39FF14", "#00E676", "#C6FF00", "#FF5252", 4d, 14d, 38d),
                shellStart: "#040704",
                shellEnd: "#091109",
                glowPrimary: "#0D5B0D",
                glowSecondary: "#0A360A",
                sidebarBackground: "#99070B07",
                sidebarBorder: "#2CED51",
                previewStart: "#101410",
                previewEnd: "#0A0D0A",
                previewBorder: "#3EFF62",
                surfaceBackground: "#FF171C17",
                surfaceBorder: "#3EFF62"),
            new ThemePreset(
                name: "海洋风格",
                caption: "蓝绿渐变和低饱和留白，适合数据驾驶舱。",
                description: "海蓝与青绿的组合会让界面更冷静，也更适合展示图表、地图和实时数据。",
                footerHint: "海洋主题适合再配合大面积图表和更轻的边界，保持信息呼吸感。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#1677FF", "#00BFA5", "#FFB020", "#F04438", 14d, 14d, 40d),
                shellStart: "#F3FBFF",
                shellEnd: "#EDF8F7",
                glowPrimary: "#B4E7FF",
                glowSecondary: "#BFEFD8",
                sidebarBackground: "#DCF8FFFF",
                sidebarBorder: "#97D8E8",
                previewStart: "#FFFFFFFF",
                previewEnd: "#FFF2FCFA",
                previewBorder: "#B9E3E5",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#B9E3E5"),
            new ThemePreset(
                name: "日落风格",
                caption: "暖橙与莓红混色，适合营销与活动页面。",
                description: "更有情绪的暖色渐变能快速拉高氛围感，用在活动页、品牌页会比标准后台更有表现力。",
                footerHint: "日落风格建议搭配更强的标题排版和更明确的主次按钮对比。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#FF7A45", "#95DE64", "#FFC53D", "#FF4D6D", 16d, 15d, 42d),
                shellStart: "#FFF7EF",
                shellEnd: "#FFF0F5",
                glowPrimary: "#FFD1A1",
                glowSecondary: "#FFC2D1",
                sidebarBackground: "#F3FFF8F1",
                sidebarBorder: "#FFC199",
                previewStart: "#FFFFFBF7",
                previewEnd: "#FFFFF5FB",
                previewBorder: "#FFCEB6",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#FFCEB6"),
            new ThemePreset(
                name: "森林风格",
                caption: "沉稳绿色系统，强调自然与可持续。",
                description: "以松绿和米白为主，界面会更安静稳重，适合园区、能源和可持续类业务。",
                footerHint: "森林主题后续可以加入纸张感底纹，让视觉更有材料感。",
                modeText: "Light",
                mode: AntdThemeMode.Light,
                seed: CreateSeed("#3A7D44", "#52C41A", "#D4B106", "#D9363E", 12d, 14d, 40d),
                shellStart: "#F7FBF5",
                shellEnd: "#F2F8EE",
                glowPrimary: "#D7EFCB",
                glowSecondary: "#EAE4C5",
                sidebarBackground: "#E6FAFBF2",
                sidebarBorder: "#B5D1A9",
                previewStart: "#FFFFFEFB",
                previewEnd: "#FFF8FBF4",
                previewBorder: "#C9D9BD",
                surfaceBackground: "#FFFFFFFF",
                surfaceBorder: "#C9D9BD"),
            new ThemePreset(
                name: "石墨风格",
                caption: "石墨灰与冷蓝点缀，适合企业级工作台。",
                description: "把品牌色收窄成局部点缀之后，视觉会更职业，更适合大屏工作台和复杂表单。",
                footerHint: "石墨风格适合配合更强的层级网格和更明确的分区结构。",
                modeText: "Dark",
                mode: AntdThemeMode.Dark,
                seed: CreateSeed("#7AA2FF", "#6BCB77", "#E8C547", "#FF6B6B", 10d, 14d, 40d),
                shellStart: "#111318",
                shellEnd: "#1A1F27",
                glowPrimary: "#263249",
                glowSecondary: "#202A3B",
                sidebarBackground: "#B3171C23",
                sidebarBorder: "#3F4A59",
                previewStart: "#191E26",
                previewEnd: "#13171D",
                previewBorder: "#404958",
                surfaceBackground: "#FF20252D",
                surfaceBorder: "#404958")
        ];
    }

    private static AntdSeedToken CreateSeed(string primary, string success, string warning, string error, double borderRadius, double fontSizeBase, double controlHeightMiddle)
    {
        return new AntdSeedToken
        {
            PrimaryColor = ParseColor(primary),
            SuccessColor = ParseColor(success),
            WarningColor = ParseColor(warning),
            ErrorColor = ParseColor(error),
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

    private static Color ParseColor(string value)
    {
        return (Color)ColorConverter.ConvertFromString(value)!;
    }

    private static string ToHexRgb(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    private sealed class ThemePreset(
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
        string surfaceBackground,
        string surfaceBorder)
    {
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

        public Color SurfaceBackground { get; } = ParseColor(surfaceBackground);

        public Color SurfaceBorder { get; } = ParseColor(surfaceBorder);
    }
}
