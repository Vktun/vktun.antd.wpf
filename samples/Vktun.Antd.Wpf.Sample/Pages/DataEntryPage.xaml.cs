using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vktun.Antd.Wpf;

namespace Vktun.Antd.Wpf.Sample.Pages;

public partial class DataEntryPage : UserControl
{
    public DataEntryPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public IList SelectedRegions { get; } = new ArrayList { "beijing", "shanghai" };

    public object SelectedChannel { get; set; } = "mail";

    public IDictionary<string, object?> FormModel { get; } = new Dictionary<string, object?>
    {
        ["name"] = "Antd WPF",
        ["environment"] = "Production",
    };

    private void ValidateFormButton_OnClick(object sender, RoutedEventArgs e)
    {
        var owner = Window.GetWindow(this);
        if (owner is null)
        {
            return;
        }

        var form = FindVisualDescendant<Form>(this);
        if (form is null)
        {
            return;
        }

        var isValid = form.Validate();
        AntdServices.Message.Show(
            owner,
            isValid ? "表单校验通过。" : "请检查必填项。",
            isValid ? MessageKind.Success : MessageKind.Warning);
    }

    private static T? FindVisualDescendant<T>(DependencyObject root)
        where T : DependencyObject
    {
        var count = VisualTreeHelper.GetChildrenCount(root);
        for (var index = 0; index < count; index++)
        {
            var child = VisualTreeHelper.GetChild(root, index);
            if (child is T typedChild)
            {
                return typedChild;
            }

            var nested = FindVisualDescendant<T>(child);
            if (nested is not null)
            {
                return nested;
            }
        }

        return null;
    }
}
