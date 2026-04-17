---
name: wpf-antd
description: Use when adding, modifying, or reviewing WPF code in the Vktun.Antd component library, especially controls, dependency properties, Generic.xaml resources, Ant Design theme tokens, overlay services, and WpfTestHost tests.
---

# WPF Ant Design Skill

## Overview

Use this skill for WPF work in the `Vktun.Antd` repository. The core principle is that C# owns behavior and public contracts, XAML owns visuals, and theme values flow from shared Ant Design tokens through dynamic WPF resources.

## Required Context

Read `.agents/rules/wpf-antd.rules.md` before editing WPF code. Then inspect the closest existing control, style dictionary, sample, and test that match the requested work.

## Standard Workflow

1. Identify the owning layer: `Core` for platform-neutral token/data work, `Wpf` for controls/resources/services, sample project for demonstrations, test project for coverage.
2. Match existing patterns before adding new ones.
3. Define or update public API with dependency properties, XML docs, and typed enums where appropriate.
4. Put visuals in the right `Themes/Styles/*.xaml` dictionary and merge new dictionaries through `Themes/Generic.xaml`.
5. Use `DynamicResource` and `core:AntdResourceKeys` for theme-dependent values.
6. Add or update WPF tests with `WpfTestHost.Run`, `AntdThemeResources`, and `FluentAssertions`.
7. Update samples or snippets when public usage changes.
8. Verify with `dotnet build Vktun.Antd.slnx` and the relevant `dotnet test` command.

## Control Pattern

```csharp
public class ExampleControl : Control
{
    static ExampleControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ExampleControl),
            new FrameworkPropertyMetadata(typeof(ExampleControl)));
    }

    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(nameof(Status), typeof(AntdStatus), typeof(ExampleControl),
            new PropertyMetadata(AntdStatus.None));

    public AntdStatus Status
    {
        get => (AntdStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
}
```

## XAML Pattern

```xaml
<Style TargetType="{x:Type local:ExampleControl}">
    <Setter Property="Foreground" Value="{DynamicResource {x:Static core:AntdResourceKeys.BrushText}}" />
    <Setter Property="Background" Value="{DynamicResource {x:Static core:AntdResourceKeys.BrushBgContainer}}" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type local:ExampleControl}">
                <Border Background="{TemplateBinding Background}">
                    <ContentPresenter />
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## Testing Pattern

```csharp
[TestMethod]
public void ExampleControl_ShouldApplyTemplateAndUseThemeResources()
{
    WpfTestHost.Run(() =>
    {
        Application.Current!.Resources = new ResourceDictionary();
        Application.Current.Resources.MergedDictionaries.Add(new AntdThemeResources());

        var control = new ExampleControl();
        var window = new Window { Content = control, Width = 420, Height = 280 };

        try
        {
            window.Show();
            control.ApplyTemplate();
            WpfTestHost.Pump();

            control.Template.Should().NotBeNull();
        }
        finally
        {
            window.Close();
        }
    });
}
```

## Common Mistakes

| Mistake | Correction |
| --- | --- |
| Hard-coding theme colors in templates | Use `DynamicResource` and `AntdResourceKeys` |
| Adding visuals in C# for a lookless control | Move structure to the control template |
| Reading template parts without tests | Add `PART_` names, `TemplatePart`, and WPF template tests |
| Updating WPF behavior without samples | Add sample usage that matches the public API |
| Forgetting theme switching | Test light/dark resource updates for visual changes |
