using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using AntdAlert = Vktun.Antd.Avalonia.Alert;
using AntdButton = Vktun.Antd.Avalonia.Button;
using AntdCalendar = Vktun.Antd.Avalonia.Calendar;
using AntdComboBox = Vktun.Antd.Avalonia.ComboBox;
using AntdGrid = Vktun.Antd.Avalonia.Grid;
using AntdLayout = Vktun.Antd.Avalonia.Layout;
using AntdList = Vktun.Antd.Avalonia.List;
using AntdProgress = Vktun.Antd.Avalonia.Progress;
using AntdSlider = Vktun.Antd.Avalonia.Slider;
using AntdSwitch = Vktun.Antd.Avalonia.Switch;

namespace Vktun.Antd.Avalonia.Sample.Catalog;

/// <summary>
/// Describes whether a WPF sample entry has an equivalent Avalonia sample preview.
/// </summary>
public enum CatalogSupportStatus
{
    /// <summary>
    /// The entry is rendered with a compatible Avalonia control or a direct Avalonia equivalent.
    /// </summary>
    Compatible,

    /// <summary>
    /// The entry is rendered through Avalonia native controls or a native control composition.
    /// </summary>
    NativeAdaptation,

    /// <summary>
    /// The entry is shown as an explicit incompatibility marker because no direct Avalonia equivalent exists yet.
    /// </summary>
    Incompatible,
}

/// <summary>
/// Represents a left-side navigation entry in the Avalonia catalog sample.
/// </summary>
/// <param name="Key">Stable category key.</param>
/// <param name="Title">Display title.</param>
/// <param name="Caption">Short category caption.</param>
public sealed record CatalogNavigationItem(string Key, string Title, string Caption);

/// <summary>
/// Represents one WPF sample control mapped to an Avalonia catalog preview.
/// </summary>
/// <param name="CategoryKey">Owning category key.</param>
/// <param name="Name">Control name shown in the WPF sample.</param>
/// <param name="WpfMapping">Original WPF sample name or mapping note.</param>
/// <param name="Status">Avalonia support status.</param>
/// <param name="Notes">Compatibility notes shown in the card.</param>
/// <param name="PreviewFactory">Factory for compatible previews.</param>
public sealed record CatalogControlItem(
    string CategoryKey,
    string Name,
    string WpfMapping,
    CatalogSupportStatus Status,
    string Notes,
    Func<Control>? PreviewFactory);

/// <summary>
/// Builds the Avalonia sample catalog from the controls displayed by the WPF sample.
/// </summary>
public static class CatalogControlCatalog
{
    /// <summary>
    /// Gets the category navigation entries used by the sample shell.
    /// </summary>
    public static IReadOnlyList<CatalogNavigationItem> NavigationItems { get; } =
    [
        new("overview", "总览", "WPF sample 控件覆盖矩阵。"),
        new("general", "通用", "Button、Typography、FloatButton。"),
        new("layout", "布局", "Divider、Row、Col、Space、Flex、Layout、Splitter。"),
        new("navigation", "导航", "Breadcrumb、Dropdown、Menu、Pagination、Steps、Tabs。"),
        new("data-entry", "数据录入", "Input、Select、DatePicker、Switch 等。"),
        new("data-display", "数据展示", "Avatar、Card、Table、Timeline 等。"),
        new("feedback", "反馈", "Alert、Drawer、Progress、Modal 服务等。"),
        new("other", "其他", "主题资源和运行时切换入口。"),
    ];

    /// <summary>
    /// Gets every WPF sample control entry and its Avalonia compatibility status.
    /// </summary>
    public static IReadOnlyList<CatalogControlItem> Items { get; } = CreateItems();

    /// <summary>
    /// Creates a scrollable page for the requested category key.
    /// </summary>
    /// <param name="key">The navigation category key.</param>
    /// <returns>A scrollable Avalonia control containing category previews.</returns>
    public static Control CreatePage(string key)
    {
        return key == "overview"
            ? CreateOverviewPage()
            : key == "other"
                ? CreateOtherPage()
                : CreateCategoryPage(key);
    }

