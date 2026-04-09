namespace Vktun.Antd.Wpf.Sample.Snippets;

public static class NavigationSnippets
{
    public static string Breadcrumb => """
<antd:Breadcrumb>
    <antd:BreadcrumbItem Header="首页" />
    <antd:BreadcrumbItem Header="组件" />
    <antd:BreadcrumbItem Header="Breadcrumb" IsLast="True" />
</antd:Breadcrumb>
""";

    public static string Dropdown => """
<antd:Dropdown Trigger="Click">
    <antd:Dropdown.Menu>
        <antd:DropdownMenu>
            <antd:DropdownItem Header="查看详情" Key="view" />
            <antd:DropdownItem Header="编辑" Key="edit" />
            <antd:DropdownItem Header="删除" Key="delete" Danger="True" />
        </antd:DropdownMenu>
    </antd:Dropdown.Menu>
    <antd:Button Content="更多操作" />
</antd:Dropdown>
""";

    public static string DropdownButton => """
<antd:DropdownButton Content="发布"
                     Type="Primary"
                     Arrow="True">
    <antd:DropdownButton.Menu>
        <antd:DropdownMenu>
            <antd:DropdownItem Header="发布到生产" Key="prod" />
            <antd:DropdownItem Header="发布到预发" Key="staging" />
        </antd:DropdownMenu>
    </antd:DropdownButton.Menu>
</antd:DropdownButton>
""";

    public static string Menu => """
<antd:Menu Width="220" SelectedKey="dashboard">
    <antd:MenuItem Header="Dashboard" Key="dashboard" />
    <antd:Submenu Header="系统管理">
        <antd:MenuItem Header="用户" Key="users" />
        <antd:MenuItem Header="角色" Key="roles" />
    </antd:Submenu>
</antd:Menu>
""";

    public static string Pagination => """
<antd:Pagination Total="240"
                 CurrentPage="3"
                 PageSize="20"
                 ShowSizeChanger="True"
                 ShowQuickJumper="True" />
""";

    public static string Steps => """
<antd:Steps Current="1">
    <antd:Step Title="已创建" Description="录入基础信息" />
    <antd:Step Title="审核中" Description="等待审批" />
    <antd:Step Title="已完成" Description="发布上线" />
</antd:Steps>
""";

    public static string Tabs => """
var tabs = new Tabs
{
    Items =
    {
        new TabPane { Key = "design", Header = "Design", Content = new TextBlock { Text = "Design token" } },
        new TabPane { Key = "code", Header = "Code", Content = new TextBlock { Text = "Code sample" } },
    },
    SelectedIndex = 0,
};
""";
}
