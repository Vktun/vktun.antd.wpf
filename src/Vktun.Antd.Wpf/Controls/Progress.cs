using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a progress bar component.
/// </summary>
public class Progress : Control
{
    static Progress()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Progress),
            new FrameworkPropertyMetadata(typeof(Progress)));
    }

    /// <summary>
    /// Identifies the <see cref="Percent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PercentProperty =
        DependencyProperty.Register(nameof(Percent), typeof(double), typeof(Progress),
            new PropertyMetadata(0d, OnPercentChanged));

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdProgressType), typeof(Progress),
            new PropertyMetadata(AntdProgressType.Line));

    /// <summary>
    /// Identifies the <see cref="Status"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(nameof(Status), typeof(AntdProgressStatus), typeof(Progress),
            new PropertyMetadata(AntdProgressStatus.Normal, OnPercentChanged));

    /// <summary>
    /// Identifies the <see cref="ShowInfo"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowInfoProperty =
        DependencyProperty.Register(nameof(ShowInfo), typeof(bool), typeof(Progress),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="StrokeWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StrokeWidthProperty =
        DependencyProperty.Register(nameof(StrokeWidth), typeof(double), typeof(Progress),
            new PropertyMetadata(8d));

    /// <summary>
    /// Identifies the <see cref="StrokeColor"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StrokeColorProperty =
        DependencyProperty.Register(nameof(StrokeColor), typeof(Brush), typeof(Progress),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="TrailColor"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TrailColorProperty =
        DependencyProperty.Register(nameof(TrailColor), typeof(Brush), typeof(Progress),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="CircleWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CircleWidthProperty =
        DependencyProperty.Register(nameof(CircleWidth), typeof(double), typeof(Progress),
            new PropertyMetadata(120d));

    /// <summary>
    /// Gets or sets the current progress percentage (0-100).
    /// </summary>
    public double Percent
    {
        get => (double)GetValue(PercentProperty);
        set => SetValue(PercentProperty, value);
    }

    /// <summary>
    /// Gets or sets the progress bar type.
    /// </summary>
    public AntdProgressType Type
    {
        get => (AntdProgressType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the progress status.
    /// </summary>
    public AntdProgressStatus Status
    {
        get => (AntdProgressStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show the percentage info.
    /// </summary>
    public bool ShowInfo
    {
        get => (bool)GetValue(ShowInfoProperty);
        set => SetValue(ShowInfoProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke line width.
    /// </summary>
    public double StrokeWidth
    {
        get => (double)GetValue(StrokeWidthProperty);
        set => SetValue(StrokeWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the stroke color.
    /// </summary>
    public Brush? StrokeColor
    {
        get => (Brush?)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the trail (background) color.
    /// </summary>
    public Brush? TrailColor
    {
        get => (Brush?)GetValue(TrailColorProperty);
        set => SetValue(TrailColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the circle diameter for Circle/Dashboard types.
    /// </summary>
    public double CircleWidth
    {
        get => (double)GetValue(CircleWidthProperty);
        set => SetValue(CircleWidthProperty, value);
    }

    private static void OnPercentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var progress = (Progress)d;
        var percent = progress.Percent;

        // Auto-set status based on percent
        if (percent >= 100)
        {
            if (progress.Status != AntdProgressStatus.Exception)
            {
                progress.SetCurrentValue(StatusProperty, AntdProgressStatus.Success);
            }
        }
    }
}