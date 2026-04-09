using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace Vktun.Antd.Wpf;

public class DropdownItem : HeaderedContentControl
{
    static DropdownItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownItem),
            new FrameworkPropertyMetadata(typeof(DropdownItem)));
    }

    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(string), typeof(DropdownItem),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(DropdownItem),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IsDisabledProperty =
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(DropdownItem),
            new PropertyMetadata(false));

    public static readonly DependencyProperty IsDividerProperty =
        DependencyProperty.Register(nameof(IsDivider), typeof(bool), typeof(DropdownItem),
            new PropertyMetadata(false));

    public static readonly DependencyProperty DangerProperty =
        DependencyProperty.Register(nameof(Danger), typeof(bool), typeof(DropdownItem),
            new PropertyMetadata(false));

    public static readonly RoutedEvent ClickEvent =
        EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(DropdownItem));

    public string? Key
    {
        get => (string?)GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsDisabled
    {
        get => (bool)GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    public bool IsDivider
    {
        get => (bool)GetValue(IsDividerProperty);
        set => SetValue(IsDividerProperty, value);
    }

    public bool Danger
    {
        get => (bool)GetValue(DangerProperty);
        set => SetValue(DangerProperty, value);
    }

    public event RoutedEventHandler Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (IsDisabled || IsDivider)
        {
            e.Handled = true;
            return;
        }

        base.OnMouseLeftButtonDown(e);
        RaiseEvent(new RoutedEventArgs(ClickEvent, this));
    }
}

