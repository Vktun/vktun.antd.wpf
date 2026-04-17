# Vktun.Antd

`Vktun.Antd` 是一套基于 Ant Design 设计语言的 .NET 桌面 UI 组件库。仓库按平台拆分实现，共享主题 token、颜色算法、资源键和语义枚举，避免把 WPF 与 Avalonia 的运行时、XAML 和资源系统混在同一个包里。

当前包含：

- `Vktun.Antd.Core`：平台无关的主题 token、颜色模型、资源键、枚举和算法。
- `Vktun.Antd.Wpf`：WPF 控件、主题资源、附加属性、浮层服务和样式。
- `Vktun.Antd.Avalonia`：Avalonia 11.3.2 控件、主题资源、样式和浮层服务。
- `Vktun.Antd.Wpf.Sample`：WPF 组件示例。
- `Vktun.Antd.Avalonia.Sample`：Avalonia 组件示例、主题预设和 WPF Catalog 对照展示。

## 项目结构

```text
src/
  Vktun.Antd.Core/
  Vktun.Antd.Wpf/
  Vktun.Antd.Avalonia/
samples/
  Vktun.Antd.Wpf.Sample/
  Vktun.Antd.Avalonia.Sample/
tests/
  Vktun.Antd.Core.Tests/
  Vktun.Antd.Wpf.Tests/
  Vktun.Antd.Avalonia.Tests/
```

## NuGet 包边界

- `Vktun.Antd.Core`
- `Vktun.Antd.Wpf`
- `Vktun.Antd.Avalonia`

应用只需要引用对应平台包。WPF 项目引用 `Vktun.Antd.Wpf`，Avalonia 项目引用 `Vktun.Antd.Avalonia`。两个 UI 包都会复用 `Vktun.Antd.Core` 中的共享 token 和枚举。

## Avalonia 使用方法

### 1. 安装或引用项目

NuGet 发布后可直接安装：

```powershell
dotnet add package Vktun.Antd.Avalonia
```

在本仓库内开发或调试时，可以使用项目引用：

```xml
<ProjectReference Include="..\..\src\Vktun.Antd.Avalonia\Vktun.Antd.Avalonia.csproj" />
```

Avalonia 应用通常还需要 Fluent 主题包：

```powershell
dotnet add package Avalonia.Themes.Fluent --version 11.3.2
```

### 2. 在 `App.axaml` 中加载主题和样式

在 Avalonia 应用的 `App.axaml` 中引入命名空间、基础主题、组件样式和 Ant Design token 资源：

```xml
<Application x:Class="YourApp.App"
             xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:antd="clr-namespace:Vktun.Antd.Avalonia;assembly=Vktun.Antd.Avalonia">
  <Application.Styles>
    <FluentTheme />
    <StyleInclude Source="avares://Vktun.Antd.Avalonia/Themes/Generic.axaml" />
  </Application.Styles>

  <Application.Resources>
    <antd:AntdThemeResources Theme="Light" />
  </Application.Resources>
</Application>
```

`AntdThemeResources` 会把 `Vktun.Antd.Core` 中生成的 token 投影为 Avalonia `ResourceDictionary`，组件样式通过 `{DynamicResource ...}` 读取这些资源。

### 3. 在 AXAML 中使用控件

页面或窗口中添加命名空间：

```xml
xmlns:antd="clr-namespace:Vktun.Antd.Avalonia;assembly=Vktun.Antd.Avalonia"
```

常用控件示例：

```xml
<StackPanel Spacing="12">
  <antd:Button Content="Primary" Type="Primary" />
  <antd:Button Content="Dashed" Type="Dashed" />

  <antd:Input Watermark="Email" Prefix="@" Suffix=".com" Status="Success" />
  <antd:PasswordInput Watermark="Password" />
  <antd:InputNumber Minimum="0" Maximum="10" Value="6" Precision="0" />

  <antd:Checkbox Content="Checkbox" IsChecked="True" />
  <antd:Radio Content="Radio" IsChecked="True" />
  <antd:Switch IsChecked="True" />
  <antd:Slider Minimum="0" Maximum="100" Value="64" />

  <antd:Tag Content="Success" Color="Success" />
  <antd:Alert Type="Info"
              Message="Info alert"
              Description="Avalonia alert surface" />

  <antd:Card Title="Card">
    <TextBlock Text="Card content" />
  </antd:Card>
</StackPanel>
```

### 4. 运行时切换主题

使用 `AntdThemeManager` 切换 Light/Dark 或自定义 seed token：

```csharp
AntdThemeManager.Current.Apply(Application.Current!, AntdThemeMode.Dark);

AntdThemeManager.Current.Apply(Application.Current!, AntdThemeMode.Light, new AntdSeedToken
{
    PrimaryColor = AntdColor.Parse("#13C2C2"),
    SuccessColor = AntdColor.Parse("#52C41A"),
    WarningColor = AntdColor.Parse("#FAAD14"),
    ErrorColor = AntdColor.Parse("#FF4D4F"),
    BorderRadius = 12,
});
```

如果需要像 sample 一样做主题预设，可以在调用 `Apply` 后覆盖具体资源：

