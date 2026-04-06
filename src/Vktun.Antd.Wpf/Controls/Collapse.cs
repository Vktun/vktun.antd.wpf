using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a collapsible panel item.
/// </summary>
public class CollapsePanel : HeaderedContentControl
{
    static CollapsePanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CollapsePanel),
            new FrameworkPropertyMetadata(typeof(CollapsePanel)));
    }

    /// <summary>
    /// Identifies the <see cref="Key"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(string), typeof(CollapsePanel),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="IsExpanded"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(CollapsePanel),
            new PropertyMetadata(false, OnIsExpandedChanged));

    /// <summary>
    /// Identifies the <see cref="Extra"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ExtraProperty =
        DependencyProperty.Register(nameof(Extra), typeof(object), typeof(CollapsePanel),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Disabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DisabledProperty =
        DependencyProperty.Register(nameof(Disabled), typeof(bool), typeof(CollapsePanel),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the unique key.
    /// </summary>
    public string? Key
    {
        get => (string?)GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the panel is expanded.
    /// </summary>
    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    /// <summary>
    /// Gets or sets the extra content in the header.
    /// </summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the panel is disabled.
    /// </summary>
    public bool Disabled
    {
        get => (bool)GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var panel = (CollapsePanel)d;
        panel.RaiseEvent(new RoutedEventArgs(IsExpandedChangedEvent, panel));
    }

    /// <summary>
    /// Identifies the IsExpandedChanged routed event.
    /// </summary>
    public static readonly RoutedEvent IsExpandedChangedEvent =
        EventManager.RegisterRoutedEvent(nameof(IsExpandedChanged), RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(CollapsePanel));

    /// <summary>
    /// Occurs when the expanded state changes.
    /// </summary>
    public event RoutedEventHandler IsExpandedChanged
    {
        add => AddHandler(IsExpandedChangedEvent, value);
        remove => RemoveHandler(IsExpandedChangedEvent, value);
    }

    /// <summary>
    /// Toggles the expanded state.
    /// </summary>
    public void Toggle()
    {
        if (!Disabled)
        {
            IsExpanded = !IsExpanded;
        }
    }
}

/// <summary>
/// Represents a collapse/accordion container component.
/// </summary>
public class Collapse : ItemsControl
{
    static Collapse()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Collapse),
            new FrameworkPropertyMetadata(typeof(Collapse)));
    }

    /// <summary>
    /// Identifies the <see cref="Accordion"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AccordionProperty =
        DependencyProperty.Register(nameof(Accordion), typeof(bool), typeof(Collapse),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="ExpandIconPosition"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ExpandIconPositionProperty =
        DependencyProperty.Register(nameof(ExpandIconPosition), typeof(HorizontalAlignment), typeof(Collapse),
            new PropertyMetadata(HorizontalAlignment.Left));

    /// <summary>
    /// Identifies the <see cref="ActiveKeys"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ActiveKeysProperty =
        DependencyProperty.Register(nameof(ActiveKeys), typeof(IList), typeof(Collapse),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets whether accordion mode is enabled (only one panel open at a time).
    /// </summary>
    public bool Accordion
    {
        get => (bool)GetValue(AccordionProperty);
        set => SetValue(AccordionProperty, value);
    }

    /// <summary>
    /// Gets or sets the expand icon position.
    /// </summary>
    public HorizontalAlignment ExpandIconPosition
    {
        get => (HorizontalAlignment)GetValue(ExpandIconPositionProperty);
        set => SetValue(ExpandIconPositionProperty, value);
    }

    /// <summary>
    /// Gets or sets the active (expanded) panel keys.
    /// </summary>
    public IList? ActiveKeys
    {
        get => (IList?)GetValue(ActiveKeysProperty);
        set => SetValue(ActiveKeysProperty, value);
    }
}