[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
public class Dropdown : ContentControl
{
    private Popup? _popup;
    private bool _isPopupClosing;
    private DispatcherTimer? _hoverCloseTimer;

    static Dropdown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Dropdown),
            new FrameworkPropertyMetadata(typeof(Dropdown)));
    }

    public static readonly DependencyProperty MenuProperty =
        DependencyProperty.Register(nameof(Menu), typeof(DropdownMenu), typeof(Dropdown),
            new PropertyMetadata(null, OnMenuChanged));

    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(Dropdown),
            new PropertyMetadata(PlacementMode.Bottom));

    public static readonly DependencyProperty TriggerProperty =
        DependencyProperty.Register(nameof(Trigger), typeof(AntdDropdownTrigger), typeof(Dropdown),
            new PropertyMetadata(AntdDropdownTrigger.Hover));

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Dropdown),
            new PropertyMetadata(false, OnIsOpenChanged));

    public static readonly DependencyProperty ArrowProperty =
        DependencyProperty.Register(nameof(Arrow), typeof(bool), typeof(Dropdown),
            new PropertyMetadata(false));

    public static readonly DependencyProperty DisabledProperty =
        DependencyProperty.Register(nameof(Disabled), typeof(bool), typeof(Dropdown),
            new PropertyMetadata(false));

    public static readonly DependencyProperty DestroyPopupOnHideProperty =
        DependencyProperty.Register(nameof(DestroyPopupOnHide), typeof(bool), typeof(Dropdown),
            new PropertyMetadata(true));

    public static readonly DependencyProperty GetPopupContainerProperty =
        DependencyProperty.Register(nameof(GetPopupContainer), typeof(FrameworkElement), typeof(Dropdown),
            new PropertyMetadata(null));

    public static readonly DependencyProperty OverlayStyleProperty =
        DependencyProperty.Register(nameof(OverlayStyle), typeof(Style), typeof(Dropdown),
            new PropertyMetadata(null));

    public static readonly DependencyProperty OverlayClassNameProperty =
        DependencyProperty.Register(nameof(OverlayClassName), typeof(string), typeof(Dropdown),
            new PropertyMetadata(null));

    public static readonly RoutedEvent VisibleChangeEvent =
        EventManager.RegisterRoutedEvent("VisibleChange", RoutingStrategy.Bubble,
            typeof(DropdownVisibleChangeRoutedEventHandler), typeof(Dropdown));

    public static readonly RoutedEvent ItemClickEvent =
        EventManager.RegisterRoutedEvent("ItemClick", RoutingStrategy.Bubble,
            typeof(DropdownItemClickRoutedEventHandler), typeof(Dropdown));

    public DropdownMenu? Menu
    {
        get => (DropdownMenu?)GetValue(MenuProperty);
        set => SetValue(MenuProperty, value);
    }

    public PlacementMode Placement
    {
        get => (PlacementMode)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    public AntdDropdownTrigger Trigger
    {
        get => (AntdDropdownTrigger)GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public bool Arrow
    {
        get => (bool)GetValue(ArrowProperty);
        set => SetValue(ArrowProperty, value);
    }

    public bool Disabled
    {
        get => (bool)GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    public bool DestroyPopupOnHide
    {
        get => (bool)GetValue(DestroyPopupOnHideProperty);
        set => SetValue(DestroyPopupOnHideProperty, value);
    }

    public FrameworkElement? GetPopupContainer
    {
        get => (FrameworkElement?)GetValue(GetPopupContainerProperty);
        set => SetValue(GetPopupContainerProperty, value);
    }

    public Style? OverlayStyle
    {
        get => (Style?)GetValue(OverlayStyleProperty);
        set => SetValue(OverlayStyleProperty, value);
    }

    public string? OverlayClassName
    {
        get => (string?)GetValue(OverlayClassNameProperty);
        set => SetValue(OverlayClassNameProperty, value);
    }

    public event DropdownVisibleChangeRoutedEventHandler VisibleChange
    {
        add => AddHandler(VisibleChangeEvent, value);
        remove => RemoveHandler(VisibleChangeEvent, value);
    }

    public event DropdownItemClickRoutedEventHandler ItemClick
    {
        add => AddHandler(ItemClickEvent, value);
        remove => RemoveHandler(ItemClickEvent, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _popup = GetTemplateChild("PART_Popup") as Popup;

        if (_popup != null)
        {
            _popup.Closed += OnPopupClosed;
        }
    }

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);

        if (Disabled) return;

        if (Trigger == AntdDropdownTrigger.Hover)
        {
            StopHoverCloseTimer();
            IsOpen = true;
        }
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);

        if (Trigger == AntdDropdownTrigger.Hover)
        {
            StartHoverCloseTimer();
        }
    }

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseLeftButtonDown(e);

        if (Disabled) return;

        if (Trigger == AntdDropdownTrigger.Click)
        {
            if (_isPopupClosing) return;
            e.Handled = true;
            var targetIsOpen = !IsOpen;
            Dispatcher.BeginInvoke(() =>
            {
                IsOpen = targetIsOpen;
            }, DispatcherPriority.Input);
        }
    }

    protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseRightButtonDown(e);

        if (Disabled) return;

        if (Trigger == AntdDropdownTrigger.ContextMenu)
        {
            e.Handled = true;
            var targetIsOpen = !IsOpen;
            Dispatcher.BeginInvoke(() =>
            {
                IsOpen = targetIsOpen;
            }, DispatcherPriority.Input);
        }
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        SetCurrentValue(IsOpenProperty, false);
        _isPopupClosing = true;
        Dispatcher.BeginInvoke(() => _isPopupClosing = false, DispatcherPriority.Background);
    }

    private static void OnMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var dropdown = (Dropdown)d;

        if (e.OldValue is DropdownMenu oldMenu)
        {
            oldMenu.RemoveHandler(DropdownItem.ClickEvent, new RoutedEventHandler(dropdown.OnDropdownItemClick));
            oldMenu.MouseEnter -= dropdown.OnPopupChildMouseEnter;
            oldMenu.MouseLeave -= dropdown.OnPopupChildMouseLeave;
        }

        if (e.NewValue is DropdownMenu newMenu)
        {
            newMenu.AddHandler(DropdownItem.ClickEvent, new RoutedEventHandler(dropdown.OnDropdownItemClick));
            newMenu.MouseEnter += dropdown.OnPopupChildMouseEnter;
            newMenu.MouseLeave += dropdown.OnPopupChildMouseLeave;
        }
    }

    private void OnPopupChildMouseEnter(object sender, MouseEventArgs e)
    {
        if (Trigger == AntdDropdownTrigger.Hover)
        {
            StopHoverCloseTimer();
        }
    }

    private void OnPopupChildMouseLeave(object sender, MouseEventArgs e)
    {
        if (Trigger == AntdDropdownTrigger.Hover)
        {
            StartHoverCloseTimer();
        }
    }

    private void StartHoverCloseTimer()
    {
        StopHoverCloseTimer();
        _hoverCloseTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(150) };
        _hoverCloseTimer.Tick += OnHoverCloseTimerTick;
        _hoverCloseTimer.Start();
    }

    private void StopHoverCloseTimer()
    {
        if (_hoverCloseTimer != null)
        {
            _hoverCloseTimer.Stop();
            _hoverCloseTimer.Tick -= OnHoverCloseTimerTick;
            _hoverCloseTimer = null;
        }
    }

    private void OnHoverCloseTimerTick(object? sender, EventArgs e)
    {
        StopHoverCloseTimer();
        IsOpen = false;
    }

    private void OnDropdownItemClick(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is DropdownItem item)
        {
            RaiseEvent(new DropdownItemClickRoutedEventArgs(ItemClickEvent, this, item));
            IsOpen = false;
        }
    }

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var dropdown = (Dropdown)d;
        var isOpen = (bool)e.NewValue;
        dropdown.RaiseEvent(new DropdownVisibleChangeRoutedEventArgs(VisibleChangeEvent, dropdown, isOpen));
    }

    public void Show()
    {
        IsOpen = true;
    }

    public void Hide()
    {
        IsOpen = false;
    }
}