    private static IReadOnlyList<CatalogControlItem> CreateItems()
    {
        return
        [
            Compatible("general", "Button", "Button", "Avalonia 同名控件，保留 Type、Size、Status、Icon 语义。", ButtonPreview),
            Compatible("general", "FloatButton", "FloatButton", "Avalonia 同名控件，保留 Shape、Placement 语义。", FloatButtonPreview),
            Incompatible("general", "TypographyTitle", "Typography.Title", "WPF 的 Title 分级控件尚未拆分；Avalonia 目前仅提供 Typography 文本基类。"),
            Incompatible("general", "TypographyText", "Typography.Text", "WPF 的 Text 专用控件尚未拆分；Avalonia 目前仅提供 Typography 文本基类。"),
            Incompatible("general", "TypographyParagraph", "Typography.Paragraph", "WPF 的 Paragraph 专用控件尚未拆分；Avalonia 目前仅提供 Typography 文本基类。"),

            Compatible("layout", "Divider", "Divider", "Avalonia 同名控件，支持水平/垂直和文本位置。", DividerPreview),
            Incompatible("layout", "Row", "Row", "WPF 栅格行组件依赖 WPF 布局实现；Avalonia 侧暂以原生 Grid/Flex 组合替代。"),
            Incompatible("layout", "Col", "Col", "WPF 栅格列组件依赖 WPF 布局实现；Avalonia 侧暂以原生 Grid/Flex 组合替代。"),
            Compatible("layout", "Space", "Space", "Avalonia 同名控件，使用 StackPanel 能力承载间距布局。", SpacePreview),
            Compatible("layout", "Flex", "Flex", "Avalonia 同名控件，使用 StackPanel/Flex 语义展示子项。", FlexPreview),
            Compatible("layout", "Layout", "Layout", "Avalonia 同名容器可承载布局内容。", LayoutPreview),
            Incompatible("layout", "LayoutHeader", "Layout.Header", "WPF 的 Header 子控件暂未拆分为 Avalonia 独立类型。"),
            Incompatible("layout", "LayoutSider", "Layout.Sider", "WPF 的 Sider 子控件暂未拆分为 Avalonia 独立类型。"),
            Incompatible("layout", "LayoutContent", "Layout.Content", "WPF 的 Content 子控件暂未拆分为 Avalonia 独立类型。"),
            Incompatible("layout", "LayoutFooter", "Layout.Footer", "WPF 的 Footer 子控件暂未拆分为 Avalonia 独立类型。"),
            Compatible("layout", "Splitter", "Splitter", "Avalonia 同名控件已作为模板化占位控件提供。", SplitterPreview),

            Compatible("navigation", "Breadcrumb", "Breadcrumb", "Avalonia 同名 ItemsControl，使用 ItemsSource 展示路径。", BreadcrumbPreview),
            Incompatible("navigation", "BreadcrumbItem", "BreadcrumbItem", "WPF 的面包屑项类型暂未拆分；Avalonia 当前通过 Breadcrumb.ItemsSource 显示。"),
            Compatible("navigation", "Dropdown", "Dropdown", "Avalonia 同名控件，保留 Content 与 Overlay。", DropdownPreview),
            Incompatible("navigation", "DropdownMenu", "DropdownMenu", "WPF 下拉菜单容器暂未拆成 Avalonia 独立控件。"),
            Incompatible("navigation", "DropdownItem", "DropdownItem", "WPF 下拉菜单项暂未拆成 Avalonia 独立控件。"),
            Incompatible("navigation", "DropdownButton", "DropdownButton", "WPF 组合式 DropdownButton 暂无 Avalonia 同名控件，可先组合 Button + Dropdown。"),
            Compatible("navigation", "Menu", "Menu", "Avalonia 同名 ItemsControl，保留 Mode 与 Theme。", MenuPreview),
            Incompatible("navigation", "MenuItem", "MenuItem", "WPF 菜单项类型暂未拆分；Avalonia 当前通过 Menu.ItemsSource 显示。"),
            Incompatible("navigation", "Submenu", "Submenu", "WPF 子菜单类型暂未拆分；Avalonia 当前通过 Menu.ItemsSource 显示。"),
            Compatible("navigation", "Pagination", "Pagination", "Avalonia 同名控件，保留 Total、PageSize、CurrentPage。", PaginationPreview),
            Compatible("navigation", "Steps", "Steps", "Avalonia 同名 ItemsControl，支持 Direction。", StepsPreview),
            Incompatible("navigation", "Step", "Step", "WPF 步骤项类型暂未拆分；Avalonia 当前通过 Steps.ItemsSource 显示。"),
            Compatible("navigation", "Tabs", "Tabs", "Avalonia 同名控件，使用 TabPane 集合切换。", TabsPreview),

            Compatible("data-entry", "Checkbox", "Checkbox", "Avalonia 同名控件，保留 Status。", CheckboxPreview),
            Incompatible("data-entry", "CheckboxGroup", "CheckboxGroup", "WPF 组合控件暂未迁移；Avalonia 可先用多个 Checkbox 组合。"),
            Compatible("data-entry", "Input", "Input", "Avalonia 同名控件，保留 Prefix、Suffix、Status、Size、Variant。", InputPreview),
            Compatible("data-entry", "PasswordInput", "PasswordInput", "Avalonia 同名控件，沿用 Input 语义。", PasswordInputPreview),
            Compatible("data-entry", "ComboBox", "ComboBox", "Avalonia 同名控件，保留 Variant、Status、Size。", ComboBoxPreview),
            Compatible("data-entry", "Select", "Select", "WPF Select 在 Avalonia 侧映射为 ComboBox。", SelectPreview),
            Compatible("data-entry", "Form", "Form", "Avalonia 同名容器，保留 Layout。", FormPreview),
            Incompatible("data-entry", "FormItem", "FormItem", "WPF 表单项控件暂未迁移；Avalonia 当前用 Form + 文本/输入组合。"),
            Compatible("data-entry", "Radio", "Radio", "Avalonia 同名控件，保留 Status。", RadioPreview),
            Incompatible("data-entry", "RadioGroup", "RadioGroup", "WPF 单选组暂未迁移；Avalonia 可先用多个 Radio 组合。"),
            Compatible("data-entry", "DatePicker", "DatePicker", "Avalonia 同名控件，保留 Status 与 Variant。", DatePickerPreview),
            Incompatible("data-entry", "RangePicker", "RangePicker", "WPF 日期范围选择暂未迁移；Avalonia 目前没有同名控件。"),
            Compatible("data-entry", "Switch", "Switch", "Avalonia 同名控件，保留 Size。", SwitchPreview),
            Compatible("data-entry", "InputNumber", "InputNumber", "Avalonia 同名控件，保留 Minimum、Maximum、Step、Precision。", InputNumberPreview),
            Compatible("data-entry", "Slider", "Slider", "Avalonia 同名控件。", SliderPreview),

            Compatible("data-display", "Avatar", "Avatar", "Avalonia 同名控件，保留 Shape。", AvatarPreview),
            Compatible("data-display", "Badge", "Badge", "Avalonia 同名控件，保留 Count 与 Status。", BadgePreview),
            Compatible("data-display", "Card", "Card", "Avalonia 同名控件，保留 Title 与 Extra。", CardPreview),
            Compatible("data-display", "Collapse", "Collapse", "Avalonia 同名 ItemsControl。", CollapsePreview),
            Incompatible("data-display", "CollapsePanel", "CollapsePanel", "WPF 折叠面板项暂未拆分；Avalonia 当前通过 Collapse.ItemsSource 显示。"),
            Compatible("data-display", "Descriptions", "Descriptions", "Avalonia 同名 ItemsControl，保留 Layout。", DescriptionsPreview),
            Incompatible("data-display", "DescriptionItem", "DescriptionItem", "WPF 描述项暂未拆分；Avalonia 当前通过 Descriptions.ItemsSource 显示。"),
            Compatible("data-display", "Empty", "Empty", "Avalonia 同名控件，保留 Description。", EmptyPreview),
            Compatible("data-display", "List", "List", "Avalonia 同名 ItemsControl，保留 Layout。", ListPreview),
            Incompatible("data-display", "ListItem", "ListItem", "WPF 列表项暂未拆分；Avalonia 当前通过 List.ItemsSource 显示。"),
            Compatible("data-display", "Tooltip", "Tooltip", "Avalonia 同名控件，保留 Text 与 Content。", TooltipPreview),
            Incompatible("data-display", "Popover", "Popover", "WPF Popover 暂未迁移；Avalonia 当前只有 Tooltip 和 Popconfirm。"),
            Compatible("data-display", "Table", "Table", "Avalonia 同名 ItemsControl，保留 Size。", TablePreview),
            Compatible("data-display", "Tag", "Tag", "Avalonia 同名控件，保留 Color、Checkable、Closable。", TagPreview),
            Compatible("data-display", "Timeline", "Timeline", "Avalonia 同名 ItemsControl，保留 Mode。", TimelinePreview),
            Incompatible("data-display", "TimelineItem", "TimelineItem", "WPF 时间线项暂未拆分；Avalonia 当前通过 Timeline.ItemsSource 显示。"),
            Compatible("data-display", "Calendar", "Calendar", "Avalonia 同名控件，保留 SelectedDate。", CalendarPreview),
            Compatible("data-display", "Segmented", "Segmented", "Avalonia 同名 ListBox，保留 Block。", SegmentedPreview),
            Compatible("data-display", "Statistic", "Statistic", "Avalonia 同名控件，保留 Title、Value、ValueFormat。", StatisticPreview),

            Compatible("feedback", "Alert", "Alert", "Avalonia 同名控件，保留 Type、Message、Description、Closable。", AlertPreview),
            Compatible("feedback", "Drawer", "Drawer", "Avalonia 同名控件，保留 IsOpen 与 Placement。", DrawerPreview),
            Incompatible("feedback", "Message", "MessageService", "Avalonia 已有 MessageService，但没有 WPF sample 中同名可视控件入口；此处标注为服务型缺口。"),
            Incompatible("feedback", "Modal", "ModalService", "Avalonia 已有 ModalService，但没有 WPF sample 中同名可视控件入口；此处标注为服务型缺口。"),
            Incompatible("feedback", "Notification", "NotificationService", "Avalonia 已有 NotificationService，但没有 WPF sample 中同名可视控件入口；此处标注为服务型缺口。"),
            Compatible("feedback", "Popconfirm", "Popconfirm", "Avalonia 同名控件，保留 IsOpen、Title、Confirm、Cancel。", PopconfirmPreview),
            Compatible("feedback", "Progress", "Progress", "Avalonia 同名控件，保留 Value、Type、Status、ShowInfo。", ProgressPreview),
            Compatible("feedback", "Result", "Result", "Avalonia 同名控件，保留 Status 与 Title。", ResultPreview),
            Compatible("feedback", "Skeleton", "Skeleton", "Avalonia 同名控件，保留 Active。", SkeletonPreview),
            Compatible("feedback", "Spin", "Spin", "Avalonia 同名控件，保留 IsSpinning 与 Tip。", SpinPreview),
            Compatible("feedback", "Watermark", "Watermark", "Avalonia 同名控件，保留 Text 与 Content。", WatermarkPreview),
        ];
    }

