using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a themed slider control.
/// </summary>
public class Slider : System.Windows.Controls.Slider
{
    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(Slider),
            new PropertyMetadata(AntdControlSize.Middle));

    static Slider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Slider), new FrameworkPropertyMetadata(typeof(Slider)));
    }

    /// <summary>
    /// Gets or sets the slider size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
}
