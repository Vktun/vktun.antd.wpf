using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;

namespace Vktun.Antd.Avalonia;

internal static class AntdProperty
{
    public static StyledProperty<T> Register<TOwner, T>(string name, T defaultValue = default!)
        where TOwner : AvaloniaObject
    {
#pragma warning disable AVP1001
        return AvaloniaProperty.Register<TOwner, T>(name, defaultValue);
#pragma warning restore AVP1001
    }
}

internal sealed class DelegateCommand<T>(Action<T?> execute, Predicate<T?>? canExecute = null) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return canExecute?.Invoke((T?)parameter) ?? true;
    }

    public void Execute(object? parameter)
    {
        execute((T?)parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}

/// <summary>
/// Represents an Ant Design styled button with semantic type, size, status, and icon support.
/// </summary>
public class Button : global::Avalonia.Controls.Button
{
    public static readonly StyledProperty<AntdButtonType> TypeProperty =
        AntdProperty.Register<Button, AntdButtonType>(nameof(Type), AntdButtonType.Default);

    public static readonly StyledProperty<AntdControlSize> SizeProperty =
        AntdProperty.Register<Button, AntdControlSize>(nameof(Size), AntdControlSize.Middle);

    public static readonly StyledProperty<AntdStatus> StatusProperty =
        AntdProperty.Register<Button, AntdStatus>(nameof(Status), AntdStatus.None);

    public static readonly StyledProperty<object?> IconProperty =
        AntdProperty.Register<Button, object?>(nameof(Icon));

    public AntdButtonType Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public AntdControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public AntdStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}

/// <summary>
/// Represents an icon-oriented Ant Design button.
/// </summary>
[Obsolete("Use Button with Icon property instead. Will be removed in v2.0.")]
public class IconButton : Button
{
}

/// <summary>
/// Represents Ant Design typography text.
/// </summary>
public class Typography : TextBlock
{
    public static readonly StyledProperty<AntdTypographyType> TypeProperty =
        AntdProperty.Register<Typography, AntdTypographyType>(nameof(Type), AntdTypographyType.Default);

    public AntdTypographyType Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
}

/// <summary>
/// Represents a floating action button.
/// </summary>
public class FloatButton : Button
{
    public static readonly StyledProperty<AntdFloatButtonShape> ShapeProperty =
        AntdProperty.Register<FloatButton, AntdFloatButtonShape>(nameof(Shape), AntdFloatButtonShape.Circle);

    public static readonly StyledProperty<AntdFloatButtonPlacement> PlacementProperty =
        AntdProperty.Register<FloatButton, AntdFloatButtonPlacement>(nameof(Placement), AntdFloatButtonPlacement.BottomRight);

    public static readonly StyledProperty<bool> IsGlobalProperty =
        AntdProperty.Register<FloatButton, bool>(nameof(IsGlobal));

    public AntdFloatButtonShape Shape
    {
        get => GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    public AntdFloatButtonPlacement Placement
    {
        get => GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    public bool IsGlobal
    {
        get => GetValue(IsGlobalProperty);
        set => SetValue(IsGlobalProperty, value);
    }
}

/// <summary>
/// Represents a semantic label with optional close and check states.
/// </summary>
public class Tag : ContentControl
{
    public static readonly StyledProperty<AntdTagColor> ColorProperty =
        AntdProperty.Register<Tag, AntdTagColor>(nameof(Color), AntdTagColor.Default);

    public static readonly StyledProperty<bool> IsClosableProperty =
        AntdProperty.Register<Tag, bool>(nameof(IsClosable));

    public static readonly StyledProperty<bool> IsCheckableProperty =
        AntdProperty.Register<Tag, bool>(nameof(IsCheckable));

    public static readonly StyledProperty<bool> IsCheckedProperty =
        AntdProperty.Register<Tag, bool>(nameof(IsChecked));

    public static readonly StyledProperty<bool> BorderlessProperty =
        AntdProperty.Register<Tag, bool>(nameof(Borderless));

    public event EventHandler? CloseClick;

    public event EventHandler? CheckedChanged;

    public AntdTagColor Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public bool IsClosable
    {
        get => GetValue(IsClosableProperty);
        set => SetValue(IsClosableProperty, value);
    }

    public bool IsCheckable
    {
        get => GetValue(IsCheckableProperty);
        set => SetValue(IsCheckableProperty, value);
    }

    public bool IsChecked
    {
        get => GetValue(IsCheckedProperty);
        set
        {
            var oldValue = IsChecked;
            SetValue(IsCheckedProperty, value);
            if (oldValue != value)
            {
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool Borderless
    {
        get => GetValue(BorderlessProperty);
        set => SetValue(BorderlessProperty, value);
    }

    public void RaiseCloseClickEvent()
    {
        CloseClick?.Invoke(this, EventArgs.Empty);
    }
}

/// <summary>
/// Represents a compact status badge.
/// </summary>
public class Badge : ContentControl
{
    public static readonly StyledProperty<int> CountProperty =
        AntdProperty.Register<Badge, int>(nameof(Count));

    public static readonly StyledProperty<AntdStatus> StatusProperty =
        AntdProperty.Register<Badge, AntdStatus>(nameof(Status), AntdStatus.None);

    public int Count
    {
        get => GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    public AntdStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
}

/// <summary>
/// Represents a reusable content card.
/// </summary>
public class Card : ContentControl
{
    public static readonly StyledProperty<object?> TitleProperty =
        AntdProperty.Register<Card, object?>(nameof(Title));

    public static readonly StyledProperty<object?> ExtraProperty =
        AntdProperty.Register<Card, object?>(nameof(Extra));

    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }
}

/// <summary>
/// Represents a feedback alert.
/// </summary>
public class Alert : ContentControl
{
    public static readonly StyledProperty<AntdAlertType> TypeProperty =
        AntdProperty.Register<Alert, AntdAlertType>(nameof(Type), AntdAlertType.Info);

    public static readonly StyledProperty<bool> ShowIconProperty =
        AntdProperty.Register<Alert, bool>(nameof(ShowIcon), true);

    public static readonly StyledProperty<bool> ClosableProperty =
        AntdProperty.Register<Alert, bool>(nameof(Closable));

    public static readonly StyledProperty<string?> CloseTextProperty =
        AntdProperty.Register<Alert, string?>(nameof(CloseText));

    public static readonly StyledProperty<object?> MessageProperty =
        AntdProperty.Register<Alert, object?>(nameof(Message));

    public static readonly StyledProperty<object?> DescriptionProperty =
        AntdProperty.Register<Alert, object?>(nameof(Description));

    public static readonly StyledProperty<bool> BannerProperty =
        AntdProperty.Register<Alert, bool>(nameof(Banner));

    public static readonly StyledProperty<bool> IsClosedProperty =
        AntdProperty.Register<Alert, bool>(nameof(IsClosed));

    public AntdAlertType Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public bool ShowIcon
    {
        get => GetValue(ShowIconProperty);
        set => SetValue(ShowIconProperty, value);
    }

    public bool Closable
    {
        get => GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    public string? CloseText
    {
        get => GetValue(CloseTextProperty);
        set => SetValue(CloseTextProperty, value);
    }

    public object? Message
    {
        get => GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public bool Banner
    {
        get => GetValue(BannerProperty);
        set => SetValue(BannerProperty, value);
    }

    public bool IsClosed
    {
        get => GetValue(IsClosedProperty);
        set => SetValue(IsClosedProperty, value);
    }

    public void Close()
    {
        IsClosed = true;
    }
}

/// <summary>
/// Represents an Ant Design switch.
/// </summary>
public class Switch : ToggleButton
{
    public static readonly StyledProperty<AntdControlSize> SizeProperty =
        AntdProperty.Register<Switch, AntdControlSize>(nameof(Size), AntdControlSize.Middle);

    public AntdControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
}

/// <summary>
/// Represents an Ant Design progress indicator.
/// </summary>
public class Progress : TemplatedControl
{
    public static readonly StyledProperty<double> ValueProperty =
        AntdProperty.Register<Progress, double>(nameof(Value));

    public static readonly StyledProperty<AntdProgressType> TypeProperty =
        AntdProperty.Register<Progress, AntdProgressType>(nameof(Type), AntdProgressType.Line);

    public static readonly StyledProperty<AntdProgressStatus> StatusProperty =
        AntdProperty.Register<Progress, AntdProgressStatus>(nameof(Status), AntdProgressStatus.Normal);

    public static readonly StyledProperty<bool> ShowInfoProperty =
        AntdProperty.Register<Progress, bool>(nameof(ShowInfo), true);

    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, Math.Clamp(value, 0d, 100d));
    }

    public AntdProgressType Type
    {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public AntdProgressStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    public bool ShowInfo
    {
        get => GetValue(ShowInfoProperty);
        set => SetValue(ShowInfoProperty, value);
    }
}

/// <summary>
/// Represents an input box with prefix, suffix, size, variant, and status metadata.
/// </summary>
public class Input : TextBox
{
    public static readonly StyledProperty<object?> PrefixProperty =
        AntdProperty.Register<Input, object?>(nameof(Prefix));

    public static readonly StyledProperty<object?> SuffixProperty =
        AntdProperty.Register<Input, object?>(nameof(Suffix));

    public static readonly StyledProperty<AntdStatus> StatusProperty =
        AntdProperty.Register<Input, AntdStatus>(nameof(Status), AntdStatus.None);

    public static readonly StyledProperty<AntdControlSize> SizeProperty =
        AntdProperty.Register<Input, AntdControlSize>(nameof(Size), AntdControlSize.Middle);

    public static readonly StyledProperty<AntdInputVariant> VariantProperty =
        AntdProperty.Register<Input, AntdInputVariant>(nameof(Variant), AntdInputVariant.Outlined);

    public object? Prefix
    {
        get => GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }

    public object? Suffix
    {
        get => GetValue(SuffixProperty);
        set => SetValue(SuffixProperty, value);
    }

    public AntdStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    public AntdControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public AntdInputVariant Variant
    {
        get => GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }
}

/// <summary>
/// Represents a password input box.
/// </summary>
public class PasswordInput : Input
{
}

/// <summary>
/// Represents a numeric input box with min, max, step, and precision.
/// </summary>
public class InputNumber : TemplatedControl
{
    public static readonly StyledProperty<double> ValueProperty =
        AntdProperty.Register<InputNumber, double>(nameof(Value));

    public static readonly StyledProperty<double> MinimumProperty =
        AntdProperty.Register<InputNumber, double>(nameof(Minimum), double.NegativeInfinity);

    public static readonly StyledProperty<double> MaximumProperty =
        AntdProperty.Register<InputNumber, double>(nameof(Maximum), double.PositiveInfinity);

    public static readonly StyledProperty<double> StepProperty =
        AntdProperty.Register<InputNumber, double>(nameof(Step), 1d);

    public static readonly StyledProperty<int> PrecisionProperty =
        AntdProperty.Register<InputNumber, int>(nameof(Precision), -1);

    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, CoerceValue(value));
    }

    public double Minimum
    {
        get => GetValue(MinimumProperty);
        set
        {
            SetValue(MinimumProperty, value);
            Value = Value;
        }
    }

    public double Maximum
    {
        get => GetValue(MaximumProperty);
        set
        {
            SetValue(MaximumProperty, value);
            Value = Value;
        }
    }

    public double Step
    {
        get => GetValue(StepProperty);
        set => SetValue(StepProperty, value <= 0d ? 1d : value);
    }

    public int Precision
    {
        get => GetValue(PrecisionProperty);
        set
        {
            SetValue(PrecisionProperty, value);
            Value = Value;
        }
    }

    public void Increment()
    {
        Value += Step;
    }

    public void Decrement()
    {
        Value -= Step;
    }

    private double CoerceValue(double value)
    {
        var coerced = Math.Clamp(value, Minimum, Maximum);
        return Precision >= 0 ? Math.Round(coerced, Precision, MidpointRounding.AwayFromZero) : coerced;
    }
}

/// <summary>
/// Represents an Ant Design ComboBox.
/// </summary>
public class ComboBox : global::Avalonia.Controls.ComboBox
{
    public static readonly StyledProperty<AntdComboBoxVariant> VariantProperty =
        AntdProperty.Register<ComboBox, AntdComboBoxVariant>(nameof(Variant), AntdComboBoxVariant.Outlined);

    public static readonly StyledProperty<AntdStatus> StatusProperty =
        AntdProperty.Register<ComboBox, AntdStatus>(nameof(Status), AntdStatus.None);

    public static readonly StyledProperty<AntdControlSize> SizeProperty =
        AntdProperty.Register<ComboBox, AntdControlSize>(nameof(Size), AntdControlSize.Middle);

    public AntdComboBoxVariant Variant
    {
        get => GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    public AntdStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    public AntdControlSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
}

/// <summary>
/// Represents an Ant Design DatePicker.
/// </summary>
public class DatePicker : global::Avalonia.Controls.DatePicker
{
    public static readonly StyledProperty<AntdInputVariant> VariantProperty =
        AntdProperty.Register<DatePicker, AntdInputVariant>(nameof(Variant), AntdInputVariant.Outlined);

    public static readonly StyledProperty<AntdStatus> StatusProperty =
        AntdProperty.Register<DatePicker, AntdStatus>(nameof(Status), AntdStatus.None);

    public AntdInputVariant Variant
    {
        get => GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    public AntdStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
}

/// <summary>
/// Represents an Ant Design checkbox.
/// </summary>
public class Checkbox : CheckBox
{
    public static readonly StyledProperty<AntdStatus> StatusProperty =
        AntdProperty.Register<Checkbox, AntdStatus>(nameof(Status), AntdStatus.None);

    public AntdStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
}

/// <summary>
/// Represents an Ant Design radio button.
/// </summary>
public class Radio : RadioButton
{
    public static readonly StyledProperty<AntdStatus> StatusProperty =
        AntdProperty.Register<Radio, AntdStatus>(nameof(Status), AntdStatus.None);

    public AntdStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
}

/// <summary>
/// Represents an Ant Design slider.
/// </summary>
public class Slider : global::Avalonia.Controls.Slider
{
}

/// <summary>
/// Represents an Ant Design form container.
/// </summary>
public class Form : StackPanel
{
    public static readonly StyledProperty<AntdFormLayout> LayoutProperty =
        AntdProperty.Register<Form, AntdFormLayout>(nameof(Layout), AntdFormLayout.Vertical);

    public AntdFormLayout Layout
    {
        get => GetValue(LayoutProperty);
        set => SetValue(LayoutProperty, value);
    }
}

/// <summary>
/// Represents an Ant Design pagination control.
/// </summary>
public class Pagination : TemplatedControl
{
    public static readonly StyledProperty<int> TotalProperty =
        AntdProperty.Register<Pagination, int>(nameof(Total));

    public static readonly StyledProperty<int> PageSizeProperty =
        AntdProperty.Register<Pagination, int>(nameof(PageSize), 10);

    public static readonly StyledProperty<int> CurrentPageProperty =
        AntdProperty.Register<Pagination, int>(nameof(CurrentPage), 1);

    public int Total
    {
        get => GetValue(TotalProperty);
        set
        {
            SetValue(TotalProperty, Math.Max(0, value));
            CurrentPage = CurrentPage;
        }
    }

    public int PageSize
    {
        get => GetValue(PageSizeProperty);
        set
        {
            SetValue(PageSizeProperty, Math.Max(1, value));
            CurrentPage = CurrentPage;
        }
    }

    public int CurrentPage
    {
        get => GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, Math.Clamp(value, 1, PageCount));
    }

    public int PageCount => Math.Max(1, (int)Math.Ceiling(Total / (double)Math.Max(1, PageSize)));
}

/// <summary>
/// Represents an Ant Design tabs control.
/// </summary>
public class Tabs : TemplatedControl
{
    private IList<TabPane>? _items;
    private int _selectedIndex = -1;
    private TabPane? _selectedItem;
    private string? _selectedKey;

    public Tabs()
    {
        SelectTabCommand = new DelegateCommand<TabPane>(SelectTab, static pane => pane is { IsEnabled: true });
    }

    public IList<TabPane>? Items
    {
        get => _items;
        set
        {
            _items = value;
            SelectFirstEnabledPane();
        }
    }

    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (_items is null || value < 0 || value >= _items.Count)
            {
                return;
            }

            SelectTab(_items[value]);
        }
    }

    public TabPane? SelectedItem
    {
        get => _selectedItem;
        private set => _selectedItem = value;
    }

    public string? SelectedKey
    {
        get => _selectedKey;
        private set => _selectedKey = value;
    }

    public ICommand SelectTabCommand { get; }

    private void SelectFirstEnabledPane()
    {
        if (_items is null)
        {
            return;
        }

        SelectTab(_items.OfType<TabPane>().FirstOrDefault(static pane => pane.IsEnabled));
    }

    private void SelectTab(TabPane? pane)
    {
        if (pane is null || _items is null || !pane.IsEnabled)
        {
            return;
        }

        foreach (var item in _items.OfType<TabPane>())
        {
            item.IsSelected = false;
        }

        pane.IsSelected = true;
        SelectedItem = pane;
        SelectedKey = pane.Key;
        _selectedIndex = _items.IndexOf(pane);
    }
}

/// <summary>
/// Represents a tab item.
/// </summary>
public class TabPane : ContentControl
{
    public static readonly StyledProperty<string?> KeyProperty =
        AntdProperty.Register<TabPane, string?>(nameof(Key));

    public static readonly StyledProperty<object?> HeaderProperty =
        AntdProperty.Register<TabPane, object?>(nameof(Header));

    public static readonly StyledProperty<bool> IsSelectedProperty =
        AntdProperty.Register<TabPane, bool>(nameof(IsSelected));

    public string? Key
    {
        get => GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }

    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
}

/// <summary>
/// Represents a simple Ant Design grid container.
/// </summary>
public class Grid : global::Avalonia.Controls.Grid
{
}

/// <summary>
/// Represents a spacing container.
/// </summary>
public class Space : StackPanel
{
}

/// <summary>
/// Represents a flex container.
/// </summary>
public class Flex : StackPanel
{
}

/// <summary>
/// Represents a layout shell.
/// </summary>
public class Layout : ContentControl
{
}

/// <summary>
/// Represents a splitter surface.
/// </summary>
public class Splitter : TemplatedControl
{
}

/// <summary>
/// Represents a breadcrumb.
/// </summary>
public class Breadcrumb : ItemsControl
{
}

/// <summary>
/// Represents a dropdown trigger and content pair.
/// </summary>
public class Dropdown : ContentControl
{
    public static readonly StyledProperty<object?> OverlayProperty =
        AntdProperty.Register<Dropdown, object?>(nameof(Overlay));

    public object? Overlay
    {
        get => GetValue(OverlayProperty);
        set => SetValue(OverlayProperty, value);
    }
}

/// <summary>
/// Represents a menu.
/// </summary>
public class Menu : ItemsControl
{
    public static readonly StyledProperty<AntdMenuMode> ModeProperty =
        AntdProperty.Register<Menu, AntdMenuMode>(nameof(Mode), AntdMenuMode.Vertical);

    public static readonly new StyledProperty<AntdMenuTheme> ThemeProperty =
        AntdProperty.Register<Menu, AntdMenuTheme>(nameof(Theme), AntdMenuTheme.Light);

    public AntdMenuMode Mode
    {
        get => GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    public new AntdMenuTheme Theme
    {
        get => GetValue(ThemeProperty);
        set => SetValue(ThemeProperty, value);
    }
}

/// <summary>
/// Represents steps.
/// </summary>
public class Steps : ItemsControl
{
    public static readonly StyledProperty<AntdStepsDirection> DirectionProperty =
        AntdProperty.Register<Steps, AntdStepsDirection>(nameof(Direction), AntdStepsDirection.Horizontal);

    public AntdStepsDirection Direction
    {
        get => GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }
}

/// <summary>
/// Represents an avatar.
/// </summary>
public class Avatar : ContentControl
{
    public static readonly StyledProperty<AntdAvatarShape> ShapeProperty =
        AntdProperty.Register<Avatar, AntdAvatarShape>(nameof(Shape), AntdAvatarShape.Circle);

    public AntdAvatarShape Shape
    {
        get => GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }
}

/// <summary>
/// Represents a calendar surface.
/// </summary>
public class Calendar : ContentControl
{
    public static readonly StyledProperty<DateTimeOffset?> SelectedDateProperty =
        AntdProperty.Register<Calendar, DateTimeOffset?>(nameof(SelectedDate));

    public DateTimeOffset? SelectedDate
    {
        get => GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }
}

/// <summary>
/// Represents a collapse container.
/// </summary>
public class Collapse : ItemsControl
{
}

/// <summary>
/// Represents descriptions.
/// </summary>
public class Descriptions : ItemsControl
{
    public static readonly StyledProperty<AntdDescriptionsLayout> LayoutProperty =
        AntdProperty.Register<Descriptions, AntdDescriptionsLayout>(nameof(Layout), AntdDescriptionsLayout.Horizontal);

    public AntdDescriptionsLayout Layout
    {
        get => GetValue(LayoutProperty);
        set => SetValue(LayoutProperty, value);
    }
}

/// <summary>
/// Represents a divider with optional content.
/// </summary>
public class Divider : ContentControl
{
    public static readonly StyledProperty<AntdDividerTextPlacement> TextPlacementProperty =
        AntdProperty.Register<Divider, AntdDividerTextPlacement>(nameof(TextPlacement), AntdDividerTextPlacement.Center);

    public static readonly StyledProperty<Orientation> OrientationProperty =
        AntdProperty.Register<Divider, Orientation>(nameof(Orientation), Orientation.Horizontal);

    public AntdDividerTextPlacement TextPlacement
    {
        get => GetValue(TextPlacementProperty);
        set => SetValue(TextPlacementProperty, value);
    }

    public Orientation Orientation
    {
        get => GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }
}

/// <summary>
/// Represents an empty state.
/// </summary>
public class Empty : ContentControl
{
    public static readonly StyledProperty<object?> DescriptionProperty =
        AntdProperty.Register<Empty, object?>(nameof(Description), "No data");

    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
}

/// <summary>
/// Represents a list.
/// </summary>
public class List : ItemsControl
{
    public static readonly StyledProperty<AntdListLayout> LayoutProperty =
        AntdProperty.Register<List, AntdListLayout>(nameof(Layout), AntdListLayout.Vertical);

    public AntdListLayout Layout
    {
        get => GetValue(LayoutProperty);
        set => SetValue(LayoutProperty, value);
    }
}

/// <summary>
/// Represents a segmented selector.
/// </summary>
public class Segmented : ListBox
{
    public static readonly StyledProperty<bool> BlockProperty =
        AntdProperty.Register<Segmented, bool>(nameof(Block));

    public bool Block
    {
        get => GetValue(BlockProperty);
        set => SetValue(BlockProperty, value);
    }
}

/// <summary>
/// Represents a statistic value.
/// </summary>
public class Statistic : TemplatedControl
{
    public static readonly StyledProperty<object?> TitleProperty =
        AntdProperty.Register<Statistic, object?>(nameof(Title));

    public static readonly StyledProperty<object?> ValueProperty =
        AntdProperty.Register<Statistic, object?>(nameof(Value));

    public static readonly StyledProperty<string?> ValueFormatProperty =
        AntdProperty.Register<Statistic, string?>(nameof(ValueFormat));

    public static readonly StyledProperty<object?> PrefixProperty =
        AntdProperty.Register<Statistic, object?>(nameof(Prefix));

    public static readonly StyledProperty<object?> SuffixProperty =
        AntdProperty.Register<Statistic, object?>(nameof(Suffix));

    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public string? ValueFormat
    {
        get => GetValue(ValueFormatProperty);
        set => SetValue(ValueFormatProperty, value);
    }

    public object? Prefix
    {
        get => GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }

    public object? Suffix
    {
        get => GetValue(SuffixProperty);
        set => SetValue(SuffixProperty, value);
    }

    public string FormattedValue => ValueFormat is { Length: > 0 }
        ? string.Format(System.Globalization.CultureInfo.InvariantCulture, ValueFormat, Value)
        : Convert.ToString(Value, System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty;
}

/// <summary>
/// Represents a table.
/// </summary>
public class Table : ItemsControl
{
    public static readonly StyledProperty<AntdTableSize> SizeProperty =
        AntdProperty.Register<Table, AntdTableSize>(nameof(Size), AntdTableSize.Middle);

    public AntdTableSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
}

/// <summary>
/// Represents a timeline.
/// </summary>
public class Timeline : ItemsControl
{
    public static readonly StyledProperty<AntdTimelineMode> ModeProperty =
        AntdProperty.Register<Timeline, AntdTimelineMode>(nameof(Mode), AntdTimelineMode.Left);

    public AntdTimelineMode Mode
    {
        get => GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }
}

/// <summary>
/// Represents a tooltip.
/// </summary>
public class Tooltip : ContentControl
{
    public static readonly StyledProperty<object?> TextProperty =
        AntdProperty.Register<Tooltip, object?>(nameof(Text));

    public object? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}

/// <summary>
/// Represents a watermark.
/// </summary>
public class Watermark : ContentControl
{
    public static readonly StyledProperty<string?> TextProperty =
        AntdProperty.Register<Watermark, string?>(nameof(Text));

    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}

/// <summary>
/// Represents a drawer.
/// </summary>
public class Drawer : ContentControl
{
    public static readonly StyledProperty<bool> IsOpenProperty =
        AntdProperty.Register<Drawer, bool>(nameof(IsOpen));

    public static readonly StyledProperty<AntdDrawerPlacement> PlacementProperty =
        AntdProperty.Register<Drawer, AntdDrawerPlacement>(nameof(Placement), AntdDrawerPlacement.Right);

    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public AntdDrawerPlacement Placement
    {
        get => GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }
}

/// <summary>
/// Represents a pop confirmation surface.
/// </summary>
public class Popconfirm : ContentControl
{
    public static readonly StyledProperty<bool> IsOpenProperty =
        AntdProperty.Register<Popconfirm, bool>(nameof(IsOpen));

    public static readonly StyledProperty<object?> TitleProperty =
        AntdProperty.Register<Popconfirm, object?>(nameof(Title));

    public event EventHandler? Confirm;

    public event EventHandler? Cancel;

    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public void RaiseConfirm()
    {
        IsOpen = false;
        Confirm?.Invoke(this, EventArgs.Empty);
    }

    public void RaiseCancel()
    {
        IsOpen = false;
        Cancel?.Invoke(this, EventArgs.Empty);
    }
}

/// <summary>
/// Represents a result page.
/// </summary>
public class Result : ContentControl
{
    public static readonly StyledProperty<AntdResultStatus> StatusProperty =
        AntdProperty.Register<Result, AntdResultStatus>(nameof(Status), AntdResultStatus.Info);

    public static readonly StyledProperty<object?> TitleProperty =
        AntdProperty.Register<Result, object?>(nameof(Title));

    public AntdResultStatus Status
    {
        get => GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}

/// <summary>
/// Represents a skeleton loading placeholder.
/// </summary>
public class Skeleton : TemplatedControl
{
    public static readonly StyledProperty<bool> ActiveProperty =
        AntdProperty.Register<Skeleton, bool>(nameof(Active));

    public bool Active
    {
        get => GetValue(ActiveProperty);
        set => SetValue(ActiveProperty, value);
    }
}

/// <summary>
/// Represents a spinning loading indicator.
/// </summary>
public class Spin : ContentControl
{
    public static readonly StyledProperty<bool> IsSpinningProperty =
        AntdProperty.Register<Spin, bool>(nameof(IsSpinning), true);

    public static readonly StyledProperty<object?> TipProperty =
        AntdProperty.Register<Spin, object?>(nameof(Tip));

    public bool IsSpinning
    {
        get => GetValue(IsSpinningProperty);
        set => SetValue(IsSpinningProperty, value);
    }

    public object? Tip
    {
        get => GetValue(TipProperty);
        set => SetValue(TipProperty, value);
    }
}