public class DropdownMenu : ItemsControl
{
    static DropdownMenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownMenu),
            new FrameworkPropertyMetadata(typeof(DropdownMenu)));
    }

    public static readonly DependencyProperty SelectedKeyProperty =
        DependencyProperty.Register(nameof(SelectedKey), typeof(string), typeof(DropdownMenu),
            new PropertyMetadata(null));

    public static readonly DependencyProperty SelectableProperty =
        DependencyProperty.Register(nameof(Selectable), typeof(bool), typeof(DropdownMenu),
            new PropertyMetadata(false));

    public static readonly DependencyProperty MultipleProperty =
        DependencyProperty.Register(nameof(Multiple), typeof(bool), typeof(DropdownMenu),
            new PropertyMetadata(false));

    public string? SelectedKey
    {
        get => (string?)GetValue(SelectedKeyProperty);
        set => SetValue(SelectedKeyProperty, value);
    }

    public bool Selectable
    {
        get => (bool)GetValue(SelectableProperty);
        set => SetValue(SelectableProperty, value);
    }

    public bool Multiple
    {
        get => (bool)GetValue(MultipleProperty);
        set => SetValue(MultipleProperty, value);
    }
}

[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
public class DropdownButton : Button
{
    private Popup? _popup;
    private bool _isPopupClosing;
    private DispatcherTimer? _hoverCloseTimer;

    static DropdownButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownButton),
            new FrameworkPropertyMetadata(typeof(DropdownButton)));
    }

    public static readonly DependencyProperty MenuProperty =
        DependencyProperty.Register(nameof(Menu), typeof(DropdownMenu), typeof(DropdownButton),
            new PropertyMetadata(null, OnMenuChanged));

    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(DropdownButton),
            new PropertyMetadata(PlacementMode.Bottom));

    public static readonly DependencyProperty TriggerProperty =
        DependencyProperty.Register(nameof(Trigger), typeof(AntdDropdownTrigger), typeof(DropdownButton),
            new PropertyMetadata(AntdDropdownTrigger.Hover));

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(DropdownButton),
            new PropertyMetadata(false, OnIsOpenChanged));

    public new static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdButtonType), typeof(DropdownButton),
            new PropertyMetadata(AntdButtonType.Default));

    public new static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(DropdownButton),
            new PropertyMetadata(AntdControlSize.Middle));

    public static readonly DependencyProperty ArrowProperty =
        DependencyProperty.Register(nameof(Arrow), typeof(bool), typeof(DropdownButton),
            new PropertyMetadata(false));

    public static readonly DependencyProperty DisabledProperty =
        DependencyProperty.Register(nameof(Disabled), typeof(bool), typeof(DropdownButton),
            new PropertyMetadata(false));

    public new static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(DropdownButton),
            new PropertyMetadata(null));

    public static readonly RoutedEvent VisibleChangeEvent =
        EventManager.RegisterRoutedEvent("VisibleChange", RoutingStrategy.Bubble,
            typeof(DropdownVisibleChangeRoutedEventHandler), typeof(DropdownButton));

    public static readonly RoutedEvent ItemClickEvent =
        EventManager.RegisterRoutedEvent("ItemClick", RoutingStrategy.Bubble,
            typeof(DropdownItemClickRoutedEventHandler), typeof(DropdownButton));

    public DropdownMenu? Menu
    {
        get => (DropdownMenu?)GetValue(MenuProperty);
        set => SetValue(MenuProperty, value);
    }

    public PlacementMode Placement
    {
        get => (PlacementMode)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    public AntdDropdownTrigger Trigger
    {
        get => (AntdDropdownTrigger)GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public new AntdButtonType Type
    {
        get => (AntdButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public new AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public bool Arrow
    {
        get => (bool)GetValue(ArrowProperty);
        set => SetValue(ArrowProperty, value);
    }

    public bool Disabled
    {
        get => (bool)GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    public new object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public event DropdownVisibleChangeRoutedEventHandler VisibleChange
    {
        add => AddHandler(VisibleChangeEvent, value);
        remove => RemoveHandler(VisibleChangeEvent, value);
    }

    public event DropdownItemClickRoutedEventHandler ItemClick
    {
        add => AddHandler(ItemClickEvent, value);
        remove => RemoveHandler(ItemClickEvent, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _popup = GetTemplateChild("PART_Popup") as Popup;

        if (_popup != null)
        {
            _popup.Closed += OnPopupClosed;
        }
    }

    protected override void OnClick()
    {
        if (Disabled) return;

        if (Trigger == AntdDropdownTrigger.Click)
        {
            if (_isPopupClosing) return;
            IsOpen = !IsOpen;
        }
    }

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        base.OnMouseEnter(e);

        if (Disabled) return;

        if (Trigger == AntdDropdownTrigger.Hover)
        {
            StopHoverCloseTimer();
            IsOpen = true;
        }
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        base.OnMouseLeave(e);

        if (Trigger == AntdDropdownTrigger.Hover)
        {
            StartHoverCloseTimer();
        }
    }

    protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseRightButtonDown(e);

        if (Disabled) return;

        if (Trigger == AntdDropdownTrigger.ContextMenu)
        {
            e.Handled = true;
            var targetIsOpen = !IsOpen;
            Dispatcher.BeginInvoke(() =>
            {
                IsOpen = targetIsOpen;
            }, DispatcherPriority.Input);
        }
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        SetCurrentValue(IsOpenProperty, false);
        _isPopupClosing = true;
        Dispatcher.BeginInvoke(() => _isPopupClosing = false, DispatcherPriority.Background);
    }

    private static void OnMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var button = (DropdownButton)d;

        if (e.OldValue is DropdownMenu oldMenu)
        {
            oldMenu.RemoveHandler(DropdownItem.ClickEvent, new RoutedEventHandler(button.OnDropdownItemClick));
            oldMenu.MouseEnter -= button.OnPopupChildMouseEnter;
            oldMenu.MouseLeave -= button.OnPopupChildMouseLeave;
        }

        if (e.NewValue is DropdownMenu newMenu)
        {
            newMenu.AddHandler(DropdownItem.ClickEvent, new RoutedEventHandler(button.OnDropdownItemClick));
            newMenu.MouseEnter += button.OnPopupChildMouseEnter;
            newMenu.MouseLeave += button.OnPopupChildMouseLeave;
        }
    }

    private void OnPopupChildMouseEnter(object sender, MouseEventArgs e)
    {
        if (Trigger == AntdDropdownTrigger.Hover)
        {
            StopHoverCloseTimer();
        }
    }

    private void OnPopupChildMouseLeave(object sender, MouseEventArgs e)
    {
        if (Trigger == AntdDropdownTrigger.Hover)
        {
            StartHoverCloseTimer();
        }
    }

    private void StartHoverCloseTimer()
    {
        StopHoverCloseTimer();
        _hoverCloseTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(150) };
        _hoverCloseTimer.Tick += OnHoverCloseTimerTick;
        _hoverCloseTimer.Start();
    }

    private void StopHoverCloseTimer()
    {
        if (_hoverCloseTimer != null)
        {
            _hoverCloseTimer.Stop();
            _hoverCloseTimer.Tick -= OnHoverCloseTimerTick;
            _hoverCloseTimer = null;
        }
    }

    private void OnHoverCloseTimerTick(object? sender, EventArgs e)
    {
        StopHoverCloseTimer();
        IsOpen = false;
    }

    private void OnDropdownItemClick(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is DropdownItem item)
        {
            RaiseEvent(new DropdownItemClickRoutedEventArgs(ItemClickEvent, this, item));
            IsOpen = false;
        }
    }

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var button = (DropdownButton)d;
        var isOpen = (bool)e.NewValue;
        button.RaiseEvent(new DropdownVisibleChangeRoutedEventArgs(VisibleChangeEvent, button, isOpen));
    }
}

