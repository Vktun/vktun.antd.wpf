namespace Vktun.Antd.Wpf.Sample.Snippets;

public static class DataEntrySnippets
{
    public static string CheckboxGroup => """
<StackPanel>
    <antd:Checkbox Content="接收通知" />
    <antd:Checkbox Content="允许协作" Indeterminate="True" />

    <antd:CheckboxGroup>
        <antd:Checkbox Content="北京" Tag="beijing" />
        <antd:Checkbox Content="上海" Tag="shanghai" />
        <antd:Checkbox Content="深圳" Tag="shenzhen" />
    </antd:CheckboxGroup>
</StackPanel>
""";

    public static string Input => """
<antd:Input Width="260"
            Prefix="@"
            Suffix=".com"
            AllowClear="True"
            Text="ant-design-wpf" />
""";

    public static string PasswordInput => """
<antd:PasswordInput Width="260"
                    Prefix="Lock"
                    Password="DesignSystem!"
                    Status="Warning" />
""";

    public static string SelectComboBox => """
<antd:ComboBox Width="220"
               SelectedIndex="0"
               Prefix="环境">
    <ComboBoxItem Content="开发环境" />
    <ComboBoxItem Content="预发环境" />
    <ComboBoxItem Content="生产环境" />
</antd:ComboBox>
""";

    public static string Form => """
<antd:Form Layout="Vertical">
    <antd:FormItem Label="应用名称" Name="name" Required="True">
        <antd:Input Text="Antd WPF" />
    </antd:FormItem>
    <antd:FormItem Label="部署环境" Name="env">
        <antd:ComboBox SelectedIndex="0">
            <ComboBoxItem Content="Production" />
            <ComboBoxItem Content="Staging" />
        </antd:ComboBox>
    </antd:FormItem>
</antd:Form>
""";

    public static string RadioGroup => """
<StackPanel>
    <antd:Radio Content="日报" IsChecked="True" />
    <antd:Radio Content="周报" />

    <antd:RadioGroup Value="mail">
        <antd:Radio Content="邮件" Tag="mail" />
        <antd:Radio Content="短信" Tag="sms" />
        <antd:Radio Content="站内信" Tag="inbox" />
    </antd:RadioGroup>
</StackPanel>
""";

    public static string DatePicker => """
<antd:Space Orientation="Horizontal" Gap="12">
    <antd:DatePicker SelectedDate="2026-04-08"
                     AllowClear="True" />
    <antd:RangePicker StartValue="2026-04-01"
                      EndValue="2026-04-08" />
</antd:Space>
""";

    public static string Switch => """
<antd:Switch IsChecked="True"
             CheckedChildren="启用"
             UnCheckedChildren="停用" />
""";

    public static string InputNumber => """
<antd:InputNumber Width="180"
                  Value="128"
                  Minimum="0"
                  Maximum="999"
                  Step="8"
                  Prefix="RMB"
                  Suffix="CNY" />
""";

    public static string Slider => """
<antd:Slider Width="240"
             Minimum="0"
             Maximum="100"
             Value="64" />
""";
}
