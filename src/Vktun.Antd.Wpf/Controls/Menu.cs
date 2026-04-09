using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Vktun.Antd.Wpf;

public delegate void MenuSelectionChangedRoutedEventHandler(object sender, MenuSelectionChangedEventArgs e);

public class MenuSelectionChangedEventArgs : RoutedEventArgs
{
    public MenuItem? Item { get; }
    public string? Key { get; }

    public MenuSelectionChangedEventArgs(RoutedEvent routedEvent, object source, MenuItem? item)
        : base(routedEvent, source)
    {
        Item = item;
        Key = item?.Key;
    }
}

public class MenuItem : HeaderedItemsControl
{
    static MenuItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItem),
            new FrameworkPropertyMetadata(typeof(MenuItem)));
    }

    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(string), typeof(MenuItem),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(MenuItem),
            new PropertyMetadata(null));

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(MenuItem),
            new PropertyMetadata(false, OnIsSelectedChanged));

    public static readonly DependencyProperty IsDisabledProperty =
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(MenuItem),
            new PropertyMetadata(false));

    public static readonly DependencyProperty HasSubmenuProperty =
        DependencyProperty.Register(nameof(HasSubmenu), typeof(bool), typeof(MenuItem),
            new PropertyMetadata(false));

    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(MenuItem),
            new PropertyMetadata(false));

    public static readonly DependencyProperty LevelProperty =
        DependencyProperty.Register(nameof(Level), typeof(int), typeof(MenuItem),
            new PropertyMetadata(0));

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

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public bool IsDisabled
    {
        get => (bool)GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    public bool HasSubmenu
    {
        get => (bool)GetValue(HasSubmenuProperty);
        set => SetValue(HasSubmenuProperty, value);
    }

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public int Level
    {
        get => (int)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        if (IsDisabled)
        {
            e.Handled = true;
            return;
        }

        var menu = FindParentMenu();
        if (menu == null)
            return;

        if (HasSubmenu)
        {
            IsExpanded = !IsExpanded;
            e.Handled = true;
            return;
        }

        if (menu.Selectable)
        {
            menu.SelectItem(this);
        }

        e.Handled = true;
    }

    internal Menu? FindParentMenu()
    {
        var parent = Parent as DependencyObject;
        while (parent != null)
        {
            if (parent is Menu menu)
                return menu;
            parent = System.Windows.Media.VisualTreeHelper.GetParent(parent) ??
                     (parent is FrameworkElement fe ? fe.Parent as DependencyObject : null);
        }
        return null;
    }

    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var item = (MenuItem)d;
        if (item.IsSelected)
        {
            var menu = item.FindParentMenu();
            if (menu != null)
            {
                menu.SelectedItem = item;
                if (menu.SelectedKey != item.Key)
                {
                    menu.SetCurrentValue(Menu.SelectedKeyProperty, item.Key);
                }
            }
        }
    }
}

public class Submenu : MenuItem
{
    static Submenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Submenu),
            new FrameworkPropertyMetadata(typeof(Submenu)));
    }

    public static readonly DependencyProperty PopupPlacementProperty =
        DependencyProperty.Register(nameof(PopupPlacement), typeof(PlacementMode), typeof(Submenu),
            new PropertyMetadata(PlacementMode.Right));

    public PlacementMode PopupPlacement
    {
        get => (PlacementMode)GetValue(PopupPlacementProperty);
        set => SetValue(PopupPlacementProperty, value);
    }
}

