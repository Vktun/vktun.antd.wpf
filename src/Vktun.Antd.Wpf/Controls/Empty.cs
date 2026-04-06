using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents an empty state placeholder component.
/// </summary>
public class Empty : ContentControl
{
    static Empty()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Empty),
            new FrameworkPropertyMetadata(typeof(Empty)));
    }

    /// <summary>
    /// Identifies the <see cref="Description"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(object), typeof(Empty),
            new PropertyMetadata("No Data"));

    /// <summary>
    /// Identifies the <see cref="Image"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register(nameof(Image), typeof(object), typeof(Empty),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="ImageStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ImageStyleProperty =
        DependencyProperty.Register(nameof(ImageStyle), typeof(Style), typeof(Empty),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the description content.
    /// </summary>
    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the custom image content.
    /// </summary>
    public object? Image
    {
        get => GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    /// <summary>
    /// Gets or sets the image style.
    /// </summary>
    public Style? ImageStyle
    {
        get => (Style?)GetValue(ImageStyleProperty);
        set => SetValue(ImageStyleProperty, value);
    }
}