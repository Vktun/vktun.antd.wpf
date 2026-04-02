using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides shared size metadata for controls.
/// </summary>
public static class ControlAssist
{
    /// <summary>
    /// Identifies the <see cref="Size"/> attached property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.RegisterAttached(
            "Size",
            typeof(AntdControlSize),
            typeof(ControlAssist),
            new FrameworkPropertyMetadata(AntdControlSize.Middle, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Gets the control size.
    /// </summary>
    public static AntdControlSize GetSize(DependencyObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return (AntdControlSize)element.GetValue(SizeProperty);
    }

    /// <summary>
    /// Sets the control size.
    /// </summary>
    public static void SetSize(DependencyObject element, AntdControlSize value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(SizeProperty, value);
    }
}
