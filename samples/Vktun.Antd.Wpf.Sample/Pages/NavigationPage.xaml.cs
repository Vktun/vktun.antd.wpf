using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Vktun.Antd.Wpf;

namespace Vktun.Antd.Wpf.Sample.Pages;

public partial class NavigationPage : UserControl
{
    public NavigationPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public ObservableCollection<TabPane> DemoTabs { get; } =
    [
        new TabPane
        {
            Key = "design",
            Header = "Design",
            Content = new TextBlock
            {
                Text = "按官方分类驱动 sample 结构，先保证映射清晰，再扩展视觉细节。",
                TextWrapping = TextWrapping.Wrap,
            },
        },
        new TabPane
        {
            Key = "code",
            Header = "Code",
            Content = new TextBlock
            {
                Text = "Tabs 当前通过 Items 集合接入 TabPane，更适合在代码里组织页面内容。",
                TextWrapping = TextWrapping.Wrap,
            },
        },
    ];
}
