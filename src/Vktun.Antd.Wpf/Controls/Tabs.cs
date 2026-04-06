using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a single tab page content container.
/// </summary>
public class TabPane : HeaderedContentControl
{
    static TabPane()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TabPane),
            new FrameworkPropertyMetadata(typeof(TabPane)));
    }

    /// <summary>
    /// Identifies the <see cref="Key"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(string), typeof(TabPane),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(TabPane),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="IsDisabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDisabledProperty =
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(TabPane),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsClosable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsClosableProperty =
        DependencyProperty.Register(nameof(IsClosable), typeof(bool), typeof(TabPane),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsSelected"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(TabPane),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the unique key for this tab.
    /// </summary>
    public string? Key
    {
        get => (string?)GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
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
    /// Gets or sets whether this tab is disabled.
    /// </summary>
    public bool IsDisabled
    {
        get => (bool)GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this tab is closable.
    /// </summary>
    public bool IsClosable
    {
        get => (bool)GetValue(IsClosableProperty);
        set => SetValue(IsClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this tab is currently selected.
    /// </summary>
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
}

/// <summary>
/// Represents a tabs component for organizing content in multiple pages.
/// </summary>
public class Tabs : Control
{
    static Tabs()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Tabs),
            new FrameworkPropertyMetadata(typeof(Tabs)));
    }

    /// <summary>
    /// Identifies the <see cref="Items"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<TabPane>), typeof(Tabs),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="SelectedKey"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedKeyProperty =
        DependencyProperty.Register(nameof(SelectedKey), typeof(string), typeof(Tabs),
            new PropertyMetadata(null, OnSelectedKeyChanged));

    /// <summary>
    /// Identifies the <see cref="SelectedIndex"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(Tabs),
            new PropertyMetadata(0, OnSelectedIndexChanged));

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdTabsType), typeof(Tabs),
            new PropertyMetadata(AntdTabsType.Line));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(Tabs),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Position"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PositionProperty =
        DependencyProperty.Register(nameof(Position), typeof(AntdTabsPosition), typeof(Tabs),
            new PropertyMetadata(AntdTabsPosition.Top));

    /// <summary>
    /// Identifies the <see cref="Animated"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AnimatedProperty =
        DependencyProperty.Register(nameof(Animated), typeof(bool), typeof(Tabs),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="HideAdd"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HideAddProperty =
        DependencyProperty.Register(nameof(HideAdd), typeof(bool), typeof(Tabs),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Centered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CenteredProperty =
        DependencyProperty.Register(nameof(Centered), typeof(bool), typeof(Tabs),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="AddIcon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AddIconProperty =
        DependencyProperty.Register(nameof(AddIcon), typeof(object), typeof(Tabs),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Extra"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ExtraProperty =
        DependencyProperty.Register(nameof(Extra), typeof(object), typeof(Tabs),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="TabBarGutter"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TabBarGutterProperty =
        DependencyProperty.Register(nameof(TabBarGutter), typeof(double), typeof(Tabs),
            new PropertyMetadata(32.0));

    /// <summary>
    /// Identifies the <see cref="TabBarStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TabBarStyleProperty =
        DependencyProperty.Register(nameof(TabBarStyle), typeof(Style), typeof(Tabs),
            new PropertyMetadata(null));

    /// <summary>
    /// Routed event for when a tab is added.
    /// </summary>
    public static readonly RoutedEvent TabAddedEvent =
        EventManager.RegisterRoutedEvent("TabAdded", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(Tabs));

    /// <summary>
    /// Routed event for when a tab is closed.
    /// </summary>
    public static readonly RoutedEvent TabClosedEvent =
        EventManager.RegisterRoutedEvent("TabClosed", RoutingStrategy.Bubble,
            typeof(TabClosedRoutedEventHandler), typeof(Tabs));

    /// <summary>
    /// Routed event for when a tab is selected.
    /// </summary>
    public static readonly RoutedEvent TabSelectedEvent =
        EventManager.RegisterRoutedEvent("TabSelected", RoutingStrategy.Bubble,
            typeof(TabSelectedRoutedEventHandler), typeof(Tabs));

    public Tabs()
    {
        Items = new ObservableCollection<TabPane>();
    }

    /// <summary>
    /// Gets or sets the collection of tab panes.
    /// </summary>
    public ObservableCollection<TabPane> Items
    {
        get => (ObservableCollection<TabPane>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    /// <summary>
    /// Gets or sets the key of the selected tab.
    /// </summary>
    public string? SelectedKey
    {
        get => (string?)GetValue(SelectedKeyProperty);
        set => SetValue(SelectedKeyProperty, value);
    }

    /// <summary>
    /// Gets or sets the index of the selected tab.
    /// </summary>
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    /// <summary>
    /// Gets or sets the tabs type style.
    /// </summary>
    public AntdTabsType Type
    {
        get => (AntdTabsType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the tabs size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the position of the tab bar.
    /// </summary>
    public AntdTabsPosition Position
    {
        get => (AntdTabsPosition)GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }

    /// <summary>
    /// Gets or sets whether tabs content switching is animated.
    /// </summary>
    public bool Animated
    {
        get => (bool)GetValue(AnimatedProperty);
        set => SetValue(AnimatedProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the add button is hidden.
    /// </summary>
    public bool HideAdd
    {
        get => (bool)GetValue(HideAddProperty);
        set => SetValue(HideAddProperty, value);
    }

    /// <summary>
    /// Gets or sets whether tabs are centered.
    /// </summary>
    public bool Centered
    {
        get => (bool)GetValue(CenteredProperty);
        set => SetValue(CenteredProperty, value);
    }

    /// <summary>
    /// Gets or sets the add button icon.
    /// </summary>
    public object? AddIcon
    {
        get => GetValue(AddIconProperty);
        set => SetValue(AddIconProperty, value);
    }

    /// <summary>
    /// Gets or sets extra content in the tab bar.
    /// </summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>
    /// Gets or sets the gutter between tabs.
    /// </summary>
    public double TabBarGutter
    {
        get => (double)GetValue(TabBarGutterProperty);
        set => SetValue(TabBarGutterProperty, value);
    }

    /// <summary>
    /// Gets or sets the tab bar style.
    /// </summary>
    public Style? TabBarStyle
    {
        get => (Style?)GetValue(TabBarStyleProperty);
        set => SetValue(TabBarStyleProperty, value);
    }

    /// <summary>
    /// Occurs when a new tab is added.
    /// </summary>
    public event RoutedEventHandler TabAdded
    {
        add => AddHandler(TabAddedEvent, value);
        remove => RemoveHandler(TabAddedEvent, value);
    }

    /// <summary>
    /// Occurs when a tab is closed.
    /// </summary>
    public event TabClosedRoutedEventHandler TabClosed
    {
        add => AddHandler(TabClosedEvent, value);
        remove => RemoveHandler(TabClosedEvent, value);
    }

    /// <summary>
    /// Occurs when a tab is selected.
    /// </summary>
    public event TabSelectedRoutedEventHandler TabSelected
    {
        add => AddHandler(TabSelectedEvent, value);
        remove => RemoveHandler(TabSelectedEvent, value);
    }

    private static void OnSelectedKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var tabs = (Tabs)d;
        tabs.UpdateSelectionByKey((string?)e.NewValue);
    }

    private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var tabs = (Tabs)d;
        tabs.UpdateSelectionByIndex((int)e.NewValue);
    }

    private void UpdateSelectionByKey(string? key)
    {
        if (Items == null || key == null) return;

        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].IsSelected = Items[i].Key == key;
            if (Items[i].IsSelected)
            {
                SelectedIndex = i;
            }
        }
    }

    private void UpdateSelectionByIndex(int index)
    {
        if (Items == null || index < 0 || index >= Items.Count) return;

        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].IsSelected = i == index;
        }

        SelectedKey = Items[index].Key;
    }
}

/// <summary>
/// Delegate for handling tab closed events.
/// </summary>
public delegate void TabClosedRoutedEventHandler(object sender, TabClosedRoutedEventArgs e);

/// <summary>
/// Delegate for handling tab selected events.
/// </summary>
public delegate void TabSelectedRoutedEventHandler(object sender, TabSelectedRoutedEventArgs e);

/// <summary>
/// Routed event arguments for tab closed events.
/// </summary>
public class TabClosedRoutedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// Gets the closed tab pane.
    /// </summary>
    public TabPane? ClosedPane { get; }

    /// <summary>
    /// Gets the key of the closed tab.
    /// </summary>
    public string? ClosedKey { get; }

    public TabClosedRoutedEventArgs(RoutedEvent routedEvent, object source, TabPane? pane)
        : base(routedEvent, source)
    {
        ClosedPane = pane;
        ClosedKey = pane?.Key;
    }
}

/// <summary>
/// Routed event arguments for tab selected events.
/// </summary>
public class TabSelectedRoutedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// Gets the selected tab pane.
    /// </summary>
    public TabPane? SelectedPane { get; }

    /// <summary>
    /// Gets the key of the selected tab.
    /// </summary>
    public string? SelectedKey { get; }

    /// <summary>
    /// Gets the index of the selected tab.
    /// </summary>
    public int SelectedIndex { get; }

    public TabSelectedRoutedEventArgs(RoutedEvent routedEvent, object source, TabPane? pane, int index)
        : base(routedEvent, source)
    {
        SelectedPane = pane;
        SelectedKey = pane?.Key;
        SelectedIndex = index;
    }
}

/// <summary>
/// Attached properties for Tabs component.
/// </summary>
public static class TabsAssist
{
    /// <summary>
    /// Identifies the <see cref="GetCloseIcon"/> attached property.
    /// </summary>
    public static readonly DependencyProperty CloseIconProperty =
        DependencyProperty.RegisterAttached("CloseIcon", typeof(object), typeof(TabsAssist),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets the close icon.
    /// </summary>
    public static object? GetCloseIcon(DependencyObject obj)
    {
        return obj.GetValue(CloseIconProperty);
    }

    /// <summary>
    /// Sets the close icon.
    /// </summary>
    public static void SetCloseIcon(DependencyObject obj, object? value)
    {
        obj.SetValue(CloseIconProperty, value);
    }
}