public delegate void DropdownVisibleChangeRoutedEventHandler(object sender, DropdownVisibleChangeRoutedEventArgs e);

public delegate void DropdownItemClickRoutedEventHandler(object sender, DropdownItemClickRoutedEventArgs e);

public class DropdownVisibleChangeRoutedEventArgs : RoutedEventArgs
{
    public bool Visible { get; }

    public DropdownVisibleChangeRoutedEventArgs(RoutedEvent routedEvent, object source, bool visible)
        : base(routedEvent, source)
    {
        Visible = visible;
    }
}

public class DropdownItemClickRoutedEventArgs : RoutedEventArgs
{
    public DropdownItem? Item { get; }

    public string? Key { get; }

    public DropdownItemClickRoutedEventArgs(RoutedEvent routedEvent, object source, DropdownItem? item)
        : base(routedEvent, source)
    {
        Item = item;
        Key = item?.Key;
    }
}

public static class DropdownAssist
{
    public static readonly DependencyProperty ArrowIconProperty =
        DependencyProperty.RegisterAttached("ArrowIcon", typeof(object), typeof(DropdownAssist),
            new PropertyMetadata(null));

    public static object? GetArrowIcon(DependencyObject obj)
    {
        return obj.GetValue(ArrowIconProperty);
    }

    public static void SetArrowIcon(DependencyObject obj, object? value)
    {
        obj.SetValue(ArrowIconProperty, value);
    }
}
