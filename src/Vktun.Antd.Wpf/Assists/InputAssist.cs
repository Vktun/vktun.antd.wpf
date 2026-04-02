using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides prefix and suffix content slots for input controls.
/// </summary>
public static class InputAssist
{
    /// <summary>
    /// Identifies the <see cref="Prefix"/> attached property.
    /// </summary>
    public static readonly DependencyProperty PrefixProperty =
        DependencyProperty.RegisterAttached(
            "Prefix",
            typeof(object),
            typeof(InputAssist),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Identifies the <see cref="Suffix"/> attached property.
    /// </summary>
    public static readonly DependencyProperty SuffixProperty =
        DependencyProperty.RegisterAttached(
            "Suffix",
            typeof(object),
            typeof(InputAssist),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Gets prefix content.
    /// </summary>
    public static object? GetPrefix(DependencyObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(PrefixProperty);
    }

    /// <summary>
    /// Sets prefix content.
    /// </summary>
    public static void SetPrefix(DependencyObject element, object? value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(PrefixProperty, value);
    }

    /// <summary>
    /// Gets suffix content.
    /// </summary>
    public static object? GetSuffix(DependencyObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(SuffixProperty);
    }

    /// <summary>
    /// Sets suffix content.
    /// </summary>
    public static void SetSuffix(DependencyObject element, object? value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(SuffixProperty, value);
    }
}
