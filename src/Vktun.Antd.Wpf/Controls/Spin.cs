using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a loading spinner indicator.
/// </summary>
public class Spin : Control
{
    static Spin()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Spin),
            new FrameworkPropertyMetadata(typeof(Spin)));
    }

    /// <summary>
    /// Identifies the <see cref="IsSpinning"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsSpinningProperty =
        DependencyProperty.Register(nameof(IsSpinning), typeof(bool), typeof(Spin),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(Spin),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Indicator"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IndicatorProperty =
        DependencyProperty.Register(nameof(Indicator), typeof(object), typeof(Spin),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Tip"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TipProperty =
        DependencyProperty.Register(nameof(Tip), typeof(string), typeof(Spin),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Content"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(Spin),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets whether the spinner is active.
    /// </summary>
    public bool IsSpinning
    {
        get => (bool)GetValue(IsSpinningProperty);
        set => SetValue(IsSpinningProperty, value);
    }

    /// <summary>
    /// Gets or sets the spinner size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom loading indicator.
    /// </summary>
    public object? Indicator
    {
        get => GetValue(IndicatorProperty);
        set => SetValue(IndicatorProperty, value);
    }

    /// <summary>
    /// Gets or sets the loading tip text.
    /// </summary>
    public string? Tip
    {
        get => (string?)GetValue(TipProperty);
        set => SetValue(TipProperty, value);
    }

    /// <summary>
    /// Gets or sets the content to wrap with loading overlay.
    /// </summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
}