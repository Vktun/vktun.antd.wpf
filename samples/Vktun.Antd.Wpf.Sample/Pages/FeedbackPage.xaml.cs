using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vktun.Antd.Wpf;

namespace Vktun.Antd.Wpf.Sample.Pages;

public partial class FeedbackPage : UserControl
{
    public FeedbackPage()
    {
        InitializeComponent();
    }

    private void OpenDrawerButton_OnClick(object sender, RoutedEventArgs e)
    {
        var drawer = FindVisualDescendant<Drawer>(this);
        if (drawer is not null)
        {
            drawer.IsOpen = true;
        }
    }

    private void CloseDrawerButton_OnClick(object sender, RoutedEventArgs e)
    {
        var drawer = FindVisualDescendant<Drawer>(this);
        if (drawer is not null)
        {
            drawer.IsOpen = false;
        }
    }

    private void Popconfirm_OnConfirm(object sender, RoutedEventArgs e)
    {
        ShowMessage("已确认删除。", MessageKind.Success);
    }

    private void Popconfirm_OnCancel(object sender, RoutedEventArgs e)
    {
        ShowMessage("已取消操作。", MessageKind.Info);
    }

    private void ShowMessageButton_OnClick(object sender, RoutedEventArgs e)
    {
        ShowMessage("保存成功", MessageKind.Success);
    }

    private void ShowNotificationButton_OnClick(object sender, RoutedEventArgs e)
    {
        var owner = Window.GetWindow(this);
        if (owner is null)
        {
            return;
        }

        AntdServices.Notification.Show(owner, new NotificationRequest
        {
            Title = "发布完成",
            Description = "通知、消息和弹窗映射为服务调用。",
            Kind = MessageKind.Info,
        });
    }

    private async void ShowModalButton_OnClick(object sender, RoutedEventArgs e)
    {
        await ShowModalAsync();
    }

    private async Task ShowModalAsync()
    {
        var owner = Window.GetWindow(this);
        if (owner is null)
        {
            return;
        }

        var result = await AntdServices.Modal.ShowAsync(owner, new ModalRequest
        {
            Title = "确认发布",
            Content = "是否继续执行发布流程？",
            OkText = "确认",
            CancelText = "取消",
        });

        ShowMessage(result == true ? "已确认发布。" : "已取消发布。", result == true ? MessageKind.Success : MessageKind.Info);
    }

    private void ShowMessage(string text, MessageKind kind)
    {
        var owner = Window.GetWindow(this);
        if (owner is null)
        {
            return;
        }

        AntdServices.Message.Show(owner, text, kind);
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
