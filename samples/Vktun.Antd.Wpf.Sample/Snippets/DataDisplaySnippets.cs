namespace Vktun.Antd.Wpf.Sample.Snippets;

public static class DataDisplaySnippets
{
    public static string Avatar => """
<antd:Space Orientation="Horizontal" Gap="12">
    <antd:Avatar Text="VK" />
    <antd:Avatar Shape="Square" Text="WPF" />
</antd:Space>
""";

    public static string Badge => """
<antd:Badge Count="7">
    <antd:Avatar Text="CI" />
</antd:Badge>
""";

    public static string Card => """
<antd:Card Header="发布摘要" Extra="更多">
    <StackPanel>
        <TextBlock Text="v1.0.0 已发布" />
        <TextBlock Text="包含 Layout / Segmented / Watermark。" />
    </StackPanel>
</antd:Card>
""";

    public static string Collapse => """
<antd:Collapse Accordion="True">
    <antd:CollapsePanel Header="设计规范" IsExpanded="True">
        <TextBlock Text="统一间距、圆角和色板语义。" />
    </antd:CollapsePanel>
    <antd:CollapsePanel Header="兼容约束">
        <TextBlock Text="以 WPF 语义兼容为优先。" />
    </antd:CollapsePanel>
</antd:Collapse>
""";

    public static string Descriptions => """
<antd:Descriptions Title="构建信息" Column="2" Bordered="True">
    <antd:DescriptionItem Label="Version" Content="1.0.0" />
    <antd:DescriptionItem Label="Theme" Content="Dark" />
    <antd:DescriptionItem Label="Commit" Content="a1b2c3d" Span="2" />
</antd:Descriptions>
""";

    public static string Empty => """
<antd:Empty Description="当前筛选条件下暂无数据" />
""";

    public static string List => """
<antd:List Header="最近变更" Bordered="True">
    <antd:ListItem Content="新增 Segmented 组件" Extra="今天" />
    <antd:ListItem Content="重构 Sample 目录页" Extra="昨天" />
</antd:List>
""";

    public static string TooltipPopover => """
<antd:Space Orientation="Horizontal" Gap="12">
    <antd:Tooltip Title="轻量提示">
        <antd:Button Content="Tooltip" />
    </antd:Tooltip>
    <antd:Popover Title="版本说明"
                   PopoverContent="包含 Calendar、Statistic 与 Watermark。">
        <antd:Button Content="Popover" />
    </antd:Popover>
</antd:Space>
""";

    public static string Table => """
<antd:Table ItemsSource="{Binding Releases}" TableSize="Middle" Bordered="True">
    <antd:Table.Columns>
        <DataGridTextColumn Header="版本" Binding="{Binding Version}" />
        <DataGridTextColumn Header="状态" Binding="{Binding Status}" />
        <DataGridTextColumn Header="日期" Binding="{Binding Date}" />
    </antd:Table.Columns>
</antd:Table>
""";

    public static string Tag => """
<antd:Space Orientation="Horizontal" Gap="8">
    <antd:Tag Color="Blue">Design</antd:Tag>
    <antd:Tag Color="Success">Stable</antd:Tag>
    <antd:Tag IsCheckable="True" IsChecked="True">Selected</antd:Tag>
</antd:Space>
""";

    public static string Timeline => """
<antd:Timeline>
    <antd:TimelineItem Content="定义映射策略" Label="阶段 1" Color="Blue" />
    <antd:TimelineItem Content="补齐 sample" Label="阶段 2" Color="Green" />
    <antd:TimelineItem Content="回归测试" Label="阶段 3" Color="Gray" />
</antd:Timeline>
""";

    public static string Calendar => """
<antd:Calendar DisplayDate="2026-04-08"
               SelectedDate="2026-04-08" />
""";

    public static string Segmented => """
<antd:Segmented SelectedIndex="1" Block="True">
    <ListBoxItem Content="日报" />
    <ListBoxItem Content="周报" />
    <ListBoxItem Content="月报" />
</antd:Segmented>
""";

    public static string Statistic => """
<antd:Statistic Title="本月发布次数"
                Value="42"
                Prefix="+"
                Suffix="次"
                Precision="0" />
""";
}
