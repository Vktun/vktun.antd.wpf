using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a tooltip that displays informative text when hovering over an element.
/// </summary>
public class Tooltip : ContentControl
{
    static Tooltip()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Tooltip),
            new FrameworkPropertyMetadata(typeof(Tooltip)));
    }

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(object), typeof(Tooltip),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Placement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(Tooltip),
            new PropertyMetadata(PlacementMode.Top));

    /// <summary>
    /// Identifies the <see cref="IsOpen"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Tooltip),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Visible"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty VisibleProperty =
        DependencyProperty.Register(nameof(Visible), typeof(bool), typeof(Tooltip),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="Arrow"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ArrowProperty =
        DependencyProperty.Register(nameof(Arrow), typeof(bool), typeof(Tooltip),
            new PropertyMetadata(true, OnArrowChanged));

    /// <summary>
    /// Identifies the <see cref="ArrowPointAtCenter"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ArrowPointAtCenterProperty =
        DependencyProperty.Register(nameof(ArrowPointAtCenter), typeof(bool), typeof(Tooltip),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="MouseEnterDelay"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MouseEnterDelayProperty =
        DependencyProperty.Register(nameof(MouseEnterDelay), typeof(int), typeof(Tooltip),
            new PropertyMetadata(100));

    /// <summary>
    /// Identifies the <see cref="MouseLeaveDelay"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MouseLeaveDelayProperty =
        DependencyProperty.Register(nameof(MouseLeaveDelay), typeof(int), typeof(Tooltip),
            new PropertyMetadata(100));

    /// <summary>
    /// Identifies the <see cref="OverlayStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayStyleProperty =
        DependencyProperty.Register(nameof(OverlayStyle), typeof(Style), typeof(Tooltip),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="OverlayInnerStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayInnerStyleProperty =
        DependencyProperty.Register(nameof(OverlayInnerStyle), typeof(Style), typeof(Tooltip),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="ZIndex"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ZIndexProperty =
        DependencyProperty.Register(nameof(ZIndex), typeof(int), typeof(Tooltip),
            new PropertyMetadata(1060));

    /// <summary>
    /// Identifies the <see cref="Color"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(AntdTooltipColor), typeof(Tooltip),
            new PropertyMetadata(AntdTooltipColor.Default));

    /// <summary>
    /// Routed event for when the tooltip visibility changes.
    /// </summary>
    public static readonly RoutedEvent VisibleChangeEvent =
        EventManager.RegisterRoutedEvent("VisibleChange", RoutingStrategy.Bubble,
            typeof(TooltipVisibleChangeRoutedEventHandler), typeof(Tooltip));

    /// <summary>
    /// Gets or sets the tooltip title/content.
    /// </summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the placement of the tooltip.
    /// </summary>
    public PlacementMode Placement
    {
        get => (PlacementMode)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the tooltip is visible.
    /// </summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the tooltip is initially visible.
    /// </summary>
    public bool Visible
    {
        get => (bool)GetValue(VisibleProperty);
        set => SetValue(VisibleProperty, value);
    }

    /// <summary>
    /// Gets or sets whether an arrow is shown.
    /// </summary>
    public bool Arrow
    {
        get => (bool)GetValue(ArrowProperty);
        set => SetValue(ArrowProperty, value);
    }

    /// <summary>
    /// Gets or sets whether arrow points to center.
    /// </summary>
    public bool ArrowPointAtCenter
    {
        get => (bool)GetValue(ArrowPointAtCenterProperty);
        set => SetValue(ArrowPointAtCenterProperty, value);
    }

    /// <summary>
    /// Gets or sets mouse enter delay in milliseconds.
    /// </summary>
    public int MouseEnterDelay
    {
        get => (int)GetValue(MouseEnterDelayProperty);
        set => SetValue(MouseEnterDelayProperty, value);
    }

    /// <summary>
    /// Gets or sets mouse leave delay in milliseconds.
    /// </summary>
    public int MouseLeaveDelay
    {
        get => (int)GetValue(MouseLeaveDelayProperty);
        set => SetValue(MouseLeaveDelayProperty, value);
    }

    /// <summary>
    /// Gets or sets the overlay style.
    /// </summary>
    public Style? OverlayStyle
    {
        get => (Style?)GetValue(OverlayStyleProperty);
        set => SetValue(OverlayStyleProperty, value);
    }

    /// <summary>
    /// Gets or sets the overlay inner style.
    /// </summary>
    public Style? OverlayInnerStyle
    {
        get => (Style?)GetValue(OverlayInnerStyleProperty);
        set => SetValue(OverlayInnerStyleProperty, value);
    }

    /// <summary>
    /// Gets or sets the z-index.
    /// </summary>
    public int ZIndex
    {
        get => (int)GetValue(ZIndexProperty);
        set => SetValue(ZIndexProperty, value);
    }

    /// <summary>
    /// Gets or sets the tooltip color.
    /// </summary>
    public AntdTooltipColor Color
    {
        get => (AntdTooltipColor)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Occurs when tooltip visibility changes.
    /// </summary>
    public event TooltipVisibleChangeRoutedEventHandler VisibleChange
    {
        add => AddHandler(VisibleChangeEvent, value);
        remove => RemoveHandler(VisibleChangeEvent, value);
    }

    private static void OnArrowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Arrow visibility is handled in template
    }
}

/// <summary>
/// Represents a popover that displays rich content when clicking or hovering.
/// </summary>
public class Popover : Tooltip
{
    static Popover()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Popover),
            new FrameworkPropertyMetadata(typeof(Popover)));
    }

    /// <summary>
    /// Identifies the <see cref="PopoverContent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PopoverContentProperty =
        DependencyProperty.Register(nameof(PopoverContent), typeof(object), typeof(Popover),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Trigger"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TriggerProperty =
        DependencyProperty.Register(nameof(Trigger), typeof(AntdPopoverTrigger), typeof(Popover),
            new PropertyMetadata(AntdPopoverTrigger.Hover));

    /// <summary>
    /// Identifies the <see cref="ShowArrow"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowArrowProperty =
        DependencyProperty.Register(nameof(ShowArrow), typeof(bool), typeof(Popover),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="OverlayMinWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayMinWidthProperty =
        DependencyProperty.Register(nameof(OverlayMinWidth), typeof(double), typeof(Popover),
            new PropertyMetadata(177.0));

    /// <summary>
    /// Identifies the <see cref="OverlayMaxWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayMaxWidthProperty =
        DependencyProperty.Register(nameof(OverlayMaxWidth), typeof(double), typeof(Popover),
            new PropertyMetadata(400.0));

    /// <summary>
    /// Gets or sets the popover content.
    /// </summary>
    public object? PopoverContent
    {
        get => GetValue(PopoverContentProperty);
        set => SetValue(PopoverContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the trigger mode.
    /// </summary>
    public AntdPopoverTrigger Trigger
    {
        get => (AntdPopoverTrigger)GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show arrow.
    /// </summary>
    public bool ShowArrow
    {
        get => (bool)GetValue(ShowArrowProperty);
        set => SetValue(ShowArrowProperty, value);
    }

    /// <summary>
    /// Gets or sets the overlay minimum width.
    /// </summary>
    public double OverlayMinWidth
    {
        get => (double)GetValue(OverlayMinWidthProperty);
        set => SetValue(OverlayMinWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the overlay maximum width.
    /// </summary>
    public double OverlayMaxWidth
    {
        get => (double)GetValue(OverlayMaxWidthProperty);
        set => SetValue(OverlayMaxWidthProperty, value);
    }
}

/// <summary>
/// Delegate for handling tooltip visible change events.
/// </summary>
public delegate void TooltipVisibleChangeRoutedEventHandler(object sender, TooltipVisibleChangeRoutedEventArgs e);

/// <summary>
/// Routed event arguments for tooltip visible change events.
/// </summary>
public class TooltipVisibleChangeRoutedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// Gets whether the tooltip is now visible.
    /// </summary>
    public bool Visible { get; }

    public TooltipVisibleChangeRoutedEventArgs(RoutedEvent routedEvent, object source, bool visible)
        : base(routedEvent, source)
    {
        Visible = visible;
    }
}

/// <summary>
/// Attached properties for Tooltip component.
/// </summary>
public static class TooltipAssist
{
    /// <summary>
    /// Identifies the <see cref="GetDestroyTooltipOnHide"/> attached property.
    /// </summary>
    public static readonly DependencyProperty DestroyTooltipOnHideProperty =
        DependencyProperty.RegisterAttached("DestroyTooltipOnHide", typeof(bool), typeof(TooltipAssist),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets whether tooltip is destroyed on hide.
    /// </summary>
    public static bool GetDestroyTooltipOnHide(DependencyObject obj)
    {
        return (bool)obj.GetValue(DestroyTooltipOnHideProperty);
    }

    /// <summary>
    /// Sets whether tooltip is destroyed on hide.
    /// </summary>
    public static void SetDestroyTooltipOnHide(DependencyObject obj, bool value)
    {
        obj.SetValue(DestroyTooltipOnHideProperty, value);
    }
}