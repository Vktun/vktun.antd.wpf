using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Vktun.Antd.Wpf;

namespace Vktun.Antd.Wpf.Sample.Pages;

public partial class OverviewPage : UserControl
{
    public OverviewPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public IReadOnlyList<CoverageMatrixItem> CoverageItems { get; } =
    [
        new("通用", "Button", "antd:Button", "已实现", AntdTagColor.Success, "直接属性替代 ButtonAssist。"),
        new("通用", "FloatButton", "antd:FloatButton", "已实现", AntdTagColor.Success, "支持局部悬浮和全局 overlay。"),
        new("通用", "Typography", "antd:Typography*", "已实现", AntdTagColor.Success, "映射 Title / Text / Paragraph。"),
        new("布局", "Divider", "antd:Divider", "已实现", AntdTagColor.Success, "支持横向、纵向和带文本分割线。"),
        new("布局", "Grid", "antd:Row / antd:Col", "已实现", AntdTagColor.Success, "按 24 栅格组织内容。"),
        new("布局", "Space", "antd:Space", "已实现", AntdTagColor.Success, "替换内部模板中的 SpaceAssist。"),
        new("布局", "Flex", "antd:Flex", "本轮新增", AntdTagColor.Blue, "支持 Gap、Wrap、Justify、Align。"),
        new("布局", "Layout", "antd:Layout*", "本轮新增", AntdTagColor.Blue, "提供 Header / Sider / Content / Footer。"),
        new("布局", "Splitter", "antd:Splitter", "本轮新增", AntdTagColor.Blue, "基于 GridSplitter 的主题化封装。"),
        new("导航", "Breadcrumb", "antd:Breadcrumb", "已实现", AntdTagColor.Success, "保留层级导航场景。"),
        new("导航", "Dropdown", "antd:Dropdown", "已实现", AntdTagColor.Success, "支持 DropdownMenu 和 item click。"),
        new("导航", "DropdownButton", "antd:DropdownButton", "已实现", AntdTagColor.Success, "主按钮和菜单拆分。"),
        new("导航", "Menu", "antd:Menu / antd:Submenu", "已实现", AntdTagColor.Success, "支持 vertical / horizontal。"),
        new("导航", "Pagination", "antd:Pagination", "已实现", AntdTagColor.Success, "支持页码、page size 和 quick jump。"),
        new("导航", "Steps", "antd:Steps", "已实现", AntdTagColor.Success, "支持流程态和方向切换。"),
        new("导航", "Tabs", "antd:Tabs", "已实现", AntdTagColor.Success, "通过 TabPane 集合承载内容。"),
        new("数据录入", "Checkbox", "antd:Checkbox", "已实现", AntdTagColor.Success, "支持 indeterminate。"),
        new("数据录入", "CheckboxGroup", "antd:CheckboxGroup", "已实现", AntdTagColor.Success, "支持 Value 集合同步。"),
        new("数据录入", "Input", "antd:Input", "已实现", AntdTagColor.Success, "Prefix / Suffix / AllowClear 直接属性。"),
        new("数据录入", "PasswordInput", "antd:PasswordInput", "已实现", AntdTagColor.Success, "沿用输入框主题语义。"),
        new("数据录入", "Select", "antd:ComboBox", "已实现", AntdTagColor.Success, "sample 中明确映射为 Select。"),
        new("数据录入", "Form", "antd:Form / antd:FormItem", "已实现", AntdTagColor.Success, "保留表单布局与校验接口。"),
        new("数据录入", "Radio", "antd:Radio", "已实现", AntdTagColor.Success, "支持 button style。"),
        new("数据录入", "RadioGroup", "antd:RadioGroup", "已实现", AntdTagColor.Success, "Value 与选中状态同步。"),
        new("数据录入", "DatePicker", "antd:DatePicker", "已实现", AntdTagColor.Success, "兼容 SelectedDate / DisplayDate / IsDropDownOpen。"),
        new("数据录入", "RangePicker", "antd:RangePicker", "已实现", AntdTagColor.Success, "与自定义 DatePicker 一起展示。"),
        new("数据录入", "Switch", "antd:Switch", "已实现", AntdTagColor.Success, "支持 checked / unchecked 文案。"),
        new("数据录入", "InputNumber", "antd:InputNumber", "本轮新增", AntdTagColor.Blue, "支持步进、钳制、精度和前后缀。"),
        new("数据录入", "Slider", "antd:Slider", "本轮新增", AntdTagColor.Blue, "当前聚焦单值滑块。"),
        new("数据展示", "Avatar", "antd:Avatar", "已实现", AntdTagColor.Success, "支持 Text、Icon、ImageSource。"),
        new("数据展示", "Badge", "antd:Badge", "已实现", AntdTagColor.Success, "支持 count 和 dot。"),
        new("数据展示", "Card", "antd:Card", "已实现", AntdTagColor.Success, "保留 Header / Extra 结构。"),
        new("数据展示", "Collapse", "antd:Collapse", "已实现", AntdTagColor.Success, "支持 Accordion。"),
        new("数据展示", "Descriptions", "antd:Descriptions", "已实现", AntdTagColor.Success, "支持 bordered 和多列布局。"),
        new("数据展示", "Empty", "antd:Empty", "已实现", AntdTagColor.Success, "空状态占位。"),
        new("数据展示", "List", "antd:List", "已实现", AntdTagColor.Success, "支持 ListItem / Header / Footer。"),
        new("数据展示", "Popover", "antd:Popover", "已实现", AntdTagColor.Success, "映射富内容提示层。"),
        new("数据展示", "Table", "antd:Table", "已实现", AntdTagColor.Success, "基于 DataGrid 的主题化表格。"),
        new("数据展示", "Tag", "antd:Tag", "已实现", AntdTagColor.Success, "语义颜色迁移到 Tag.Color。"),
        new("数据展示", "Timeline", "antd:Timeline", "已实现", AntdTagColor.Success, "支持时间线颜色和标签。"),
        new("数据展示", "Tooltip", "antd:Tooltip", "已实现", AntdTagColor.Success, "悬浮提示与颜色映射。"),
        new("数据展示", "Calendar", "antd:Calendar", "本轮新增", AntdTagColor.Blue, "把主题资源中的日历样式独立暴露。"),
        new("数据展示", "Segmented", "antd:Segmented", "本轮新增", AntdTagColor.Blue, "单选分段组件。"),
        new("数据展示", "Statistic", "antd:Statistic", "本轮新增", AntdTagColor.Blue, "格式化数值与前后缀组合。"),
        new("反馈", "Alert", "antd:Alert", "已实现", AntdTagColor.Success, "支持 type / icon / closable。"),
        new("反馈", "Drawer", "antd:Drawer", "已实现", AntdTagColor.Success, "内嵌抽屉布局。"),
        new("反馈", "Popconfirm", "antd:Popconfirm", "已实现", AntdTagColor.Success, "确认事件与按钮风格。"),
        new("反馈", "Progress", "antd:Progress", "已实现", AntdTagColor.Success, "支持 line / circle。"),
        new("反馈", "Result", "antd:Result", "已实现", AntdTagColor.Success, "操作结果页。"),
        new("反馈", "Skeleton", "antd:Skeleton", "已实现", AntdTagColor.Success, "支持 avatar / paragraph。"),
        new("反馈", "Spin", "antd:Spin", "已实现", AntdTagColor.Success, "包裹内容 loading 态。"),
        new("反馈", "Message", "AntdServices.Message", "已实现", AntdTagColor.Success, "服务调用，不是声明式控件。"),
        new("反馈", "Modal", "AntdServices.Modal", "已实现", AntdTagColor.Success, "服务调用，不是声明式控件。"),
        new("反馈", "Notification", "AntdServices.Notification", "已实现", AntdTagColor.Success, "服务调用，不是声明式控件。"),
        new("反馈", "Watermark", "antd:Watermark", "本轮新增", AntdTagColor.Blue, "平铺文本水印。"),
        new("其他", "ConfigProvider", "AntdThemeResources + AntdThemeManager", "已实现", AntdTagColor.Success, "sample 中给出主题切换映射说明。"),
        new("候选", "Anchor", "后续候选", "后续候选", AntdTagColor.Warning, "适合目录锚点导航，未纳入本轮。"),
        new("候选", "AutoComplete", "后续候选", "后续候选", AntdTagColor.Warning, "可与 ComboBox 共用部分基础设施。"),
        new("候选", "TimePicker", "后续候选", "后续候选", AntdTagColor.Warning, "和 DatePicker 一起推进更稳妥。"),
        new("候选", "Tree", "后续候选", "后续候选", AntdTagColor.Warning, "适合单独规划数据结构和虚拟化。"),
        new("候选", "Upload", "后续候选", "后续候选", AntdTagColor.Warning, "涉及文件选择与进度链路。"),
        new("候选", "Tour", "后续候选", "后续候选", AntdTagColor.Warning, "依赖引导层和定位系统。"),
    ];

    public int ImplementedCount => CoverageItems.Count(item => item.Status == "已实现");

    public int AddedCount => CoverageItems.Count(item => item.Status == "本轮新增");

    public int CandidateCount => CoverageItems.Count(item => item.Status == "后续候选");

    public sealed record CoverageMatrixItem(
        string Category,
        string Component,
        string Mapping,
        string Status,
        AntdTagColor StatusColor,
        string Notes);
}
