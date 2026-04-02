using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides shared semantic status metadata for controls.
/// </summary>
public static class StatusAssist
{
    /// <summary>
    /// Identifies the <see cref="Status"/> attached property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.RegisterAttached(
            "Status",
            typeof(AntdStatus),
            typeof(StatusAssist),
            new FrameworkPropertyMetadata(AntdStatus.None, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Gets the semantic status.
    /// </summary>
    public static AntdStatus GetStatus(DependencyObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return (AntdStatus)element.GetValue(StatusProperty);
    }

    /// <summary>
    /// Sets the semantic status.
    /// </summary>
    public static void SetStatus(DependencyObject element, AntdStatus value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(StatusProperty, value);
    }
}