public class Menu : ItemsControl
{
    static Menu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Menu),
            new FrameworkPropertyMetadata(typeof(Menu)));
    }

    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register(nameof(Mode), typeof(AntdMenuMode), typeof(Menu),
            new PropertyMetadata(AntdMenuMode.Vertical, OnModeChanged));

    public static readonly DependencyProperty ThemeProperty =
        DependencyProperty.Register(nameof(Theme), typeof(AntdMenuTheme), typeof(Menu),
            new PropertyMetadata(AntdMenuTheme.Light));

    public static readonly DependencyProperty SelectedKeyProperty =
        DependencyProperty.Register(nameof(SelectedKey), typeof(string), typeof(Menu),
            new PropertyMetadata(null, OnSelectedKeyChanged));

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(MenuItem), typeof(Menu),
            new PropertyMetadata(null));

    public static readonly DependencyProperty DefaultSelectedKeyProperty =
        DependencyProperty.Register(nameof(DefaultSelectedKey), typeof(string), typeof(Menu),
            new PropertyMetadata(null));

    public static readonly DependencyProperty OpenKeysProperty =
        DependencyProperty.Register(nameof(OpenKeys), typeof(ObservableCollection<string>), typeof(Menu),
            new PropertyMetadata(null));

    public static readonly DependencyProperty DefaultOpenKeysProperty =
        DependencyProperty.Register(nameof(DefaultOpenKeys), typeof(string[]), typeof(Menu),
            new PropertyMetadata(null));

    public static readonly DependencyProperty InlineIndentProperty =
        DependencyProperty.Register(nameof(InlineIndent), typeof(double), typeof(Menu),
            new PropertyMetadata(24.0));

    public static readonly DependencyProperty SubMenuOpenDelayProperty =
        DependencyProperty.Register(nameof(SubMenuOpenDelay), typeof(int), typeof(Menu),
            new PropertyMetadata(100));

    public static readonly DependencyProperty SubMenuCloseDelayProperty =
        DependencyProperty.Register(nameof(SubMenuCloseDelay), typeof(int), typeof(Menu),
            new PropertyMetadata(150));

    public static readonly DependencyProperty SelectableProperty =
        DependencyProperty.Register(nameof(Selectable), typeof(bool), typeof(Menu),
            new PropertyMetadata(true));

    public static readonly DependencyProperty CollapsedProperty =
        DependencyProperty.Register(nameof(Collapsed), typeof(bool), typeof(Menu),
            new PropertyMetadata(false));

    public static readonly RoutedEvent SelectionChangedEvent =
        EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble,
            typeof(MenuSelectionChangedRoutedEventHandler), typeof(Menu));

    public event MenuSelectionChangedRoutedEventHandler SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    public AntdMenuMode Mode
    {
        get => (AntdMenuMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    public AntdMenuTheme Theme
    {
        get => (AntdMenuTheme)GetValue(ThemeProperty);
        set => SetValue(ThemeProperty, value);
    }

    public string? SelectedKey
    {
        get => (string?)GetValue(SelectedKeyProperty);
        set => SetValue(SelectedKeyProperty, value);
    }

    public MenuItem? SelectedItem
    {
        get => (MenuItem?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public string? DefaultSelectedKey
    {
        get => (string?)GetValue(DefaultSelectedKeyProperty);
        set => SetValue(DefaultSelectedKeyProperty, value);
    }

    public ObservableCollection<string>? OpenKeys
    {
        get => (ObservableCollection<string>?)GetValue(OpenKeysProperty);
        set => SetValue(OpenKeysProperty, value);
    }

    public string[]? DefaultOpenKeys
    {
        get => (string[]?)GetValue(DefaultOpenKeysProperty);
        set => SetValue(DefaultOpenKeysProperty, value);
    }

    public double InlineIndent
    {
        get => (double)GetValue(InlineIndentProperty);
        set => SetValue(InlineIndentProperty, value);
    }

    public int SubMenuOpenDelay
    {
        get => (int)GetValue(SubMenuOpenDelayProperty);
        set => SetValue(SubMenuOpenDelayProperty, value);
    }

    public int SubMenuCloseDelay
    {
        get => (int)GetValue(SubMenuCloseDelayProperty);
        set => SetValue(SubMenuCloseDelayProperty, value);
    }

    public bool Selectable
    {
        get => (bool)GetValue(SelectableProperty);
        set => SetValue(SelectableProperty, value);
    }

    public bool Collapsed
    {
        get => (bool)GetValue(CollapsedProperty);
        set => SetValue(CollapsedProperty, value);
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        UpdateItemLevels();

        if (SelectedKey != null)
        {
            UpdateSelection();
        }
        else if (DefaultSelectedKey != null)
        {
            SelectedKey = DefaultSelectedKey;
        }

        if (DefaultOpenKeys != null && OpenKeys == null)
        {
            OpenKeys = new ObservableCollection<string>(DefaultOpenKeys);
        }
    }

    internal void SelectItem(MenuItem item)
    {
        DeselectAll(Items);

        item.IsSelected = true;
        SelectedItem = item;
        SelectedKey = item.Key;

        RaiseEvent(new MenuSelectionChangedEventArgs(SelectionChangedEvent, this, item));
    }

    private void DeselectAll(ItemCollection items)
    {
        foreach (var i in items)
        {
            if (i is MenuItem menuItem)
            {
                if (menuItem.IsSelected)
                    menuItem.IsSelected = false;
                if (menuItem.HasSubmenu)
                    DeselectAll(menuItem.Items);
            }
        }
    }

    private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var menu = (Menu)d;
        menu.UpdateItemLevels();
    }

    private static void OnSelectedKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var menu = (Menu)d;
        menu.UpdateSelection();
    }

    public void UpdateItemLevels()
    {
        UpdateItemLevels(Items, 0);
    }

    private void UpdateItemLevels(ItemCollection items, int level)
    {
        foreach (var item in items)
        {
            if (item is MenuItem menuItem)
            {
                menuItem.Level = level;
                menuItem.HasSubmenu = menuItem.Items.Count > 0;
                if (menuItem.HasSubmenu)
                {
                    UpdateItemLevels(menuItem.Items, level + 1);
                }
            }
        }
    }

    public void UpdateSelection()
    {
        UpdateSelection(Items, SelectedKey);
    }

    private void UpdateSelection(ItemCollection items, string? selectedKey)
    {
        foreach (var item in items)
        {
            if (item is MenuItem menuItem)
            {
                menuItem.IsSelected = menuItem.Key == selectedKey;
                if (menuItem.HasSubmenu)
                {
                    UpdateSelection(menuItem.Items, selectedKey);
                }
            }
        }
    }
}

public static class MenuAssist
{
    public static readonly DependencyProperty ExpandIconProperty =
        DependencyProperty.RegisterAttached("ExpandIcon", typeof(object), typeof(MenuAssist),
            new PropertyMetadata(null));

    public static object? GetExpandIcon(DependencyObject obj)
    {
        return obj.GetValue(ExpandIconProperty);
    }

    public static void SetExpandIcon(DependencyObject obj, object? value)
    {
        obj.SetValue(ExpandIconProperty, value);
    }
}