    private static CatalogControlItem Compatible(
        string categoryKey,
        string name,
        string wpfMapping,
        string notes,
        Func<Control> previewFactory)
    {
        return new CatalogControlItem(categoryKey, name, wpfMapping, CatalogSupportStatus.Compatible, notes, previewFactory);
    }

    private static CatalogControlItem Incompatible(string categoryKey, string name, string wpfMapping, string notes)
    {
        return new CatalogControlItem(
            categoryKey,
            name,
            wpfMapping,
            CatalogSupportStatus.NativeAdaptation,
            $"{notes} 已先使用 Avalonia 原生控件或原生组合进行展示适配。",
            () => CreateNativeAdaptationPreview(name));
    }

    private static Control CreateOverviewPage()
    {
        var total = Items.Count;
        var compatible = Items.Count(static item => item.Status == CatalogSupportStatus.Compatible);
        var nativeAdapted = Items.Count(static item => item.Status == CatalogSupportStatus.NativeAdaptation);
        var incompatible = Items.Count(static item => item.Status == CatalogSupportStatus.Incompatible);

        var stack = CreatePageStack(
            "总览",
            $"按 WPF sample 出现的控件逐个列出：{total} 项，兼容 {compatible} 项，原生适配 {nativeAdapted} 项，不兼容 {incompatible} 项。");

        foreach (var navigationItem in NavigationItems.Where(static item => item.Key is not "overview" and not "other"))
        {
            var categoryItems = Items.Where(item => item.CategoryKey == navigationItem.Key).ToArray();
            stack.Children.Add(CreateMatrixCard(
                navigationItem.Title,
                string.Join("、", categoryItems.Select(static item => $"{item.Name}({StatusText(item.Status)})"))));
        }

        return CreateScroll(stack);
    }

