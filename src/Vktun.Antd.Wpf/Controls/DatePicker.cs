using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents the picker type for date selection.
/// </summary>
public enum AntdDatePickerMode
{
    Date,
    Week,
    Month,
    Quarter,
    Year,
}

/// <summary>
/// Represents a date picker component with Ant Design styling.
/// </summary>
[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
[TemplatePart(Name = "PART_Calendar", Type = typeof(Calendar))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
public class DatePicker : Control
{
    static DatePicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DatePicker), new FrameworkPropertyMetadata(typeof(DatePicker)));
    }

    private TextBox? _textBox;
    private Calendar? _calendar;
    private Popup? _popup;

    /// <summary>
    /// Gets or sets the selected date.
    /// </summary>
    public DateTime? Value
    {
        get => (DateTime?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Value"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(DateTime?), typeof(DatePicker),
            new PropertyMetadata(null, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DatePicker picker)
        {
            picker.UpdateDisplayText();
            picker.RaiseValueChangedEvent();
        }
    }

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Placeholder"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(DatePicker),
            new PropertyMetadata("请选择日期"));

    /// <summary>
    /// Gets or sets the date format string.
    /// </summary>
    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Format"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FormatProperty =
        DependencyProperty.Register(nameof(Format), typeof(string), typeof(DatePicker),
            new PropertyMetadata("yyyy-MM-dd"));

    /// <summary>
    /// Gets or sets the picker mode.
    /// </summary>
    public AntdDatePickerMode Mode
    {
        get => (AntdDatePickerMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Mode"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register(nameof(Mode), typeof(AntdDatePickerMode), typeof(DatePicker),
            new PropertyMetadata(AntdDatePickerMode.Date));

    /// <summary>
    /// Gets or sets whether to show time selection.
    /// </summary>
    public bool ShowTime
    {
        get => (bool)GetValue(ShowTimeProperty);
        set => SetValue(ShowTimeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="ShowTime"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowTimeProperty =
        DependencyProperty.Register(nameof(ShowTime), typeof(bool), typeof(DatePicker),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the picker is disabled.
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
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(DatePicker),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the size of the picker.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(DatePicker),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Gets or sets whether the picker allows clearing.
    /// </summary>
    public bool AllowClear
    {
        get => (bool)GetValue(AllowClearProperty);
        set => SetValue(AllowClearProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="AllowClear"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AllowClearProperty =
        DependencyProperty.Register(nameof(AllowClear), typeof(bool), typeof(DatePicker),
            new PropertyMetadata(true));

    /// <summary>
    /// Raised when the value changes.
    /// </summary>
    public static readonly RoutedEvent ValueChangedEvent =
        EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(DatePicker));

    public event RoutedEventHandler ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    private void RaiseValueChangedEvent()
    {
        RaiseEvent(new RoutedEventArgs(ValueChangedEvent, this));
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _textBox = GetTemplateChild("PART_TextBox") as TextBox;
        _calendar = GetTemplateChild("PART_Calendar") as Calendar;
        _popup = GetTemplateChild("PART_Popup") as Popup;

        if (_calendar != null)
        {
            _calendar.SelectedDatesChanged += OnCalendarSelectedDatesChanged;
        }

        UpdateDisplayText();
    }

    private void OnCalendarSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_calendar?.SelectedDate != null)
        {
            Value = _calendar.SelectedDate.Value;
            if (_popup != null)
            {
                _popup.IsOpen = false;
            }
        }
    }

    private void UpdateDisplayText()
    {
        if (_textBox != null)
        {
            _textBox.Text = Value?.ToString(Format) ?? string.Empty;
        }
    }

    /// <summary>
    /// Clears the selected date.
    /// </summary>
    public void Clear()
    {
        Value = null;
    }
}

/// <summary>
/// Represents a date range picker component with Ant Design styling.
/// </summary>
[TemplatePart(Name = "PART_StartTextBox", Type = typeof(TextBox))]
[TemplatePart(Name = "PART_EndTextBox", Type = typeof(TextBox))]
[TemplatePart(Name = "PART_Calendar", Type = typeof(Calendar))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
public class RangePicker : Control
{
    static RangePicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RangePicker), new FrameworkPropertyMetadata(typeof(RangePicker)));
    }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime? StartValue
    {
        get => (DateTime?)GetValue(StartValueProperty);
        set => SetValue(StartValueProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="StartValue"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StartValueProperty =
        DependencyProperty.Register(nameof(StartValue), typeof(DateTime?), typeof(RangePicker),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime? EndValue
    {
        get => (DateTime?)GetValue(EndValueProperty);
        set => SetValue(EndValueProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="EndValue"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EndValueProperty =
        DependencyProperty.Register(nameof(EndValue), typeof(DateTime?), typeof(RangePicker),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the placeholder text for start date.
    /// </summary>
    public string StartPlaceholder
    {
        get => (string)GetValue(StartPlaceholderProperty);
        set => SetValue(StartPlaceholderProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="StartPlaceholder"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StartPlaceholderProperty =
        DependencyProperty.Register(nameof(StartPlaceholder), typeof(string), typeof(RangePicker),
            new PropertyMetadata("开始日期"));

    /// <summary>
    /// Gets or sets the placeholder text for end date.
    /// </summary>
    public string EndPlaceholder
    {
        get => (string)GetValue(EndPlaceholderProperty);
        set => SetValue(EndPlaceholderProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="EndPlaceholder"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EndPlaceholderProperty =
        DependencyProperty.Register(nameof(EndPlaceholder), typeof(string), typeof(RangePicker),
            new PropertyMetadata("结束日期"));

    /// <summary>
    /// Gets or sets the date format string.
    /// </summary>
    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Format"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FormatProperty =
        DependencyProperty.Register(nameof(Format), typeof(string), typeof(RangePicker),
            new PropertyMetadata("yyyy-MM-dd"));

    /// <summary>
    /// Gets or sets whether the picker is disabled.
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
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(RangePicker),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the size of the picker.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(RangePicker),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Gets or sets whether the picker allows clearing.
    /// </summary>
    public bool AllowClear
    {
        get => (bool)GetValue(AllowClearProperty);
        set => SetValue(AllowClearProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="AllowClear"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AllowClearProperty =
        DependencyProperty.Register(nameof(AllowClear), typeof(bool), typeof(RangePicker),
            new PropertyMetadata(true));

    /// <summary>
    /// Clears the selected dates.
    /// </summary>
    public void Clear()
    {
        StartValue = null;
        EndValue = null;
    }
}