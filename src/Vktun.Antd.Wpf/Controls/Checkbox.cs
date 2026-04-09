using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a checkbox component with Ant Design styling.
/// </summary>
public class Checkbox : CheckBox
{
    static Checkbox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Checkbox), new FrameworkPropertyMetadata(typeof(Checkbox)));
    }

    /// <summary>
    /// Gets or sets whether the checkbox is in indeterminate state.
    /// </summary>
    public new bool Indeterminate
    {
        get => (bool)GetValue(IndeterminateProperty);
        set => SetValue(IndeterminateProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Indeterminate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IndeterminateProperty =
        DependencyProperty.Register(nameof(Indeterminate), typeof(bool), typeof(Checkbox),
            new PropertyMetadata(false, OnIndeterminateChanged));

    private static void OnIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Checkbox checkbox)
        {
            checkbox.IsThreeState = checkbox.Indeterminate;
            if (checkbox.Indeterminate && !checkbox.IsChecked.HasValue)
            {
                checkbox.IsChecked = null;
            }
        }
    }
}

/// <summary>
/// Represents a group of checkboxes with Ant Design styling.
/// </summary>
public class CheckboxGroup : ItemsControl
{
    static CheckboxGroup()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckboxGroup), new FrameworkPropertyMetadata(typeof(CheckboxGroup)));
    }

    /// <summary>
    /// Gets or sets the selected values.
    /// </summary>
    public IList Value
    {
        get => (IList)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Value"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(IList), typeof(CheckboxGroup),
            new PropertyMetadata(null, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CheckboxGroup group)
        {
            group.UpdateCheckboxStates();
        }
    }

    /// <summary>
    /// Gets or sets whether the checkbox group is disabled.
    /// </summary>
    public bool IsDisabled
    {
        get => (bool)GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsDisabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDisabledProperty =
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(CheckboxGroup),
            new PropertyMetadata(false));

    /// <summary>
    /// Raised when the value changes.
    /// </summary>
    public static readonly RoutedEvent ValueChangedEvent =
        EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(CheckboxGroup));

    public event RoutedEventHandler ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    /// <summary>
    /// Gets or sets the options for the checkbox group when not using Items.
    /// </summary>
    public IEnumerable Options
    {
        get => (IEnumerable)GetValue(OptionsProperty);
        set => SetValue(OptionsProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Options"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OptionsProperty =
        DependencyProperty.Register(nameof(Options), typeof(IEnumerable), typeof(CheckboxGroup),
            new PropertyMetadata(null, OnOptionsChanged));

    private static void OnOptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CheckboxGroup group && e.NewValue is IEnumerable options)
        {
            group.ItemsSource = options;
        }
    }

    private void UpdateCheckboxStates()
    {
        if (Value == null) return;

        foreach (var item in Items)
        {
            if (item is Checkbox checkbox)
            {
                var checkboxValue = GetCheckboxValue(checkbox);
                checkbox.IsChecked = Value.Contains(checkboxValue);
            }
        }
    }

    private object GetCheckboxValue(Checkbox checkbox)
    {
        return checkbox.Tag ?? checkbox.Content;
    }

    /// <summary>
    /// Called when a child checkbox is checked/unchecked.
    /// </summary>
    internal void OnChildCheckboxChanged(Checkbox checkbox)
    {
        if (Value == null)
        {
            Value = new ArrayList();
        }

        var checkboxValue = GetCheckboxValue(checkbox);

        if (checkbox.IsChecked == true && !Value.Contains(checkboxValue))
        {
            Value.Add(checkboxValue);
        }
        else if (checkbox.IsChecked == false && Value.Contains(checkboxValue))
        {
            Value.Remove(checkboxValue);
        }

        RaiseValueChangedEvent();
    }

    private void RaiseValueChangedEvent()
    {
        RaiseEvent(new RoutedEventArgs(ValueChangedEvent, this));
    }
}

/// <summary>
/// Represents a checkbox option for use in CheckboxGroup.
/// </summary>
public class CheckboxOption : ContentControl
{
    static CheckboxOption()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckboxOption), new FrameworkPropertyMetadata(typeof(CheckboxOption)));
    }

    /// <summary>
    /// Gets or sets the value of the checkbox option.
    /// </summary>
    public object OptionValue
    {
        get => GetValue(OptionValueProperty);
        set => SetValue(OptionValueProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="OptionValue"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OptionValueProperty =
        DependencyProperty.Register(nameof(OptionValue), typeof(object), typeof(CheckboxOption),
            new PropertyMetadata(null));
}