    private static Control CreateOtherPage()
    {
        var stack = CreatePageStack("其他", "Avalonia sample 主题入口与 WPF sample 的 ConfigProvider/主题资源说明。");

        stack.Children.Add(CreateMatrixCard("AntdThemeResources", "<antd:AntdThemeResources Theme=\"Light\" />"));
        stack.Children.Add(CreateMatrixCard("AntdThemeManager", "AntdThemeManager.Current.Apply(Application.Current, mode, seed)"));
        stack.Children.Add(CreateMatrixCard("SeedToken", "PrimaryColor、SuccessColor、WarningColor、ErrorColor、BorderRadius"));

        return CreateScroll(stack);
    }

    private static Control CreateCategoryPage(string key)
    {
        var navigationItem = NavigationItems.FirstOrDefault(item => item.Key == key)
            ?? NavigationItems.First(static item => item.Key == "overview");

        var stack = CreatePageStack(navigationItem.Title, navigationItem.Caption);
        var wrap = new WrapPanel
        {
            ItemWidth = 320,
            Orientation = Orientation.Horizontal,
        };

        foreach (var item in Items.Where(item => item.CategoryKey == key))
        {
            wrap.Children.Add(CreateControlCard(item));
        }

        stack.Children.Add(wrap);

        return CreateScroll(stack);
    }

    private static StackPanel CreatePageStack(string title, string description)
    {
        return new StackPanel
        {
            Spacing = 16,
            Children =
            {
                new TextBlock
                {
                    Text = title,
                    FontSize = 28,
                    FontWeight = FontWeight.SemiBold,
                    Foreground = TextBrush(),
                },
                new TextBlock
                {
                    Text = description,
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = SecondaryTextBrush(),
                },
            },
        };
    }

