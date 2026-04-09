namespace Vktun.Antd.Wpf.Sample.Snippets;

public static class FeedbackSnippets
{
    public static string Alert => """
<antd:Alert Type="Success"
            Message="构建完成"
            Description="所有页面和服务示例均已加载。"
            ShowIcon="True" />
""";

    public static string Drawer => """
<antd:Drawer Title="发布说明"
             Width="360"
             Placement="Right"
             IsOpen="{Binding IsDrawerOpen}">
    <TextBlock Text="Drawer 适合承载补充操作和配置表单。" />
</antd:Drawer>
""";

    public static string Popconfirm => """
<antd:Popconfirm Title="确认删除这条记录？"
                 OkText="删除"
                 CancelText="取消">
    <antd:Button Content="Delete" Status="Error" />
</antd:Popconfirm>
""";

    public static string Progress => """
<StackPanel>
    <antd:Progress Percent="68" />
    <antd:Progress Percent="100" Status="Success" />
</StackPanel>
""";

    public static string Result => """
<antd:Result Status="Success"
             Title="目录页已重构"
             SubTitle="组件分类、代码片段和主题切换已经打通。" />
""";

    public static string SkeletonSpin => """
<StackPanel>
    <antd:Skeleton Avatar="True" ParagraphRows="3" />
    <antd:Spin Tip="正在加载目录数据..." />
</StackPanel>
""";

    public static string ServiceFeedback => """
var owner = Window.GetWindow(this)!;

AntdServices.Message.Show(owner, "保存成功", MessageKind.Success);
AntdServices.Notification.Show(owner, new NotificationRequest
{
    Title = "发布完成",
    Description = "通知、消息和弹窗映射为服务调用。",
});

await AntdServices.Modal.ShowAsync(owner, new ModalRequest
{
    Title = "确认发布",
    Content = "是否继续执行发布流程？",
});
""";

    public static string Watermark => """
<antd:Watermark Text="Ant Design WPF"
                FontSize="18"
                GapX="180"
                GapY="120">
    <antd:Card Header="受保护内容">
        <TextBlock Text="水印会平铺到包裹内容之上。" />
    </antd:Card>
</antd:Watermark>
""";
}
