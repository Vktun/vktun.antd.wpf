using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a segmented single-selection control.
/// </summary>
public class Segmented : ListBox
{
    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(Segmented),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Block"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BlockProperty =
        DependencyProperty.Register(nameof(Block), typeof(bool), typeof(Segmented),
            new PropertyMetadata(false));

    static Segmented()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Segmented), new FrameworkPropertyMetadata(typeof(Segmented)));
    }

    public Segmented()
    {
        SelectionMode = SelectionMode.Single;
    }

    /// <summary>
    /// Gets or sets the segmented size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether items should evenly fill the available width.
    /// </summary>
    public bool Block
    {
        get => (bool)GetValue(BlockProperty);
        set => SetValue(BlockProperty, value);
    }
}
