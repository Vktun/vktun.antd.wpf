# vktun.antd

基于 Ant Design 设计语言的 .NET 桌面 UI 库集合。仓库顶层不再绑定单一客户端技术，当前提供共享 token 核心库、WPF 实现和 Avalonia 实现骨架。

## 项目结构

- `src/Vktun.Antd.Core`：平台无关的主题 token、颜色模型、枚举和资源键。
- `src/Vktun.Antd.Wpf`：WPF 控件、附加属性、主题资源、消息/通知/弹窗服务。
- `src/Vktun.Antd.Avalonia`：Avalonia 主题资源层，后续控件会按组件批次迁移。
- `samples/Vktun.Antd.Wpf.Sample`：WPF 组件示例。
- `samples/Vktun.Antd.Avalonia.Sample`：Avalonia 主题资源示例。
- `tests/Vktun.Antd.Core.Tests`：共享 token 测试。
- `tests/Vktun.Antd.Wpf.Tests`：WPF 主题资源、附加属性和浮层宿主测试。
- `tests/Vktun.Antd.Avalonia.Tests`：Avalonia 主题资源测试。

## NuGet 包边界

- `Vktun.Antd.Core`
- `Vktun.Antd.Wpf`
- `Vktun.Antd.Avalonia`

仓库和解决方案使用 `Vktun.Antd` 作为品牌名；具体客户端实现仍然保留技术后缀，避免 WPF 和 Avalonia 的运行时、XAML、资源系统混在同一个包里。

## WPF 快速使用

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
    PrimaryColor = AntdColor.Parse("#13C2C2")
});
```

典型控件用法：

```xaml
<Button Content="Primary" antd:ButtonAssist.Type="Primary" />
<TextBox antd:InputAssist.Prefix="@" antd:StatusAssist.Status="Success" />
<antd:Switch IsChecked="True" />
<antd:Tag antd:StatusAssist.Status="Warning">Warning</antd:Tag>
```

## Avalonia 快速使用

```xaml
<Application.Resources>
  <antd:AntdThemeResources Theme="Light" />
</Application.Resources>
```

Avalonia 当前先提供共享 token 到 Avalonia `ResourceDictionary` 的映射层。控件层建议按 Button、Input、Tag、Alert、Card、Switch 的顺序逐步迁移。

## 验证

```powershell
dotnet build Vktun.Antd.slnx
dotnet test Vktun.Antd.slnx
```

## Avalonia component catalog usage

Load the shared token resources and the Avalonia component styles in `App.axaml`:

```xml
<Application.Styles>
  <FluentTheme />
  <StyleInclude Source="avares://Vktun.Antd.Avalonia/Themes/Generic.axaml" />
</Application.Styles>

<Application.Resources>
  <antd:AntdThemeResources Theme="Light" />
</Application.Resources>
```

Switch theme resources at runtime:

```csharp
AntdThemeManager.Current.Apply(Application.Current!, AntdThemeMode.Dark);
AntdThemeManager.Current.Apply(Application.Current!, AntdThemeMode.Light, new AntdSeedToken
{
    PrimaryColor = AntdColor.Parse("#13C2C2"),
});
```

Use the Avalonia controls with the same Ant Design semantic API shape as the WPF package:

```xml
<antd:Button Content="Primary" Type="Primary" />
<antd:Input Watermark="Email" Prefix="@" Status="Success" />
<antd:Switch IsChecked="True" />
<antd:Tag Color="Success">Ready</antd:Tag>
```
