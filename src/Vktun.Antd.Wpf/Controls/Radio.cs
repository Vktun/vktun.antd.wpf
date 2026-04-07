using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a radio button style for radio group.
/// </summary>
public enum AntdRadioButtonStyle
{
    /// <summary>
    /// Default radio button style with circle indicator.
    /// </summary>
    Default,
    /// <summary>
    /// Solid button style.
    /// </summary>
    Solid,
    /// <summary>
    /// Outline button style.
    /// </summary>
    Outline,
}

/// <summary>
/// Represents a radio button component with Ant Design styling.
/// </summary>
public class Radio : RadioButton
{
    static Radio()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Radio), new FrameworkPropertyMetadata(typeof(Radio)));
    }

    /// <summary>
    /// Gets or sets the button style for radio.
    /// </summary>
    public AntdRadioButtonStyle ButtonStyle
    {
        get => (AntdRadioButtonStyle)GetValue(ButtonStyleProperty);
        set => SetValue(ButtonStyleProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="ButtonStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ButtonStyleProperty =
        DependencyProperty.Register(nameof(ButtonStyle), typeof(AntdRadioButtonStyle), typeof(Radio),
            new PropertyMetadata(AntdRadioButtonStyle.Default));
}

/// <summary>
/// Represents a group of radio buttons with Ant Design styling.
/// </summary>
public class RadioGroup : ItemsControl
{
    static RadioGroup()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioGroup), new FrameworkPropertyMetadata(typeof(RadioGroup)));
    }

    /// <summary>
    /// Gets or sets the selected value.
    /// </summary>
    public object Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Value"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(object), typeof(RadioGroup),
            new PropertyMetadata(null, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RadioGroup group)
        {
            group.UpdateRadioStates();
            group.RaiseValueChangedEvent();
        }
    }

    /// <summary>
    /// Gets or sets the button style for all radios in the group.
    /// </summary>
    public AntdRadioButtonStyle ButtonStyle
    {
        get => (AntdRadioButtonStyle)GetValue(ButtonStyleProperty);
        set => SetValue(ButtonStyleProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="ButtonStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ButtonStyleProperty =
        DependencyProperty.Register(nameof(ButtonStyle), typeof(AntdRadioButtonStyle), typeof(RadioGroup),
            new PropertyMetadata(AntdRadioButtonStyle.Default));

    /// <summary>
    /// Gets or sets whether the radio group is disabled.
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
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(RadioGroup),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the radio group uses button style.
    /// </summary>
    public bool IsButtonStyle
    {
        get => (bool)GetValue(IsButtonStyleProperty);
        set => SetValue(IsButtonStyleProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsButtonStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsButtonStyleProperty =
        DependencyProperty.Register(nameof(IsButtonStyle), typeof(bool), typeof(RadioGroup),
            new PropertyMetadata(false));

    /// <summary>
    /// Raised when the value changes.
    /// </summary>
    public static readonly RoutedEvent ValueChangedEvent =
        EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(RadioGroup));

    public event RoutedEventHandler ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    /// <summary>
    /// Gets or sets the options for the radio group when not using Items.
    /// </summary>
    public System.Collections.IEnumerable Options
    {
        get => (System.Collections.IEnumerable)GetValue(OptionsProperty);
        set => SetValue(OptionsProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Options"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OptionsProperty =
        DependencyProperty.Register(nameof(Options), typeof(System.Collections.IEnumerable), typeof(RadioGroup),
            new PropertyMetadata(null, OnOptionsChanged));

    private static void OnOptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RadioGroup group && e.NewValue is System.Collections.IEnumerable options)
        {
            group.ItemsSource = options;
        }
    }

    private void UpdateRadioStates()
    {
        foreach (var item in Items)
        {
            if (item is Radio radio)
            {
                var radioValue = GetRadioValue(radio);
                radio.IsChecked = Equals(Value, radioValue);
            }
        }
    }

    private object GetRadioValue(Radio radio)
    {
        return radio.Tag ?? radio.Content;
    }

    /// <summary>
    /// Called when a child radio is selected.
    /// </summary>
    internal void OnChildRadioSelected(Radio radio)
    {
        Value = GetRadioValue(radio);
    }

    private void RaiseValueChangedEvent()
    {
        RaiseEvent(new RoutedEventArgs(ValueChangedEvent, this));
    }

    /// <summary>
    /// Called when an item container is generated.
    /// </summary>
    protected override DependencyObject GetContainerForItemOverride()
    {
        return new Radio();
    }

    /// <summary>
    /// Determines if an item is its own container.
    /// </summary>
    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is Radio;
    }
}