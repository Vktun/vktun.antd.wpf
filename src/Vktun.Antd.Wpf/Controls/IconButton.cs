using System;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a button with a dedicated icon slot.
/// </summary>
[Obsolete("Use antd:Button with Icon property instead. Will be removed in v2.0.")]
public class IconButton : Button
{
    static IconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
    }

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(IconButton), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the icon content.
    /// </summary>
    public new object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}
