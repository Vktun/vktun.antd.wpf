using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents an avatar component for displaying user icons or images.
/// </summary>
public class Avatar : ContentControl
{
    static Avatar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Avatar),
            new FrameworkPropertyMetadata(typeof(Avatar)));
    }

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(Avatar),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Shape"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeProperty =
        DependencyProperty.Register(nameof(Shape), typeof(AntdAvatarShape), typeof(Avatar),
            new PropertyMetadata(AntdAvatarShape.Circle));

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(Avatar),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(Avatar),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="ImageSource"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(Avatar),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Gap"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty GapProperty =
        DependencyProperty.Register(nameof(Gap), typeof(double), typeof(Avatar),
            new PropertyMetadata(4d));

    /// <summary>
    /// Gets or sets the avatar size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the avatar shape.
    /// </summary>
    public AntdAvatarShape Shape
    {
        get => (AntdAvatarShape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon content.
    /// </summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets the text content.
    /// </summary>
    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the image source.
    /// </summary>
    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the gap between text and avatar edge.
    /// </summary>
    public double Gap
    {
        get => (double)GetValue(GapProperty);
        set => SetValue(GapProperty, value);
    }
}