    private static ScrollViewer CreateScroll(Control content)
    {
        return new ScrollViewer
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            Content = content,
        };
    }

    private static Border CreateMatrixCard(string title, string body)
    {
        return new Border
        {
            Padding = new Thickness(16),
            CornerRadius = new CornerRadius(10),
            Background = SurfaceBrush(),
            BorderBrush = BorderBrush(),
            BorderThickness = new Thickness(1),
            Child = new StackPanel
            {
                Spacing = 8,
                Children =
                {
                    new TextBlock
                    {
                        Text = title,
                        FontWeight = FontWeight.SemiBold,
                        Foreground = TextBrush(),
                    },
                    new TextBlock
                    {
                        Text = body,
                        TextWrapping = TextWrapping.Wrap,
                        Foreground = SecondaryTextBrush(),
                    },
                },
            },
        };
    }

    private static Border CreateControlCard(CatalogControlItem item)
    {
        var preview = item.PreviewFactory is not null
            ? item.PreviewFactory()
            : CreateIncompatiblePreview(item);

        return new Border
        {
            MinHeight = 184,
            Margin = new Thickness(0, 0, 14, 14),
            Padding = new Thickness(16),
            CornerRadius = new CornerRadius(10),
            Background = SurfaceBrush(),
            BorderBrush = BorderBrush(),
            BorderThickness = new Thickness(1),
            Child = new StackPanel
            {
                Spacing = 12,
                Children =
                {
                    CreateCardHeader(item),
                    new TextBlock
                    {
                        Text = item.Notes,
                        TextWrapping = TextWrapping.Wrap,
                        Foreground = SecondaryTextBrush(),
                    },
                    new Border
                    {
                        MinHeight = 72,
                        Padding = new Thickness(12),
                        CornerRadius = new CornerRadius(8),
                        Background = PreviewBrush(),
                        Child = preview,
                    },
                },
            },
        };
    }

    private static Grid CreateCardHeader(CatalogControlItem item)
    {
        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,Auto"),
        };

        grid.Children.Add(new StackPanel
        {
            Spacing = 2,
            Children =
            {
                new TextBlock
                {
                    Text = item.Name,
                    FontSize = 18,
                    FontWeight = FontWeight.SemiBold,
                    Foreground = TextBrush(),
                },
                new TextBlock
                {
                    Text = $"WPF: {item.WpfMapping}",
                    FontSize = 12,
                    Foreground = MutedTextBrush(),
                },
            },
        });

        var statusTag = new Tag
        {
            Content = StatusText(item.Status),
            Color = item.Status switch
            {
                CatalogSupportStatus.Compatible => AntdTagColor.Success,
                CatalogSupportStatus.NativeAdaptation => AntdTagColor.Blue,
                _ => AntdTagColor.Warning,
            },
        };
        Grid.SetColumn(statusTag, 1);
        grid.Children.Add(statusTag);

        return grid;
    }

    private static Control CreateIncompatiblePreview(CatalogControlItem item)
    {
        return new StackPanel
        {
            Spacing = 8,
            Children =
            {
                new Tag
                {
                    Content = "不兼容",
                    Color = AntdTagColor.Warning,
                },
                new TextBlock
                {
                    Text = item.Notes,
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = SecondaryTextBrush(),
                },
            },
        };
    }

    private static string StatusText(CatalogSupportStatus status)
    {
        return status switch
        {
            CatalogSupportStatus.Compatible => "兼容",
            CatalogSupportStatus.NativeAdaptation => "原生适配",
            _ => "不兼容",
        };
    }

    private static Control CreateNativeAdaptationPreview(string name)
    {
        return name switch
        {
            "TypographyTitle" => new TextBlock
            {
                Text = "Native TextBlock Title",
                FontSize = 24,
                FontWeight = FontWeight.SemiBold,
                Foreground = TextBrush(),
            },
            "TypographyText" => new TextBlock
            {
                Text = "Native TextBlock text",
                Foreground = TextBrush(),
            },
            "TypographyParagraph" => new TextBlock
            {
                Text = "Native TextBlock paragraph wraps across multiple lines to match paragraph usage.",
                TextWrapping = TextWrapping.Wrap,
                Foreground = SecondaryTextBrush(),
            },
            "Row" => NativeGridPreview(),
            "Col" => NativeGridPreview(),
            "LayoutHeader" => NativeLayoutRegionPreview("Header", "#1677FF"),
            "LayoutSider" => NativeLayoutRegionPreview("Sider", "#0958D9"),
            "LayoutContent" => NativeLayoutRegionPreview("Content", "#4096FF"),
            "LayoutFooter" => NativeLayoutRegionPreview("Footer", "#69B1FF"),
            "BreadcrumbItem" => NativeInlineText("Home", "/", "Components", "/", name),
            "DropdownMenu" => NativeMenuPreview(),
            "DropdownItem" => NativeMenuPreview(),
            "DropdownButton" => NativeDropdownButtonPreview(),
            "MenuItem" => NativeMenuPreview(),
            "Submenu" => NativeMenuPreview(),
            "Step" => NativeInlineText("1. Start", "2. Review", "3. Ship"),
            "CheckboxGroup" => NativeInline(
                new global::Avalonia.Controls.CheckBox { Content = "A", IsChecked = true },
                new global::Avalonia.Controls.CheckBox { Content = "B" },
                new global::Avalonia.Controls.CheckBox { Content = "C" }),
            "FormItem" => NativeFormItemPreview(),
            "RadioGroup" => NativeInline(
                new global::Avalonia.Controls.RadioButton { Content = "Daily", GroupName = "native-radio", IsChecked = true },
                new global::Avalonia.Controls.RadioButton { Content = "Weekly", GroupName = "native-radio" }),
            "RangePicker" => NativeInline(
                new global::Avalonia.Controls.DatePicker { SelectedDate = DateTimeOffset.Now.Date },
                new TextBlock { Text = "to", VerticalAlignment = VerticalAlignment.Center, Foreground = SecondaryTextBrush() },
                new global::Avalonia.Controls.DatePicker { SelectedDate = DateTimeOffset.Now.Date.AddDays(7) }),
            "CollapsePanel" => new global::Avalonia.Controls.Expander
            {
                Header = "Native Expander",
                IsExpanded = true,
                Content = "Panel content",
            },
            "DescriptionItem" => NativeDescriptionItemPreview(),
            "ListItem" => new global::Avalonia.Controls.ListBox
            {
                ItemsSource = new[]
                {
                    new global::Avalonia.Controls.ListBoxItem { Content = "Native list item A" },
                    new global::Avalonia.Controls.ListBoxItem { Content = "Native list item B" },
                },
            },
            "Popover" => NativePopoverPreview(),
            "TimelineItem" => NativeTimelineItemPreview(),
            "Message" => NativeNoticePreview("Message", "Saved successfully"),
            "Modal" => NativeModalPreview(),
            "Notification" => NativeNoticePreview("Notification", "Build completed"),
            _ => NativeNoticePreview(name, "Native Avalonia preview"),
        };
    }

    private static Control NativeGridPreview()
    {
        var grid = new global::Avalonia.Controls.Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,*"),
            RowDefinitions = new RowDefinitions("Auto,Auto"),
        };

        AddGridCell(grid, "col-12", 0, 0);
        AddGridCell(grid, "col-12", 1, 0);
        AddGridCell(grid, "row", 0, 1);
        global::Avalonia.Controls.Grid.SetColumnSpan(grid.Children[^1], 2);

        return grid;
    }

    private static void AddGridCell(global::Avalonia.Controls.Grid grid, string text, int column, int row)
    {
        var border = NativeBlock(text);
        border.Margin = new Thickness(0, 0, 8, 8);
        global::Avalonia.Controls.Grid.SetColumn(border, column);
        global::Avalonia.Controls.Grid.SetRow(border, row);
        grid.Children.Add(border);
    }

    private static Control NativeLayoutRegionPreview(string text, string color)
    {
        return new Border
        {
            Padding = new Thickness(12),
            CornerRadius = new CornerRadius(8),
            Background = new SolidColorBrush(Color.Parse(color)),
            Child = new TextBlock
            {
                Text = text,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
            },
        };
    }

    private static Control NativeMenuPreview()
    {
        return new global::Avalonia.Controls.Menu
        {
            ItemsSource = new object[]
            {
                new global::Avalonia.Controls.MenuItem { Header = "Overview" },
                new global::Avalonia.Controls.MenuItem
                {
                    Header = "Components",
                    ItemsSource = new object[]
                    {
                        new global::Avalonia.Controls.MenuItem { Header = "Button" },
                        new global::Avalonia.Controls.MenuItem { Header = "Table" },
                    },
                },
            },
        };
    }

    private static Control NativeDropdownButtonPreview()
    {
        return new StackPanel
        {
            Spacing = 8,
            Children =
            {
                new global::Avalonia.Controls.Button { Content = "Native dropdown button" },
                NativeInlineText("Menu item A", "Menu item B"),
            },
        };
    }

    private static Control NativeFormItemPreview()
    {
        var grid = new global::Avalonia.Controls.Grid
        {
            ColumnDefinitions = new ColumnDefinitions("80,*"),
        };

        var label = new TextBlock
        {
            Text = "Name",
            VerticalAlignment = VerticalAlignment.Center,
            Foreground = SecondaryTextBrush(),
        };
        var input = new global::Avalonia.Controls.TextBox
        {
            Watermark = "Native TextBox",
            Text = "Vktun",
        };
        global::Avalonia.Controls.Grid.SetColumn(input, 1);

        grid.Children.Add(label);
        grid.Children.Add(input);

        return grid;
    }

    private static Control NativeDescriptionItemPreview()
    {
        var grid = new global::Avalonia.Controls.Grid
        {
            ColumnDefinitions = new ColumnDefinitions("90,*"),
        };

        var label = new TextBlock
        {
            Text = "Platform",
            FontWeight = FontWeight.SemiBold,
            Foreground = TextBrush(),
        };
        var value = new TextBlock
        {
            Text = "Avalonia native",
            Foreground = SecondaryTextBrush(),
        };
        global::Avalonia.Controls.Grid.SetColumn(value, 1);
        grid.Children.Add(label);
        grid.Children.Add(value);

        return grid;
    }

    private static Control NativePopoverPreview()
    {
        return new Border
        {
            Padding = new Thickness(12),
            CornerRadius = new CornerRadius(8),
            Background = SurfaceBrush(),
            BorderBrush = BorderBrush(),
            BorderThickness = new Thickness(1),
            Child = new StackPanel
            {
                Spacing = 6,
                Children =
                {
                    new TextBlock { Text = "Native popover", FontWeight = FontWeight.SemiBold, Foreground = TextBrush() },
                    new TextBlock { Text = "Composed with Border + StackPanel.", Foreground = SecondaryTextBrush() },
                },
            },
        };
    }

    private static Control NativeTimelineItemPreview()
    {
        var grid = new global::Avalonia.Controls.Grid
        {
            ColumnDefinitions = new ColumnDefinitions("24,*"),
        };

        var marker = new Border
        {
            Width = 10,
            Height = 10,
            CornerRadius = new CornerRadius(5),
            Background = BrushFor(AntdResourceKeys.BrushPrimary, "#1677FF"),
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 4, 0, 0),
        };
        var text = new TextBlock
        {
            Text = "Created - native timeline item",
            Foreground = TextBrush(),
        };
        global::Avalonia.Controls.Grid.SetColumn(text, 1);
        grid.Children.Add(marker);
        grid.Children.Add(text);

        return grid;
    }

    private static Control NativeModalPreview()
    {
        return new Border
        {
            Width = 220,
            Padding = new Thickness(14),
            CornerRadius = new CornerRadius(8),
            Background = SurfaceBrush(),
            BorderBrush = BorderBrush(),
            BorderThickness = new Thickness(1),
            Child = new StackPanel
            {
                Spacing = 10,
                Children =
                {
                    new TextBlock { Text = "Native modal surface", FontWeight = FontWeight.SemiBold, Foreground = TextBrush() },
                    new TextBlock { Text = "Confirm this action?", TextWrapping = TextWrapping.Wrap, Foreground = SecondaryTextBrush() },
                    NativeInline(
                        new global::Avalonia.Controls.Button { Content = "Cancel" },
                        new global::Avalonia.Controls.Button { Content = "OK" }),
                },
            },
        };
    }

    private static Control NativeNoticePreview(string title, string body)
    {
        return new Border
        {
            Padding = new Thickness(12),
            CornerRadius = new CornerRadius(8),
            Background = SurfaceBrush(),
            BorderBrush = BorderBrush(),
            BorderThickness = new Thickness(1),
            Child = new StackPanel
            {
                Spacing = 4,
                Children =
                {
                    new TextBlock { Text = title, FontWeight = FontWeight.SemiBold, Foreground = TextBrush() },
                    new TextBlock { Text = body, TextWrapping = TextWrapping.Wrap, Foreground = SecondaryTextBrush() },
                },
            },
        };
    }

    private static Control NativeInlineText(params string[] texts)
    {
        return NativeInline(texts.Select(static text => new TextBlock
        {
            Text = text,
            Margin = new Thickness(0, 0, 8, 8),
        }).ToArray());
    }

    private static Control NativeInline(params Control[] controls)
    {
        var wrap = new WrapPanel
        {
            Orientation = Orientation.Horizontal,
        };

        foreach (var control in controls)
        {
            control.Margin = new Thickness(0, 0, 8, 8);
            wrap.Children.Add(control);
        }

        return wrap;
    }

    private static Border NativeBlock(string text)
    {
        return new Border
        {
            Padding = new Thickness(10),
            CornerRadius = new CornerRadius(8),
            Background = PreviewBrush(),
            BorderBrush = BorderBrush(),
            BorderThickness = new Thickness(1),
            Child = new TextBlock
            {
                Text = text,
                Foreground = TextBrush(),
            },
        };
    }

    private static Control ButtonPreview()
    {
        return Inline(
            new AntdButton { Content = "Default" },
            new AntdButton { Content = "Primary", Type = AntdButtonType.Primary },
            new AntdButton { Content = "Dashed", Type = AntdButtonType.Dashed });
    }

    private static Control FloatButtonPreview()
    {
        return Inline(
            new FloatButton { Content = "+", Shape = AntdFloatButtonShape.Circle },
            new FloatButton { Content = "?", Shape = AntdFloatButtonShape.Square });
    }

    private static Control DividerPreview()
    {
        return new Divider { Content = "Divider" };
    }

    private static Control SpacePreview()
    {
        return new Space
        {
            Orientation = Orientation.Horizontal,
            Spacing = 8,
            Children =
            {
                new Tag { Content = "A" },
                new Tag { Content = "B", Color = AntdTagColor.Blue },
                new Tag { Content = "C", Color = AntdTagColor.Green },
            },
        };
    }

    private static Control FlexPreview()
    {
        return new Flex
        {
            Orientation = Orientation.Horizontal,
            Spacing = 8,
            Children =
            {
                new AntdButton { Content = "Left" },
                new AntdButton { Content = "Right", Type = AntdButtonType.Primary },
            },
        };
    }

    private static Control LayoutPreview()
    {
        return new AntdLayout
        {
            Content = new StackPanel
            {
                Spacing = 8,
                Children =
                {
                    new Tag { Content = "Header", Color = AntdTagColor.Blue },
                    new TextBlock { Text = "Content", Foreground = TextBrush() },
                    new Tag { Content = "Footer", Color = AntdTagColor.Green },
                },
            },
        };
    }

    private static Control SplitterPreview()
    {
        return new StackPanel
        {
            Spacing = 8,
            Children =
            {
                new Splitter(),
                new TextBlock
                {
                    Text = "Splitter 模板占位",
                    Foreground = SecondaryTextBrush(),
                },
            },
        };
    }

    private static Control BreadcrumbPreview()
    {
        return new Breadcrumb { ItemsSource = new[] { "Home", "Components", "Tabs" } };
    }

    private static Control DropdownPreview()
    {
        return new Dropdown { Content = "Dropdown trigger", Overlay = "Overlay content" };
    }

    private static Control MenuPreview()
    {
        return new Menu { ItemsSource = new[] { "Overview", "General", "Feedback" } };
    }

    private static Control PaginationPreview()
    {
        return new Pagination { Total = 320, PageSize = 10, CurrentPage = 3 };
    }

    private static Control StepsPreview()
    {
        return new Steps { ItemsSource = new[] { "Start", "Review", "Ship" } };
    }

    private static Control TabsPreview()
    {
        return new Tabs
        {
            Items = new List<TabPane>
            {
                new() { Key = "one", Header = "One", Content = "First tab" },
                new() { Key = "two", Header = "Two", Content = "Second tab" },
            },
        };
    }

    private static Control CheckboxPreview()
    {
        return Inline(
            new Checkbox { Content = "Checked", IsChecked = true },
            new Checkbox { Content = "Warning", Status = AntdStatus.Warning });
    }

    private static Control InputPreview()
    {
        return new Input { Watermark = "Input", Prefix = "@", Suffix = ".com", Text = "vktun" };
    }

    private static Control PasswordInputPreview()
    {
        return new PasswordInput { Watermark = "Password", Text = "secret" };
    }

    private static Control InputNumberPreview()
    {
        return new InputNumber { Minimum = 0, Maximum = 10, Value = 6, Precision = 0 };
    }

    private static Control ComboBoxPreview()
    {
        return new AntdComboBox
        {
            ItemsSource = new[] { "Ocean", "Aurora", "Nebula" },
            SelectedIndex = 0,
        };
    }

    private static Control SelectPreview()
    {
        return new AntdComboBox
        {
            ItemsSource = new[] { "Select A", "Select B", "Select C" },
            SelectedIndex = 1,
        };
    }

    private static Control DatePickerPreview()
    {
        return new DatePicker { SelectedDate = DateTimeOffset.Now };
    }

    private static Control RadioPreview()
    {
        return Inline(
            new Radio { Content = "A", GroupName = "catalog-radio", IsChecked = true },
            new Radio { Content = "B", GroupName = "catalog-radio" });
    }

    private static Control SwitchPreview()
    {
        return new AntdSwitch { IsChecked = true };
    }

    private static Control SliderPreview()
    {
        return new AntdSlider { Minimum = 0, Maximum = 100, Value = 64 };
    }

    private static Control FormPreview()
    {
        return new Form
        {
            Spacing = 8,
            Children =
            {
                new TextBlock { Text = "Name", Foreground = SecondaryTextBrush() },
                new Input { Watermark = "Form input" },
            },
        };
    }

    private static Control AvatarPreview()
    {
        return Inline(
            new Avatar { Content = "A" },
            new Avatar { Content = "B", Shape = AntdAvatarShape.Square });
    }

    private static Control BadgePreview()
    {
        return new Badge { Count = 8, Content = "Inbox" };
    }

    private static Control CardPreview()
    {
        return new Card { Title = "Card", Content = "Card content" };
    }

    private static Control CollapsePreview()
    {
        return new Collapse { ItemsSource = new[] { "Panel 1", "Panel 2" } };
    }

    private static Control DescriptionsPreview()
    {
        return new Descriptions { ItemsSource = new[] { "Name: Vktun", "Platform: Avalonia" } };
    }

    private static Control EmptyPreview()
    {
        return new Empty { Description = "No data" };
    }

    private static Control ListPreview()
    {
        return new AntdList { ItemsSource = new[] { "List item A", "List item B" } };
    }

    private static Control TooltipPreview()
    {
        return new Tooltip { Text = "Tooltip", Content = "Hover target" };
    }

    private static Control TablePreview()
    {
        return new Table { ItemsSource = new[] { "Row 1", "Row 2" } };
    }

    private static Control TagPreview()
    {
        return Inline(
            new Tag { Content = "Success", Color = AntdTagColor.Success },
            new Tag { Content = "Closable", IsClosable = true });
    }

    private static Control TimelinePreview()
    {
        return new Timeline { ItemsSource = new[] { "Created", "Reviewed", "Published" } };
    }

    private static Control CalendarPreview()
    {
        return new AntdCalendar { SelectedDate = DateTimeOffset.Now, Content = DateTimeOffset.Now.ToString("yyyy-MM-dd") };
    }

    private static Control SegmentedPreview()
    {
        return new Segmented
        {
            ItemsSource = new[] { "Daily", "Weekly", "Monthly" },
            SelectedIndex = 0,
        };
    }

    private static Control StatisticPreview()
    {
        return new Statistic { Title = "Conversion", Value = 0.2734, ValueFormat = "{0:P1}" };
    }

    private static Control AlertPreview()
    {
        return new AntdAlert
        {
            Type = AntdAlertType.Info,
            Message = "Info alert",
            Description = "Avalonia alert surface",
        };
    }

    private static Control DrawerPreview()
    {
        return new Drawer { IsOpen = true, Content = "Drawer content" };
    }

    private static Control PopconfirmPreview()
    {
        return new Popconfirm { IsOpen = true, Title = "Confirm?", Content = "Delete" };
    }

    private static Control ProgressPreview()
    {
        return new AntdProgress { Value = 72 };
    }

    private static Control ResultPreview()
    {
        return new Result { Status = AntdResultStatus.Success, Title = "Success" };
    }

    private static Control SkeletonPreview()
    {
        return new Skeleton { Active = true };
    }

    private static Control SpinPreview()
    {
        return new Spin { Tip = "Loading" };
    }

    private static Control WatermarkPreview()
    {
        return new Watermark { Text = "Vktun", Content = "Watermark content" };
    }

    private static Control Inline(params Control[] controls)
    {
        var wrap = new WrapPanel
        {
            Orientation = Orientation.Horizontal,
        };

        foreach (var control in controls)
        {
            control.Margin = new Thickness(0, 0, 8, 8);
            wrap.Children.Add(control);
        }

        return wrap;
    }

    private static IBrush SurfaceBrush()
    {
        return BrushFor(AntdResourceKeys.BrushBgContainer, "#FFFFFF");
    }

    private static IBrush PreviewBrush()
    {
        return BrushFor(AntdResourceKeys.BrushBgLayout, "#F5F7FA");
    }

    private static IBrush BorderBrush()
    {
        return BrushFor(AntdResourceKeys.BrushBorderSecondary, "#D9D9D9");
    }

    private static IBrush TextBrush()
    {
        return BrushFor(AntdResourceKeys.BrushText, "#1F1F1F");
    }

    private static IBrush SecondaryTextBrush()
    {
        return BrushFor(AntdResourceKeys.BrushTextSecondary, "#595959");
    }

    private static IBrush MutedTextBrush()
    {
        return BrushFor(AntdResourceKeys.BrushTextTertiary, "#8C8C8C");
    }

    private static IBrush BrushFor(string key, string fallback)
    {
        if (Application.Current?.TryGetResource(key, null, out var resource) == true &&
            resource is IBrush brush)
        {
            return brush;
        }

        return new SolidColorBrush(Color.Parse(fallback));
    }
}
