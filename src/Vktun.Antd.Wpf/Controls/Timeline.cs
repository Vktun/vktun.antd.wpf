using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a single item in a timeline.
/// </summary>
public class TimelineItem : Control
{
    static TimelineItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineItem),
            new FrameworkPropertyMetadata(typeof(TimelineItem)));
    }

    /// <summary>
    /// Identifies the <see cref="Color"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(AntdTimelineColor), typeof(TimelineItem),
            new PropertyMetadata(AntdTimelineColor.Blue));

    /// <summary>
    /// Identifies the <see cref="Dot"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DotProperty =
        DependencyProperty.Register(nameof(Dot), typeof(object), typeof(TimelineItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Label"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(object), typeof(TimelineItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Content"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(TimelineItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Position"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PositionProperty =
        DependencyProperty.Register(nameof(Position), typeof(AntdTimelinePosition), typeof(TimelineItem),
            new PropertyMetadata(AntdTimelinePosition.Left));

    /// <summary>
    /// Identifies the <see cref="IsLast"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsLastProperty =
        DependencyProperty.Register(nameof(IsLast), typeof(bool), typeof(TimelineItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the timeline item color.
    /// </summary>
    public AntdTimelineColor Color
    {
        get => (AntdTimelineColor)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Gets or sets custom dot content.
    /// </summary>
    public object? Dot
    {
        get => GetValue(DotProperty);
        set => SetValue(DotProperty, value);
    }

    /// <summary>
    /// Gets or sets the label content (time/date).
    /// </summary>
    public object? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the position of the label.
    /// </summary>
    public AntdTimelinePosition Position
    {
        get => (AntdTimelinePosition)GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this is the last item.
    /// </summary>
    public bool IsLast
    {
        get => (bool)GetValue(IsLastProperty);
        set => SetValue(IsLastProperty, value);
    }
}

/// <summary>
/// Represents a timeline component for displaying a series of events.
/// </summary>
public class Timeline : ItemsControl
{
    static Timeline()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Timeline),
            new FrameworkPropertyMetadata(typeof(Timeline)));
    }

    /// <summary>
    /// Identifies the <see cref="Mode"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register(nameof(Mode), typeof(AntdTimelineMode), typeof(Timeline),
            new PropertyMetadata(AntdTimelineMode.Left));

    /// <summary>
    /// Identifies the <see cref="Pending"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PendingProperty =
        DependencyProperty.Register(nameof(Pending), typeof(object), typeof(Timeline),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="PendingDot"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PendingDotProperty =
        DependencyProperty.Register(nameof(PendingDot), typeof(object), typeof(Timeline),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Reverse"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ReverseProperty =
        DependencyProperty.Register(nameof(Reverse), typeof(bool), typeof(Timeline),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the timeline mode (left/alternate/right).
    /// </summary>
    public AntdTimelineMode Mode
    {
        get => (AntdTimelineMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    /// <summary>
    /// Gets or sets the pending content (ghost node at the end).
    /// </summary>
    public object? Pending
    {
        get => GetValue(PendingProperty);
        set => SetValue(PendingProperty, value);
    }

    /// <summary>
    /// Gets or sets the pending dot.
    /// </summary>
    public object? PendingDot
    {
        get => GetValue(PendingDotProperty);
        set => SetValue(PendingDotProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the timeline is reversed.
    /// </summary>
    public bool Reverse
    {
        get => (bool)GetValue(ReverseProperty);
        set => SetValue(ReverseProperty, value);
    }

    /// <summary>
    /// Updates the IsLast property for items.
    /// </summary>
    public void UpdateLastItem()
    {
        var items = Items.OfType<TimelineItem>().ToList();
        for (int i = 0; i < items.Count; i++)
        {
            items[i].IsLast = i == items.Count - 1;
        }
    }

    protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
        UpdateLastItem();
    }
}

/// <summary>
/// Attached properties for Timeline component.
/// </summary>
public static class TimelineAssist
{
    /// <summary>
    /// Identifies the <see cref="GetLineSize"/> attached property.
    /// </summary>
    public static readonly DependencyProperty LineSizeProperty =
        DependencyProperty.RegisterAttached("LineSize", typeof(double), typeof(TimelineAssist),
            new PropertyMetadata(2.0));

    /// <summary>
    /// Gets the line size.
    /// </summary>
    public static double GetLineSize(DependencyObject obj)
    {
        return (double)obj.GetValue(LineSizeProperty);
    }

    /// <summary>
    /// Sets the line size.
    /// </summary>
    public static void SetLineSize(DependencyObject obj, double value)
    {
        obj.SetValue(LineSizeProperty, value);
    }
}