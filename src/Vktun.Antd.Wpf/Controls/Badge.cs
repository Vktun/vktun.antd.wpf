using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a content presenter with an overlaid count or dot.
/// </summary>
public class Badge : ContentControl
{
    static Badge()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Badge), new FrameworkPropertyMetadata(typeof(Badge)));
    }

    /// <summary>
    /// Identifies the <see cref="Count"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CountProperty =
        DependencyProperty.Register(nameof(Count), typeof(object), typeof(Badge), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Dot"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DotProperty =
        DependencyProperty.Register(nameof(Dot), typeof(bool), typeof(Badge), new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the badge count.
    /// </summary>
    public object? Count
    {
        get => GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether only a dot should be shown.
    /// </summary>
    public bool Dot
    {
        get => (bool)GetValue(DotProperty);
        set => SetValue(DotProperty, value);
    }
}
