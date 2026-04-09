using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a content wrapper that paints a tiled text watermark over its content.
/// </summary>
public class Watermark : ContentControl
{
    private static readonly DependencyPropertyKey WatermarkBrushPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(WatermarkBrush), typeof(Brush), typeof(Watermark),
            new PropertyMetadata(Brushes.Transparent));

    /// <summary>
    /// Identifies the <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(Watermark),
            new PropertyMetadata(string.Empty, OnWatermarkPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="Angle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AngleProperty =
        DependencyProperty.Register(nameof(Angle), typeof(double), typeof(Watermark),
            new PropertyMetadata(-24d, OnWatermarkPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="GapX"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty GapXProperty =
        DependencyProperty.Register(nameof(GapX), typeof(double), typeof(Watermark),
            new PropertyMetadata(180d, OnWatermarkPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="GapY"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty GapYProperty =
        DependencyProperty.Register(nameof(GapY), typeof(double), typeof(Watermark),
            new PropertyMetadata(120d, OnWatermarkPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="WatermarkBrush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WatermarkBrushProperty = WatermarkBrushPropertyKey.DependencyProperty;

    static Watermark()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Watermark), new FrameworkPropertyMetadata(typeof(Watermark)));
        OpacityProperty.OverrideMetadata(typeof(Watermark), new FrameworkPropertyMetadata(0.12d, OnWatermarkPropertyChanged));
        FontSizeProperty.OverrideMetadata(typeof(Watermark), new FrameworkPropertyMetadata(18d, OnWatermarkPropertyChanged));
    }

    /// <summary>
    /// Gets or sets the watermark text.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the watermark rotation angle.
    /// </summary>
    public double Angle
    {
        get => (double)GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal tile spacing.
    /// </summary>
    public double GapX
    {
        get => (double)GetValue(GapXProperty);
        set => SetValue(GapXProperty, value);
    }

    /// <summary>
    /// Gets or sets the vertical tile spacing.
    /// </summary>
    public double GapY
    {
        get => (double)GetValue(GapYProperty);
        set => SetValue(GapYProperty, value);
    }

    /// <summary>
    /// Gets the rendered watermark brush.
    /// </summary>
    public Brush WatermarkBrush
    {
        get => (Brush)GetValue(WatermarkBrushProperty);
        private set => SetValue(WatermarkBrushPropertyKey, value);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.Property == ForegroundProperty)
        {
            UpdateWatermarkBrush();
        }
    }

    private static void OnWatermarkPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Watermark watermark)
        {
            watermark.UpdateWatermarkBrush();
        }
    }

    private void UpdateWatermarkBrush()
    {
        if (string.IsNullOrWhiteSpace(Text))
        {
            WatermarkBrush = Brushes.Transparent;
            return;
        }

        var foreground = Foreground as SolidColorBrush ?? new SolidColorBrush(Color.FromRgb(0x16, 0x77, 0xFF));
        var ink = Color.FromArgb(
            (byte)System.Math.Clamp(Opacity * 255d, 0d, 255d),
            foreground.Color.R,
            foreground.Color.G,
            foreground.Color.B);

        var tile = new Grid
        {
            Width = System.Math.Max(120d, GapX),
            Height = System.Math.Max(80d, GapY),
            Background = Brushes.Transparent,
        };

        tile.Children.Add(new TextBlock
        {
            Text = Text,
            FontSize = FontSize,
            FontWeight = FontWeights.SemiBold,
            Foreground = new SolidColorBrush(ink),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            RenderTransformOrigin = new Point(0.5, 0.5),
            RenderTransform = new RotateTransform(Angle),
        });

        WatermarkBrush = new VisualBrush(tile)
        {
            TileMode = TileMode.Tile,
            Stretch = Stretch.None,
            Viewbox = new Rect(0, 0, tile.Width, tile.Height),
            ViewboxUnits = BrushMappingMode.Absolute,
            Viewport = new Rect(0, 0, tile.Width, tile.Height),
            ViewportUnits = BrushMappingMode.Absolute,
            AlignmentX = AlignmentX.Left,
            AlignmentY = AlignmentY.Top,
        };
    }
}
