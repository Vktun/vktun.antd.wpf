using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

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
    private bool _isSynchronizingSelection;
    private FrameworkElement? _tabContentHost;
    private ItemsControl? _tabHeadersItemsControl;
    private Border? _activeIndicator;
    private TranslateTransform? _activeIndicatorTransform;
    private double _indicatorX = double.NaN;
    private double _indicatorWidth = double.NaN;

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
            new PropertyMetadata(null, OnItemsChanged));

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
            new PropertyMetadata(-1, OnSelectedIndexChanged));

    /// <summary>
    /// Identifies the <see cref="SelectedItem"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(TabPane), typeof(Tabs),
            new PropertyMetadata(null));

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
        SelectTabCommand = new RelayCommand(
            parameter => SelectTab(parameter as TabPane),
            parameter => parameter is TabPane pane && !pane.IsDisabled);
        SizeChanged += OnTabsSizeChanged;
        Loaded += OnTabsLoaded;
    }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
        if (_tabHeadersItemsControl is not null)
        {
            _tabHeadersItemsControl.LayoutUpdated -= OnTabHeadersLayoutUpdated;
        }

        base.OnApplyTemplate();

        _tabContentHost = GetTemplateChild("PART_TabContent") as FrameworkElement;
        _tabHeadersItemsControl = GetTemplateChild("PART_TabHeadersItemsControl") as ItemsControl;
        _activeIndicator = GetTemplateChild("PART_ActiveIndicator") as Border;
        _activeIndicatorTransform = _activeIndicator is null
            ? null
            : EnsureMutableTranslateTransform(_activeIndicator);

        if (_tabHeadersItemsControl is not null)
        {
            _tabHeadersItemsControl.LayoutUpdated += OnTabHeadersLayoutUpdated;
        }

        Dispatcher.BeginInvoke(new Action(() => UpdateActiveIndicator(false)), DispatcherPriority.Loaded);
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
    /// Gets the currently selected tab pane.
    /// </summary>
    public TabPane? SelectedItem
    {
        get => (TabPane?)GetValue(SelectedItemProperty);
        private set => SetValue(SelectedItemProperty, value);
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
    /// Gets the command used by tab headers to switch selection.
    /// </summary>
    public ICommand SelectTabCommand { get; }

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

    private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var tabs = (Tabs)d;

        if (e.OldValue is ObservableCollection<TabPane> oldItems)
        {
            oldItems.CollectionChanged -= tabs.OnItemsCollectionChanged;
        }

        if (e.NewValue is ObservableCollection<TabPane> newItems)
        {
            newItems.CollectionChanged += tabs.OnItemsCollectionChanged;
        }

        tabs.CoerceSelectionAfterItemsChanged();
    }

    private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        CoerceSelectionAfterItemsChanged();
    }

    private void UpdateSelectionByKey(string? key)
    {
        if (_isSynchronizingSelection)
        {
            return;
        }

        if (Items == null || Items.Count == 0)
        {
            ClearSelectionState();
            return;
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            CoerceSelectionAfterItemsChanged();
            return;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Key == key)
            {
                SelectByIndex(i, true);
                return;
            }
        }
    }

    private void UpdateSelectionByIndex(int index)
    {
        if (_isSynchronizingSelection)
        {
            return;
        }

        if (Items == null || Items.Count == 0)
        {
            ClearSelectionState();
            return;
        }

        if (index < 0 || index >= Items.Count)
        {
            CoerceSelectionAfterItemsChanged();
            return;
        }

        SelectByIndex(index, true);
    }

    private void SelectTab(TabPane? pane)
    {
        if (pane is null || pane.IsDisabled || Items == null)
        {
            return;
        }

        var index = Items.IndexOf(pane);
        if (index < 0)
        {
            return;
        }

        SelectByIndex(index, true);
    }

    private void SelectByIndex(int index, bool raiseEvent)
    {
        if (Items == null || index < 0 || index >= Items.Count)
        {
            return;
        }

        if (Items[index].IsDisabled)
        {
            var fallbackIndex = FindFirstEnabledIndex();
            if (fallbackIndex < 0)
            {
                ClearSelectionState();
                return;
            }

            index = fallbackIndex;
        }

        var selectedPane = Items[index];
        var changed = !ReferenceEquals(SelectedItem, selectedPane);

        _isSynchronizingSelection = true;
        try
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].IsSelected = i == index;
            }

            if (SelectedIndex != index)
            {
                SetCurrentValue(SelectedIndexProperty, index);
            }

            if (!Equals(SelectedKey, selectedPane.Key))
            {
                SetCurrentValue(SelectedKeyProperty, selectedPane.Key);
            }

            if (!ReferenceEquals(SelectedItem, selectedPane))
            {
                SetCurrentValue(SelectedItemProperty, selectedPane);
            }
        }
        finally
        {
            _isSynchronizingSelection = false;
        }

        if (raiseEvent && changed)
        {
            StartContentTransition();
            UpdateActiveIndicator(true);
            RaiseEvent(new TabSelectedRoutedEventArgs(TabSelectedEvent, this, selectedPane, index));
            return;
        }

        UpdateActiveIndicator(false);
    }

    private void StartContentTransition()
    {
        if (!Animated || _tabContentHost is null)
        {
            return;
        }

        var translateTransform = EnsureMutableTranslateTransform(_tabContentHost);

        var duration = TimeSpan.FromMilliseconds(220);
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };

        _tabContentHost.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, duration)
        {
            EasingFunction = easing,
        });

        translateTransform.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(6, 0, duration)
        {
            EasingFunction = easing,
        });
    }

    private void CoerceSelectionAfterItemsChanged()
    {
        if (Items == null || Items.Count == 0)
        {
            ClearSelectionState();
            return;
        }

        if (SelectedItem is not null)
        {
            var currentItemIndex = Items.IndexOf(SelectedItem);
            if (currentItemIndex >= 0 && !Items[currentItemIndex].IsDisabled)
            {
                SelectByIndex(currentItemIndex, false);
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(SelectedKey))
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Key == SelectedKey && !Items[i].IsDisabled)
                {
                    SelectByIndex(i, false);
                    return;
                }
            }
        }

        if (SelectedIndex >= 0 && SelectedIndex < Items.Count && !Items[SelectedIndex].IsDisabled)
        {
            SelectByIndex(SelectedIndex, false);
            return;
        }

        var firstEnabledIndex = FindFirstEnabledIndex();
        if (firstEnabledIndex >= 0)
        {
            SelectByIndex(firstEnabledIndex, false);
            return;
        }

        ClearSelectionState();
    }

    private int FindFirstEnabledIndex()
    {
        if (Items == null)
        {
            return -1;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            if (!Items[i].IsDisabled)
            {
                return i;
            }
        }

        return -1;
    }

    private void ClearSelectionState()
    {
        if (Items is not null)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].IsSelected = false;
            }
        }

        _isSynchronizingSelection = true;
        try
        {
            if (SelectedIndex != -1)
            {
                SetCurrentValue(SelectedIndexProperty, -1);
            }

            if (SelectedKey is not null)
            {
                SetCurrentValue(SelectedKeyProperty, null);
            }

            if (SelectedItem is not null)
            {
                SetCurrentValue(SelectedItemProperty, null);
            }
        }
        finally
        {
            _isSynchronizingSelection = false;
        }

        ResetActiveIndicator();
    }

    private void OnTabsLoaded(object sender, RoutedEventArgs e)
    {
        UpdateActiveIndicator(false);
    }

    private void OnTabsSizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateActiveIndicator(false);
    }

    private void OnTabHeadersLayoutUpdated(object? sender, EventArgs e)
    {
        UpdateActiveIndicator(false);
    }

    private void UpdateActiveIndicator(bool animate)
    {
        if (_activeIndicator is null || _activeIndicatorTransform is null || _tabHeadersItemsControl is null)
        {
            return;
        }

        _activeIndicatorTransform = EnsureMutableTranslateTransform(_activeIndicator);

        if (SelectedItem is null)
        {
            ResetActiveIndicator();
            return;
        }

        var selectedButton = FindHeaderButtonForPane(SelectedItem);
        if (selectedButton is null || selectedButton.ActualWidth <= 0 || selectedButton.ActualHeight <= 0)
        {
            return;
        }

        if (_activeIndicator.Parent is not Visual indicatorHost)
        {
            return;
        }

        var targetRect = selectedButton.TransformToAncestor(indicatorHost)
            .TransformBounds(new Rect(0, 0, selectedButton.ActualWidth, selectedButton.ActualHeight));
        var targetWidth = Math.Max(20, targetRect.Width - 12);
        var targetX = targetRect.Left + (targetRect.Width - targetWidth) / 2;

        if (!Animated || !animate || double.IsNaN(_indicatorX) || double.IsNaN(_indicatorWidth))
        {
            _activeIndicator.BeginAnimation(FrameworkElement.WidthProperty, null);
            _activeIndicatorTransform.BeginAnimation(TranslateTransform.XProperty, null);
            _activeIndicator.Width = targetWidth;
            _activeIndicatorTransform.X = targetX;
            _activeIndicator.Opacity = 0.95;
            _indicatorX = targetX;
            _indicatorWidth = targetWidth;
            return;
        }

        if (Math.Abs(_indicatorX - targetX) < 0.5 && Math.Abs(_indicatorWidth - targetWidth) < 0.5)
        {
            return;
        }

        var duration = TimeSpan.FromMilliseconds(260);
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        _activeIndicator.Opacity = 0.95;

        _activeIndicator.BeginAnimation(FrameworkElement.WidthProperty, new DoubleAnimation(_indicatorWidth, targetWidth, duration)
        {
            EasingFunction = easing,
        });

        _activeIndicatorTransform.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(_indicatorX, targetX, duration)
        {
            EasingFunction = easing,
        });

        _indicatorX = targetX;
        _indicatorWidth = targetWidth;
    }

    private void ResetActiveIndicator()
    {
        if (_activeIndicator is null || _activeIndicatorTransform is null)
        {
            return;
        }

        _activeIndicatorTransform = EnsureMutableTranslateTransform(_activeIndicator);

        _activeIndicator.BeginAnimation(FrameworkElement.WidthProperty, null);
        _activeIndicatorTransform.BeginAnimation(TranslateTransform.XProperty, null);
        _activeIndicator.Width = 0;
        _activeIndicatorTransform.X = 0;
        _activeIndicator.Opacity = 0;
        _indicatorX = double.NaN;
        _indicatorWidth = double.NaN;
    }

    private static TranslateTransform EnsureMutableTranslateTransform(FrameworkElement element)
    {
        if (element.RenderTransform is TranslateTransform { IsFrozen: false } transform)
        {
            return transform;
        }

        var mutableTransform = element.RenderTransform is TranslateTransform frozenTransform
            ? frozenTransform.CloneCurrentValue()
            : new TranslateTransform();
        element.RenderTransform = mutableTransform;
        return mutableTransform;
    }

    private Button? FindHeaderButtonForPane(TabPane pane)
    {
        return FindVisualChildByDataContext<Button>(_tabHeadersItemsControl, pane);
    }

    private static T? FindVisualChildByDataContext<T>(DependencyObject? root, object dataContext)
        where T : FrameworkElement
    {
        if (root is null)
        {
            return null;
        }

        var childCount = VisualTreeHelper.GetChildrenCount(root);
        for (var i = 0; i < childCount; i++)
        {
            var child = VisualTreeHelper.GetChild(root, i);
            if (child is T match && ReferenceEquals(match.DataContext, dataContext))
            {
                return match;
            }

            var nested = FindVisualChildByDataContext<T>(child, dataContext);
            if (nested is not null)
            {
                return nested;
            }
        }

        return null;
    }

    private sealed class RelayCommand(Action<object?> execute, Predicate<object?> canExecute) : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
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
