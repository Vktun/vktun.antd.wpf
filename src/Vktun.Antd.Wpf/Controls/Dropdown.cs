using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a dropdown menu item.
/// </summary>
public class DropdownItem : HeaderedContentControl
{
    static DropdownItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownItem),
            new FrameworkPropertyMetadata(typeof(DropdownItem)));
    }

    /// <summary>
    /// Identifies the <see cref="Key"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(string), typeof(DropdownItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(DropdownItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="IsDisabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDisabledProperty =
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(DropdownItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsDivider"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDividerProperty =
        DependencyProperty.Register(nameof(IsDivider), typeof(bool), typeof(DropdownItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Danger"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DangerProperty =
        DependencyProperty.Register(nameof(Danger), typeof(bool), typeof(DropdownItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the unique key for this item.
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
    /// Gets or sets whether this item is disabled.
    /// </summary>
    public bool IsDisabled
    {
        get => (bool)GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this item is a divider.
    /// </summary>
    public bool IsDivider
    {
        get => (bool)GetValue(IsDividerProperty);
        set => SetValue(IsDividerProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this item is a danger action.
    /// </summary>
    public bool Danger
    {
        get => (bool)GetValue(DangerProperty);
        set => SetValue(DangerProperty, value);
    }
}

/// <summary>
/// Represents a dropdown menu that displays a list of actions.
/// </summary>
public class Dropdown : ContentControl
{
    static Dropdown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Dropdown),
            new FrameworkPropertyMetadata(typeof(Dropdown)));
    }

    /// <summary>
    /// Identifies the <see cref="Menu"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MenuProperty =
        DependencyProperty.Register(nameof(Menu), typeof(DropdownMenu), typeof(Dropdown),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Placement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(Dropdown),
            new PropertyMetadata(PlacementMode.Bottom));

    /// <summary>
    /// Identifies the <see cref="Trigger"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TriggerProperty =
        DependencyProperty.Register(nameof(Trigger), typeof(AntdDropdownTrigger), typeof(Dropdown),
            new PropertyMetadata(AntdDropdownTrigger.Hover));

    /// <summary>
    /// Identifies the <see cref="IsOpen"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Dropdown),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Arrow"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ArrowProperty =
        DependencyProperty.Register(nameof(Arrow), typeof(bool), typeof(Dropdown),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Disabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DisabledProperty =
        DependencyProperty.Register(nameof(Disabled), typeof(bool), typeof(Dropdown),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="DestroyPopupOnHide"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DestroyPopupOnHideProperty =
        DependencyProperty.Register(nameof(DestroyPopupOnHide), typeof(bool), typeof(Dropdown),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="GetPopupContainer"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty GetPopupContainerProperty =
        DependencyProperty.Register(nameof(GetPopupContainer), typeof(FrameworkElement), typeof(Dropdown),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="OverlayStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayStyleProperty =
        DependencyProperty.Register(nameof(OverlayStyle), typeof(Style), typeof(Dropdown),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="OverlayClassName"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayClassNameProperty =
        DependencyProperty.Register(nameof(OverlayClassName), typeof(string), typeof(Dropdown),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="VisibleChange"/> routed event.
    /// </summary>
    public static readonly RoutedEvent VisibleChangeEvent =
        EventManager.RegisterRoutedEvent("VisibleChange", RoutingStrategy.Bubble,
            typeof(DropdownVisibleChangeRoutedEventHandler), typeof(Dropdown));

    /// <summary>
    /// Identifies the <see cref="ItemClick"/> routed event.
    /// </summary>
    public static readonly RoutedEvent ItemClickEvent =
        EventManager.RegisterRoutedEvent("ItemClick", RoutingStrategy.Bubble,
            typeof(DropdownItemClickRoutedEventHandler), typeof(Dropdown));

    /// <summary>
    /// Gets or sets the dropdown menu.
    /// </summary>
    public DropdownMenu? Menu
    {
        get => (DropdownMenu?)GetValue(MenuProperty);
        set => SetValue(MenuProperty, value);
    }

    /// <summary>
    /// Gets or sets the placement of the dropdown.
    /// </summary>
    public PlacementMode Placement
    {
        get => (PlacementMode)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>
    /// Gets or sets the trigger mode.
    /// </summary>
    public AntdDropdownTrigger Trigger
    {
        get => (AntdDropdownTrigger)GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the dropdown is visible.
    /// </summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
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
    /// Gets or sets whether the dropdown is disabled.
    /// </summary>
    public bool Disabled
    {
        get => (bool)GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to destroy the popup on hide.
    /// </summary>
    public bool DestroyPopupOnHide
    {
        get => (bool)GetValue(DestroyPopupOnHideProperty);
        set => SetValue(DestroyPopupOnHideProperty, value);
    }

    /// <summary>
    /// Gets or sets the popup container element.
    /// </summary>
    public FrameworkElement? GetPopupContainer
    {
        get => (FrameworkElement?)GetValue(GetPopupContainerProperty);
        set => SetValue(GetPopupContainerProperty, value);
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
    /// Gets or sets the overlay class name.
    /// </summary>
    public string? OverlayClassName
    {
        get => (string?)GetValue(OverlayClassNameProperty);
        set => SetValue(OverlayClassNameProperty, value);
    }

    /// <summary>
    /// Occurs when the dropdown visibility changes.
    /// </summary>
    public event DropdownVisibleChangeRoutedEventHandler VisibleChange
    {
        add => AddHandler(VisibleChangeEvent, value);
        remove => RemoveHandler(VisibleChangeEvent, value);
    }

    /// <summary>
    /// Occurs when a menu item is clicked.
    /// </summary>
    public event DropdownItemClickRoutedEventHandler ItemClick
    {
        add => AddHandler(ItemClickEvent, value);
        remove => RemoveHandler(ItemClickEvent, value);
    }

    /// <summary>
    /// Shows the dropdown.
    /// </summary>
    public void Show()
    {
        IsOpen = true;
    }

    /// <summary>
    /// Hides the dropdown.
    /// </summary>
    public void Hide()
    {
        IsOpen = false;
    }
}

/// <summary>
/// Represents a dropdown menu container.
/// </summary>
public class DropdownMenu : ItemsControl
{
    static DropdownMenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownMenu),
            new FrameworkPropertyMetadata(typeof(DropdownMenu)));
    }

    /// <summary>
    /// Identifies the <see cref="SelectedKey"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedKeyProperty =
        DependencyProperty.Register(nameof(SelectedKey), typeof(string), typeof(DropdownMenu),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Selectable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectableProperty =
        DependencyProperty.Register(nameof(Selectable), typeof(bool), typeof(DropdownMenu),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Multiple"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MultipleProperty =
        DependencyProperty.Register(nameof(Multiple), typeof(bool), typeof(DropdownMenu),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the selected item key.
    /// </summary>
    public string? SelectedKey
    {
        get => (string?)GetValue(SelectedKeyProperty);
        set => SetValue(SelectedKeyProperty, value);
    }

    /// <summary>
    /// Gets or sets whether items are selectable.
    /// </summary>
    public bool Selectable
    {
        get => (bool)GetValue(SelectableProperty);
        set => SetValue(SelectableProperty, value);
    }

    /// <summary>
    /// Gets or sets whether multiple selection is allowed.
    /// </summary>
    public bool Multiple
    {
        get => (bool)GetValue(MultipleProperty);
        set => SetValue(MultipleProperty, value);
    }
}

/// <summary>
/// Represents a dropdown button component.
/// </summary>
public class DropdownButton : Button
{
    static DropdownButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DropdownButton),
            new FrameworkPropertyMetadata(typeof(DropdownButton)));
    }

    /// <summary>
    /// Identifies the <see cref="Menu"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MenuProperty =
        DependencyProperty.Register(nameof(Menu), typeof(DropdownMenu), typeof(DropdownButton),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Placement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(DropdownButton),
            new PropertyMetadata(PlacementMode.Bottom));

    /// <summary>
    /// Identifies the <see cref="Trigger"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TriggerProperty =
        DependencyProperty.Register(nameof(Trigger), typeof(AntdDropdownTrigger), typeof(DropdownButton),
            new PropertyMetadata(AntdDropdownTrigger.Hover));

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdButtonType), typeof(DropdownButton),
            new PropertyMetadata(AntdButtonType.Default));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(DropdownButton),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Arrow"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ArrowProperty =
        DependencyProperty.Register(nameof(Arrow), typeof(bool), typeof(DropdownButton),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Disabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DisabledProperty =
        DependencyProperty.Register(nameof(Disabled), typeof(bool), typeof(DropdownButton),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(DropdownButton),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the dropdown menu.
    /// </summary>
    public DropdownMenu? Menu
    {
        get => (DropdownMenu?)GetValue(MenuProperty);
        set => SetValue(MenuProperty, value);
    }

    /// <summary>
    /// Gets or sets the placement of the dropdown.
    /// </summary>
    public PlacementMode Placement
    {
        get => (PlacementMode)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>
    /// Gets or sets the trigger mode.
    /// </summary>
    public AntdDropdownTrigger Trigger
    {
        get => (AntdDropdownTrigger)GetValue(TriggerProperty);
        set => SetValue(TriggerProperty, value);
    }

    /// <summary>
    /// Gets or sets the button type.
    /// </summary>
    public new AntdButtonType Type
    {
        get => (AntdButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets the button size.
    /// </summary>
    public new AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
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
    /// Gets or sets whether the button is disabled.
    /// </summary>
    public bool Disabled
    {
        get => (bool)GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon content.
    /// </summary>
    public new object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}

/// <summary>
/// Delegate for handling dropdown visible change events.
/// </summary>
public delegate void DropdownVisibleChangeRoutedEventHandler(object sender, DropdownVisibleChangeRoutedEventArgs e);

/// <summary>
/// Delegate for handling dropdown item click events.
/// </summary>
public delegate void DropdownItemClickRoutedEventHandler(object sender, DropdownItemClickRoutedEventArgs e);

/// <summary>
/// Routed event arguments for dropdown visible change events.
/// </summary>
public class DropdownVisibleChangeRoutedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// Gets whether the dropdown is now visible.
    /// </summary>
    public bool Visible { get; }

    public DropdownVisibleChangeRoutedEventArgs(RoutedEvent routedEvent, object source, bool visible)
        : base(routedEvent, source)
    {
        Visible = visible;
    }
}

/// <summary>
/// Routed event arguments for dropdown item click events.
/// </summary>
public class DropdownItemClickRoutedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// Gets the clicked item.
    /// </summary>
    public DropdownItem? Item { get; }

    /// <summary>
    /// Gets the key of the clicked item.
    /// </summary>
    public string? Key { get; }

    public DropdownItemClickRoutedEventArgs(RoutedEvent routedEvent, object source, DropdownItem? item)
        : base(routedEvent, source)
    {
        Item = item;
        Key = item?.Key;
    }
}

/// <summary>
/// Attached properties for Dropdown component.
/// </summary>
public static class DropdownAssist
{
    /// <summary>
    /// Identifies the <see cref="GetArrowIcon"/> attached property.
    /// </summary>
    public static readonly DependencyProperty ArrowIconProperty =
        DependencyProperty.RegisterAttached("ArrowIcon", typeof(object), typeof(DropdownAssist),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets the arrow icon.
    /// </summary>
    public static object? GetArrowIcon(DependencyObject obj)
    {
        return obj.GetValue(ArrowIconProperty);
    }

    /// <summary>
    /// Sets the arrow icon.
    /// </summary>
    public static void SetArrowIcon(DependencyObject obj, object? value)
    {
        obj.SetValue(ArrowIconProperty, value);
    }
}