```csharp
Application.Current!.Resources[AntdResourceKeys.BrushBgLayout] =
    new SolidColorBrush(Color.Parse("#F6FBFF"));
Application.Current.Resources[AntdResourceKeys.BrushBgContainer] =
    new SolidColorBrush(Color.Parse("#FFFFFF"));
Application.Current.Resources[AntdResourceKeys.BrushBorderSecondary] =
    new SolidColorBrush(Color.Parse("#D0D9E4"));
```

### 5. 浮层服务用法

Avalonia 浮层使用 `OverlayHost` 挂载到宿主控件，不依赖 WPF `Window` 机制。

```csharp
var root = new Grid();

var messageService = new MessageService();
messageService.Show(root, "保存成功", MessageKind.Success);

var notificationService = new NotificationService();
notificationService.Show(root, new NotificationRequest
{
    Title = "上传完成",
    Description = "文件已处理完成",
});

var modalService = new ModalService();
bool confirmed = await modalService.ShowAsync(root, new ModalRequest
{
    Title = "确认操作",
    Content = "是否继续？",
});
```

### 6. Avalonia 示例程序

运行 Avalonia 示例：

```powershell
dotnet run --project samples/Vktun.Antd.Avalonia.Sample/Vktun.Antd.Avalonia.Sample.csproj
```

示例程序包含：

- 左侧组件分类导航。
- 默认、暗黑、类 MUI、类 shadcn、卡通、插画、Bootstrap、玻璃、极客等主题预设。
- WPF sample 中出现过的控件逐项展示。
- 对尚未提供同名 Avalonia 控件的 WPF-only 项，先使用 Avalonia 原生控件或组合控件做“原生适配”展示。

## WPF 使用方法

### 1. 安装或引用项目

```powershell
dotnet add package Vktun.Antd.Wpf
```

在本仓库内开发或调试时，可以使用项目引用：

```xml
<ProjectReference Include="..\..\src\Vktun.Antd.Wpf\Vktun.Antd.Wpf.csproj" />
```

### 2. 在应用资源中合并主题

```xml
<Application.Resources>
  <ResourceDictionary>
    <ResourceDictionary.MergedDictionaries>
      <antd:AntdThemeResources Theme="Light" />
    </ResourceDictionary.MergedDictionaries>
  </ResourceDictionary>
</Application.Resources>
```

### 3. 使用 WPF 控件和附加属性

```xml
<Button Content="Primary" antd:ButtonAssist.Type="Primary" />
<TextBox antd:InputAssist.Prefix="@"
         antd:StatusAssist.Status="Success" />
<antd:Switch IsChecked="True" />
<antd:Tag antd:StatusAssist.Status="Warning">Warning</antd:Tag>
```

### 4. 运行时切换主题

```csharp
AntdThemeManager.Current.Apply(Application.Current, AntdThemeMode.Dark);

AntdThemeManager.Current.Apply(Application.Current, AntdThemeMode.Light, new AntdSeedToken
{
    PrimaryColor = AntdColor.Parse("#13C2C2"),
});
```

## 组件覆盖概览

Avalonia 当前 sample 已按 WPF sample 分类展示以下控件或原生适配项：

- 通用：`Button`、`FloatButton`、`TypographyTitle`、`TypographyText`、`TypographyParagraph`。
- 布局：`Divider`、`Row`、`Col`、`Space`、`Flex`、`Layout`、`LayoutHeader`、`LayoutSider`、`LayoutContent`、`LayoutFooter`、`Splitter`。
- 导航：`Breadcrumb`、`BreadcrumbItem`、`Dropdown`、`DropdownMenu`、`DropdownItem`、`DropdownButton`、`Menu`、`MenuItem`、`Submenu`、`Pagination`、`Steps`、`Step`、`Tabs`。
- 数据录入：`Checkbox`、`CheckboxGroup`、`Input`、`PasswordInput`、`ComboBox`、`Select`、`Form`、`FormItem`、`Radio`、`RadioGroup`、`DatePicker`、`RangePicker`、`Switch`、`InputNumber`、`Slider`。
- 数据展示：`Avatar`、`Badge`、`Card`、`Collapse`、`CollapsePanel`、`Descriptions`、`DescriptionItem`、`Empty`、`List`、`ListItem`、`Tooltip`、`Popover`、`Table`、`Tag`、`Timeline`、`TimelineItem`、`Calendar`、`Segmented`、`Statistic`。
- 反馈：`Alert`、`Drawer`、`Message`、`Modal`、`Notification`、`Popconfirm`、`Progress`、`Result`、`Skeleton`、`Spin`、`Watermark`。

## 构建和测试

```powershell
dotnet build Vktun.Antd.slnx --no-restore
dotnet test Vktun.Antd.slnx --no-restore
```

只验证 Avalonia：

```powershell
dotnet test tests/Vktun.Antd.Avalonia.Tests/Vktun.Antd.Avalonia.Tests.csproj --no-restore
```

## 开发约定

- 平台无关的 token、颜色算法、资源键和枚举放在 `Vktun.Antd.Core`。
- WPF 与 Avalonia 保持独立 UI 实现，不互相引用平台专有类型。
- 主题相关样式优先使用动态资源，不在控件模板中硬编码业务颜色。
- 新控件或行为变更需要同步 sample 和测试。
