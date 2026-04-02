# vktun.antd.wpf

基于 Ant Design 设计语言的 WPF UI 库首版实现，包含：

- `src/Vktun.Antd.Wpf`：主题、token、附加属性、基础控件、消息/通知/弹窗服务
- `samples/Vktun.Antd.Wpf.Sample`：登录页、设置页、工作台示例
- `tests/Vktun.Antd.Wpf.Tests`：主题资源、附加属性和浮层宿主测试

## 快速使用

在应用资源中合并主题资源：

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <antd:AntdThemeResources Theme="Light" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

运行时切换主题：

```csharp
AntdThemeManager.Current.Apply(Application.Current, AntdThemeMode.Dark);
AntdThemeManager.Current.Apply(Application.Current, AntdThemeMode.Light, new AntdSeedToken
{
    PrimaryColor = (Color)ColorConverter.ConvertFromString("#13C2C2")
});
```

典型控件用法：

```xaml
<Button Content="Primary" antd:ButtonAssist.Type="Primary" />
<TextBox antd:InputAssist.Prefix="@" antd:StatusAssist.Status="Success" />
<antd:Switch IsChecked="True" />
<antd:Tag antd:StatusAssist.Status="Warning">Warning</antd:Tag>
```

## 验证

```powershell
dotnet build Vktun.Antd.Wpf.slnx
dotnet test Vktun.Antd.Wpf.slnx
```
