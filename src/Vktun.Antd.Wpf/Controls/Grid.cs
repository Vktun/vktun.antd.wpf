using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a row container in the Ant Design grid system.
/// </summary>
public class Row : Panel
{
    static Row()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Row), new FrameworkPropertyMetadata(typeof(Row)));
    }

    /// <summary>
    /// Gets or sets the gutter spacing between columns.
    /// </summary>
    public double Gutter
    {
        get => (double)GetValue(GutterProperty);
        set => SetValue(GutterProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Gutter"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty GutterProperty =
        DependencyProperty.Register(nameof(Gutter), typeof(double), typeof(Row),
            new PropertyMetadata(0.0, OnGutterChanged));

    private static void OnGutterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Row row)
        {
            row.InvalidateMeasure();
            row.InvalidateArrange();
        }
    }

    /// <summary>
    /// Gets or sets whether the row should wrap to next line.
    /// </summary>
    public bool Wrap
    {
        get => (bool)GetValue(WrapProperty);
        set => SetValue(WrapProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Wrap"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WrapProperty =
        DependencyProperty.Register(nameof(Wrap), typeof(bool), typeof(Row),
            new PropertyMetadata(true, OnWrapChanged));

    private static void OnWrapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Row row)
        {
            row.InvalidateMeasure();
            row.InvalidateArrange();
        }
    }

    /// <summary>
    /// Gets or sets the horizontal alignment of columns.
    /// </summary>
    public HorizontalAlignment Justify
    {
        get => (HorizontalAlignment)GetValue(JustifyProperty);
        set => SetValue(JustifyProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Justify"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty JustifyProperty =
        DependencyProperty.Register(nameof(Justify), typeof(HorizontalAlignment), typeof(Row),
            new PropertyMetadata(HorizontalAlignment.Left));

    /// <summary>
    /// Gets or sets the vertical alignment of columns.
    /// </summary>
    public VerticalAlignment Align
    {
        get => (VerticalAlignment)GetValue(AlignProperty);
        set => SetValue(AlignProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Align"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AlignProperty =
        DependencyProperty.Register(nameof(Align), typeof(VerticalAlignment), typeof(Row),
            new PropertyMetadata(VerticalAlignment.Top));

    protected override Size MeasureOverride(Size availableSize)
    {
        var gutter = Gutter;
        var totalGutter = gutter * (InternalChildren.Count - 1);
        var availableWidth = availableSize.Width - totalGutter;

        double currentRowHeight = 0;
        double currentRowWidth = 0;
        double totalHeight = 0;

        foreach (UIElement child in InternalChildren)
        {
            if (child is Col col)
            {
                var span = GetEffectiveSpan(col, availableSize.Width);
                var colWidth = (availableWidth * span) / 24;
                
                child.Measure(new Size(colWidth, availableSize.Height));
                
                if (Wrap && currentRowWidth + colWidth > availableWidth)
                {
                    totalHeight += currentRowHeight;
                    currentRowWidth = colWidth;
                    currentRowHeight = child.DesiredSize.Height;
                }
                else
                {
                    currentRowWidth += colWidth + gutter;
                    currentRowHeight = Math.Max(currentRowHeight, child.DesiredSize.Height);
                }
            }
        }

        totalHeight += currentRowHeight;
        return new Size(availableSize.Width, totalHeight);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var gutter = Gutter;
        var totalGutter = gutter * (InternalChildren.Count - 1);
        var availableWidth = finalSize.Width - totalGutter;

        double currentX = 0;
        double currentY = 0;
        double currentRowHeight = 0;
        double currentRowWidth = 0;

        foreach (UIElement child in InternalChildren)
        {
            if (child is Col col)
            {
                var span = GetEffectiveSpan(col, finalSize.Width);
                var offset = GetEffectiveOffset(col, finalSize.Width);
                var colWidth = (availableWidth * span) / 24;
                var offsetX = (availableWidth * offset) / 24;

                if (Wrap && currentX + colWidth + offsetX > finalSize.Width && currentX > 0)
                {
                    currentY += currentRowHeight;
                    currentX = offsetX;
                    currentRowHeight = 0;
                }
                else
                {
                    currentX += offsetX;
                }

                var rect = new Rect(currentX, currentY, colWidth, child.DesiredSize.Height);
                child.Arrange(rect);

                currentX += colWidth + gutter;
                currentRowWidth += colWidth + gutter;
                currentRowHeight = Math.Max(currentRowHeight, child.DesiredSize.Height);
            }
        }

        return finalSize;
    }

    private int GetEffectiveSpan(Col col, double containerWidth)
    {
        // Responsive breakpoints
        if (containerWidth < 576 && col.XsSpan > 0) return col.XsSpan;
        if (containerWidth < 768 && col.SmSpan > 0) return col.SmSpan;
        if (containerWidth < 992 && col.MdSpan > 0) return col.MdSpan;
        if (containerWidth < 1200 && col.LgSpan > 0) return col.LgSpan;
        if (containerWidth >= 1200 && col.XlSpan > 0) return col.XlSpan;

        return col.Span;
    }

    private int GetEffectiveOffset(Col col, double containerWidth)
    {
        // Responsive breakpoints
        if (containerWidth < 576 && col.XsOffset > 0) return col.XsOffset;
        if (containerWidth < 768 && col.SmOffset > 0) return col.SmOffset;
        if (containerWidth < 992 && col.MdOffset > 0) return col.MdOffset;
        if (containerWidth < 1200 && col.LgOffset > 0) return col.LgOffset;
        if (containerWidth >= 1200 && col.XlOffset > 0) return col.XlOffset;

        return col.Offset;
    }
}

/// <summary>
/// Represents a column in the Ant Design grid system.
/// </summary>
public class Col : ContentControl
{
    static Col()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Col), new FrameworkPropertyMetadata(typeof(Col)));
    }

    /// <summary>
    /// Gets or sets the number of columns to span (1-24).
    /// </summary>
    public int Span
    {
        get => (int)GetValue(SpanProperty);
        set => SetValue(SpanProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Span"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SpanProperty =
        DependencyProperty.Register(nameof(Span), typeof(int), typeof(Col),
            new PropertyMetadata(24));

    /// <summary>
    /// Gets or sets the number of columns to offset.
    /// </summary>
    public int Offset
    {
        get => (int)GetValue(OffsetProperty);
        set => SetValue(OffsetProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Offset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OffsetProperty =
        DependencyProperty.Register(nameof(Offset), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the span for extra small screens (&lt;576px).
    /// </summary>
    public int XsSpan
    {
        get => (int)GetValue(XsSpanProperty);
        set => SetValue(XsSpanProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="XsSpan"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XsSpanProperty =
        DependencyProperty.Register(nameof(XsSpan), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the offset for extra small screens (&lt;576px).
    /// </summary>
    public int XsOffset
    {
        get => (int)GetValue(XsOffsetProperty);
        set => SetValue(XsOffsetProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="XsOffset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XsOffsetProperty =
        DependencyProperty.Register(nameof(XsOffset), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the span for small screens (≥576px).
    /// </summary>
    public int SmSpan
    {
        get => (int)GetValue(SmSpanProperty);
        set => SetValue(SmSpanProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="SmSpan"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SmSpanProperty =
        DependencyProperty.Register(nameof(SmSpan), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the offset for small screens (≥576px).
    /// </summary>
    public int SmOffset
    {
        get => (int)GetValue(SmOffsetProperty);
        set => SetValue(SmOffsetProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="SmOffset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SmOffsetProperty =
        DependencyProperty.Register(nameof(SmOffset), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the span for medium screens (≥768px).
    /// </summary>
    public int MdSpan
    {
        get => (int)GetValue(MdSpanProperty);
        set => SetValue(MdSpanProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="MdSpan"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MdSpanProperty =
        DependencyProperty.Register(nameof(MdSpan), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the offset for medium screens (≥768px).
    /// </summary>
    public int MdOffset
    {
        get => (int)GetValue(MdOffsetProperty);
        set => SetValue(MdOffsetProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="MdOffset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MdOffsetProperty =
        DependencyProperty.Register(nameof(MdOffset), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the span for large screens (≥992px).
    /// </summary>
    public int LgSpan
    {
        get => (int)GetValue(LgSpanProperty);
        set => SetValue(LgSpanProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="LgSpan"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LgSpanProperty =
        DependencyProperty.Register(nameof(LgSpan), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the offset for large screens (≥992px).
    /// </summary>
    public int LgOffset
    {
        get => (int)GetValue(LgOffsetProperty);
        set => SetValue(LgOffsetProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="LgOffset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LgOffsetProperty =
        DependencyProperty.Register(nameof(LgOffset), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the span for extra large screens (≥1200px).
    /// </summary>
    public int XlSpan
    {
        get => (int)GetValue(XlSpanProperty);
        set => SetValue(XlSpanProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="XlSpan"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XlSpanProperty =
        DependencyProperty.Register(nameof(XlSpan), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the offset for extra large screens (≥1200px).
    /// </summary>
    public int XlOffset
    {
        get => (int)GetValue(XlOffsetProperty);
        set => SetValue(XlOffsetProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="XlOffset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XlOffsetProperty =
        DependencyProperty.Register(nameof(XlOffset), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the span for extra extra large screens (≥1600px).
    /// </summary>
    public int XxlSpan
    {
        get => (int)GetValue(XxlSpanProperty);
        set => SetValue(XxlSpanProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="XxlSpan"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XxlSpanProperty =
        DependencyProperty.Register(nameof(XxlSpan), typeof(int), typeof(Col),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the offset for extra extra large screens (≥1600px).
    /// </summary>
    public int XxlOffset
    {
        get => (int)GetValue(XxlOffsetProperty);
        set => SetValue(XxlOffsetProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="XxlOffset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XxlOffsetProperty =
        DependencyProperty.Register(nameof(XxlOffset), typeof(int), typeof(Col),
            new PropertyMetadata(0));
}