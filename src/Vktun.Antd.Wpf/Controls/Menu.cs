using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a menu item in a navigation menu.
/// </summary>
public class MenuItem : HeaderedItemsControl
{
    static MenuItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItem),
            new FrameworkPropertyMetadata(typeof(MenuItem)));
    }

    /// <summary>
    /// Identifies the <see cref="Key"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KeyProperty =
        DependencyProperty.Register(nameof(Key), typeof(string), typeof(MenuItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(MenuItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="IsSelected"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(MenuItem),
            new PropertyMetadata(false, OnIsSelectedChanged));

    /// <summary>
    /// Identifies the <see cref="IsDisabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDisabledProperty =
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(MenuItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="HasSubmenu"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HasSubmenuProperty =
        DependencyProperty.Register(nameof(HasSubmenu), typeof(bool), typeof(MenuItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsExpanded"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(MenuItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Level"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LevelProperty =
        DependencyProperty.Register(nameof(Level), typeof(int), typeof(MenuItem),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the unique key for this menu item.
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
    /// Gets or sets whether this item is selected.
    /// </summary>
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
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
    /// Gets or sets whether this item has a submenu.
    /// </summary>
    public bool HasSubmenu
    {
        get => (bool)GetValue(HasSubmenuProperty);
        set => SetValue(HasSubmenuProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the submenu is expanded.
    /// </summary>
    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    /// <summary>
    /// Gets or sets the nesting level of this item.
    /// </summary>
    public int Level
    {
        get => (int)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var item = (MenuItem)d;
        if (item.IsSelected && item.Parent is Menu menu)
        {
            menu.SelectedItem = item;
        }
    }
}

/// <summary>
/// Represents a submenu group in a navigation menu.
/// </summary>
public class Submenu : MenuItem
{
    static Submenu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Submenu),
            new FrameworkPropertyMetadata(typeof(Submenu)));
    }

    /// <summary>
    /// Identifies the <see cref="PopupPlacement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PopupPlacementProperty =
        DependencyProperty.Register(nameof(PopupPlacement), typeof(PlacementMode), typeof(Submenu),
            new PropertyMetadata(PlacementMode.Right));

    /// <summary>
    /// Gets or sets the popup placement mode.
    /// </summary>
    public PlacementMode PopupPlacement
    {
        get => (PlacementMode)GetValue(PopupPlacementProperty);
        set => SetValue(PopupPlacementProperty, value);
    }
}

/// <summary>
/// Represents a navigation menu component.
/// </summary>
public class Menu : ItemsControl
{
    static Menu()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Menu),
            new FrameworkPropertyMetadata(typeof(Menu)));
    }

    /// <summary>
    /// Identifies the <see cref="Mode"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register(nameof(Mode), typeof(AntdMenuMode), typeof(Menu),
            new PropertyMetadata(AntdMenuMode.Vertical, OnModeChanged));

    /// <summary>
    /// Identifies the <see cref="Theme"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ThemeProperty =
        DependencyProperty.Register(nameof(Theme), typeof(AntdMenuTheme), typeof(Menu),
            new PropertyMetadata(AntdMenuTheme.Light));

    /// <summary>
    /// Identifies the <see cref="SelectedKey"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedKeyProperty =
        DependencyProperty.Register(nameof(SelectedKey), typeof(string), typeof(Menu),
            new PropertyMetadata(null, OnSelectedKeyChanged));

    /// <summary>
    /// Identifies the <see cref="SelectedItem"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(MenuItem), typeof(Menu),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="DefaultSelectedKey"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DefaultSelectedKeyProperty =
        DependencyProperty.Register(nameof(DefaultSelectedKey), typeof(string), typeof(Menu),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="OpenKeys"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OpenKeysProperty =
        DependencyProperty.Register(nameof(OpenKeys), typeof(ObservableCollection<string>), typeof(Menu),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="DefaultOpenKeys"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DefaultOpenKeysProperty =
        DependencyProperty.Register(nameof(DefaultOpenKeys), typeof(string[]), typeof(Menu),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="InlineIndent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty InlineIndentProperty =
        DependencyProperty.Register(nameof(InlineIndent), typeof(double), typeof(Menu),
            new PropertyMetadata(24.0));

    /// <summary>
    /// Identifies the <see cref="SubMenuOpenDelay"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SubMenuOpenDelayProperty =
        DependencyProperty.Register(nameof(SubMenuOpenDelay), typeof(int), typeof(Menu),
            new PropertyMetadata(100));

    /// <summary>
    /// Identifies the <see cref="SubMenuCloseDelay"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SubMenuCloseDelayProperty =
        DependencyProperty.Register(nameof(SubMenuCloseDelay), typeof(int), typeof(Menu),
            new PropertyMetadata(150));

    /// <summary>
    /// Identifies the <see cref="Selectable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectableProperty =
        DependencyProperty.Register(nameof(Selectable), typeof(bool), typeof(Menu),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="Collapsed"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CollapsedProperty =
        DependencyProperty.Register(nameof(Collapsed), typeof(bool), typeof(Menu),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the menu display mode.
    /// </summary>
    public AntdMenuMode Mode
    {
        get => (AntdMenuMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    /// <summary>
    /// Gets or sets the menu theme.
    /// </summary>
    public AntdMenuTheme Theme
    {
        get => (AntdMenuTheme)GetValue(ThemeProperty);
        set => SetValue(ThemeProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected menu item key.
    /// </summary>
    public string? SelectedKey
    {
        get => (string?)GetValue(SelectedKeyProperty);
        set => SetValue(SelectedKeyProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected menu item.
    /// </summary>
    public MenuItem? SelectedItem
    {
        get => (MenuItem?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    /// <summary>
    /// Gets or sets the default selected key.
    /// </summary>
    public string? DefaultSelectedKey
    {
        get => (string?)GetValue(DefaultSelectedKeyProperty);
        set => SetValue(DefaultSelectedKeyProperty, value);
    }

    /// <summary>
    /// Gets or sets the currently open submenu keys.
    /// </summary>
    public ObservableCollection<string>? OpenKeys
    {
        get => (ObservableCollection<string>?)GetValue(OpenKeysProperty);
        set => SetValue(OpenKeysProperty, value);
    }

    /// <summary>
    /// Gets or sets the default open submenu keys.
    /// </summary>
    public string[]? DefaultOpenKeys
    {
        get => (string[]?)GetValue(DefaultOpenKeysProperty);
        set => SetValue(DefaultOpenKeysProperty, value);
    }

    /// <summary>
    /// Gets or sets the inline menu indent width.
    /// </summary>
    public double InlineIndent
    {
        get => (double)GetValue(InlineIndentProperty);
        set => SetValue(InlineIndentProperty, value);
    }

    /// <summary>
    /// Gets or sets the submenu open delay in milliseconds.
    /// </summary>
    public int SubMenuOpenDelay
    {
        get => (int)GetValue(SubMenuOpenDelayProperty);
        set => SetValue(SubMenuOpenDelayProperty, value);
    }

    /// <summary>
    /// Gets or sets the submenu close delay in milliseconds.
    /// </summary>
    public int SubMenuCloseDelay
    {
        get => (int)GetValue(SubMenuCloseDelayProperty);
        set => SetValue(SubMenuCloseDelayProperty, value);
    }

    /// <summary>
    /// Gets or sets whether menu items are selectable.
    /// </summary>
    public bool Selectable
    {
        get => (bool)GetValue(SelectableProperty);
        set => SetValue(SelectableProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the menu is collapsed (inline mode only).
    /// </summary>
    public bool Collapsed
    {
        get => (bool)GetValue(CollapsedProperty);
        set => SetValue(CollapsedProperty, value);
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

    /// <summary>
    /// Updates the level property for all menu items.
    /// </summary>
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

    /// <summary>
    /// Updates the selection based on SelectedKey.
    /// </summary>
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

/// <summary>
/// Attached properties for Menu component.
/// </summary>
public static class MenuAssist
{
    /// <summary>
    /// Identifies the <see cref="GetExpandIcon"/> attached property.
    /// </summary>
    public static readonly DependencyProperty ExpandIconProperty =
        DependencyProperty.RegisterAttached("ExpandIcon", typeof(object), typeof(MenuAssist),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets the expand icon.
    /// </summary>
    public static object? GetExpandIcon(DependencyObject obj)
    {
        return obj.GetValue(ExpandIconProperty);
    }

    /// <summary>
    /// Sets the expand icon.
    /// </summary>
    public static void SetExpandIcon(DependencyObject obj, object? value)
    {
        obj.SetValue(ExpandIconProperty, value);
    }
}