using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a divider line for separating content.
/// </summary>
public class Divider : Control
{
    static Divider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Divider),
            new FrameworkPropertyMetadata(typeof(Divider)));
    }

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(Divider),
            new PropertyMetadata(Orientation.Horizontal));

    /// <summary>
    /// Identifies the <see cref="Dashed"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DashedProperty =
        DependencyProperty.Register(nameof(Dashed), typeof(bool), typeof(Divider),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="TextPlacement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextPlacementProperty =
        DependencyProperty.Register(nameof(TextPlacement), typeof(AntdDividerTextPlacement), typeof(Divider),
            new PropertyMetadata(AntdDividerTextPlacement.Center));

    /// <summary>
    /// Identifies the <see cref="Content"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(Divider),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the divider orientation.
    /// </summary>
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the divider is dashed.
    /// </summary>
    public bool Dashed
    {
        get => (bool)GetValue(DashedProperty);
        set => SetValue(DashedProperty, value);
    }

    /// <summary>
    /// Gets or sets the text placement for horizontal dividers.
    /// </summary>
    public AntdDividerTextPlacement TextPlacement
    {
        get => (AntdDividerTextPlacement)GetValue(TextPlacementProperty);
        set => SetValue(TextPlacementProperty, value);
    }

    /// <summary>
    /// Gets or sets the content displayed in the divider.
    /// </summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
}