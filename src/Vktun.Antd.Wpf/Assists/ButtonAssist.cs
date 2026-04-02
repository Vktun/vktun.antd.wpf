using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides attached properties for semantic button variants.
/// </summary>
public static class ButtonAssist
{
    /// <summary>
    /// Identifies the <see cref="Type"/> attached property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.RegisterAttached(
            "Type",
            typeof(AntdButtonType),
            typeof(ButtonAssist),
            new FrameworkPropertyMetadata(AntdButtonType.Default, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Gets the semantic button type.
    /// </summary>
    public static AntdButtonType GetType(DependencyObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return (AntdButtonType)element.GetValue(TypeProperty);
    }

    /// <summary>
    /// Sets the semantic button type.
    /// </summary>
    public static void SetType(DependencyObject element, AntdButtonType value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(TypeProperty, value);
    }
}
