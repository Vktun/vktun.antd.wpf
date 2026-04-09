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
[TemplatePart(Name = "PART_Button", Type = typeof(Button))]
[TemplatePart(Name = "PART_ClearButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_Calendar", Type = typeof(Calendar))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
public class DatePicker : Control
{
    private TextBox? _textBox;
    private Calendar? _calendar;
    private Popup? _popup;
    private Button? _toggleButton;
    private Button? _clearButton;
    private bool _isSyncingPopup;

    static DatePicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DatePicker), new FrameworkPropertyMetadata(typeof(DatePicker)));
    }

    /// <summary>
    /// Identifies the <see cref="Value"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(DateTime?), typeof(DatePicker),
            new PropertyMetadata(null, OnValueChanged));

    /// <summary>
    /// Identifies the <see cref="DisplayDate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DisplayDateProperty =
        DependencyProperty.Register(nameof(DisplayDate), typeof(DateTime), typeof(DatePicker),
            new PropertyMetadata(DateTime.Today, OnDisplayDateChanged));

    /// <summary>
    /// Identifies the <see cref="IsDropDownOpen"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDropDownOpenProperty =
        DependencyProperty.Register(nameof(IsDropDownOpen), typeof(bool), typeof(DatePicker),
            new PropertyMetadata(false, OnIsDropDownOpenChanged));

    /// <summary>
    /// Identifies the <see cref="Placeholder"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(DatePicker),
            new PropertyMetadata("请选择日期"));

    /// <summary>
    /// Identifies the <see cref="Format"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FormatProperty =
        DependencyProperty.Register(nameof(Format), typeof(string), typeof(DatePicker),
            new PropertyMetadata("yyyy-MM-dd", OnValueChanged));

    /// <summary>
    /// Identifies the <see cref="Mode"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register(nameof(Mode), typeof(AntdDatePickerMode), typeof(DatePicker),
            new PropertyMetadata(AntdDatePickerMode.Date));

    /// <summary>
    /// Identifies the <see cref="ShowTime"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowTimeProperty =
        DependencyProperty.Register(nameof(ShowTime), typeof(bool), typeof(DatePicker),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsDisabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDisabledProperty =
        DependencyProperty.Register(nameof(IsDisabled), typeof(bool), typeof(DatePicker),
            new PropertyMetadata(false, OnIsDisabledChanged));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(DatePicker),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Status"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(nameof(Status), typeof(AntdStatus), typeof(DatePicker),
            new PropertyMetadata(AntdStatus.None));

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

    /// <summary>
    /// Gets or sets the selected date.
    /// </summary>
    public DateTime? Value
    {
        get => (DateTime?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected date. This is an alias of <see cref="Value"/> for compatibility.
    /// </summary>
    public DateTime? SelectedDate
    {
        get => Value;
        set => Value = value;
    }

    /// <summary>
    /// Gets or sets the calendar display date.
    /// </summary>
    public DateTime DisplayDate
    {
        get => (DateTime)GetValue(DisplayDateProperty);
        set => SetValue(DisplayDateProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the popup calendar is open.
    /// </summary>
    public bool IsDropDownOpen
    {
        get => (bool)GetValue(IsDropDownOpenProperty);
        set => SetValue(IsDropDownOpenProperty, value);
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
    /// Gets or sets the date format string.
    /// </summary>
    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    /// <summary>
    /// Gets or sets the picker mode.
    /// </summary>
    public AntdDatePickerMode Mode
    {
        get => (AntdDatePickerMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show time selection.
    /// </summary>
    public bool ShowTime
    {
        get => (bool)GetValue(ShowTimeProperty);
        set => SetValue(ShowTimeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the picker is disabled.
    /// </summary>
    public bool IsDisabled
    {
        get => (bool)GetValue(IsDisabledProperty);
        set => SetValue(IsDisabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the picker.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the semantic status.
    /// </summary>
    public AntdStatus Status
    {
        get => (AntdStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the picker allows clearing.
    /// </summary>
    public bool AllowClear
    {
        get => (bool)GetValue(AllowClearProperty);
        set => SetValue(AllowClearProperty, value);
    }

    /// <summary>
    /// Occurs when the selected date changes.
    /// </summary>
    public event RoutedEventHandler ValueChanged
    {
        add => AddHandler(ValueChangedEvent, value);
        remove => RemoveHandler(ValueChangedEvent, value);
    }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
        if (_calendar is not null)
        {
            _calendar.SelectedDatesChanged -= OnCalendarSelectedDatesChanged;
        }

        if (_popup is not null)
        {
            _popup.Opened -= OnPopupOpened;
            _popup.Closed -= OnPopupClosed;
        }

        if (_toggleButton is not null)
        {
            _toggleButton.Click -= OnToggleButtonClick;
        }

        if (_clearButton is not null)
        {
            _clearButton.Click -= OnClearButtonClick;
        }

        base.OnApplyTemplate();

        _textBox = GetTemplateChild("PART_TextBox") as TextBox;
        _calendar = GetTemplateChild("PART_Calendar") as Calendar;
        _popup = GetTemplateChild("PART_Popup") as Popup;
        _toggleButton = GetTemplateChild("PART_Button") as Button;
        _clearButton = GetTemplateChild("PART_ClearButton") as Button;

        if (_textBox is not null)
        {
            _textBox.IsReadOnly = true;
            _textBox.Text = FormatValue(Value);
        }

        if (_calendar is not null)
        {
            _calendar.SelectedDatesChanged += OnCalendarSelectedDatesChanged;
            _calendar.DisplayDate = DisplayDate;
            _calendar.SelectedDate = Value;
        }

        if (_popup is not null)
        {
            _popup.Opened += OnPopupOpened;
            _popup.Closed += OnPopupClosed;
            _popup.IsOpen = IsDropDownOpen;
        }

        if (_toggleButton is not null)
        {
            _toggleButton.Click += OnToggleButtonClick;
        }

        if (_clearButton is not null)
        {
            _clearButton.Click += OnClearButtonClick;
        }

        UpdateDisplayText();
    }

    /// <summary>
    /// Clears the selected date.
    /// </summary>
    public void Clear()
    {
        Value = null;
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not DatePicker picker)
        {
            return;
        }

        picker.UpdateDisplayText();

        if (picker._calendar is not null)
        {
            picker._calendar.SelectedDate = picker.Value;
            if (picker.Value.HasValue)
            {
                picker._calendar.DisplayDate = picker.Value.Value;
            }
        }

        picker.RaiseValueChangedEvent();
    }

    private static void OnDisplayDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DatePicker picker && picker._calendar is not null)
        {
            picker._calendar.DisplayDate = picker.DisplayDate;
        }
    }

    private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DatePicker picker && picker._popup is not null && !picker._isSyncingPopup)
        {
            picker._popup.IsOpen = picker.IsDropDownOpen;
        }
    }

    private static void OnIsDisabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DatePicker picker)
        {
            picker.IsEnabled = !picker.IsDisabled;
        }
    }

    private void OnPopupOpened(object? sender, EventArgs e)
    {
        _isSyncingPopup = true;
        try
        {
            SetCurrentValue(IsDropDownOpenProperty, true);
        }
        finally
        {
            _isSyncingPopup = false;
        }
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        _isSyncingPopup = true;
        try
        {
            SetCurrentValue(IsDropDownOpenProperty, false);
        }
        finally
        {
            _isSyncingPopup = false;
        }
    }

    private void OnToggleButtonClick(object sender, RoutedEventArgs e)
    {
        if (!IsEnabled)
        {
            return;
        }

        if (_calendar is not null)
        {
            _calendar.DisplayDate = Value ?? DisplayDate;
            _calendar.SelectedDate = Value;
        }

        IsDropDownOpen = !IsDropDownOpen;
    }

    private void OnClearButtonClick(object sender, RoutedEventArgs e)
    {
        Clear();
        IsDropDownOpen = false;
        e.Handled = true;
    }

    private void OnCalendarSelectedDatesChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_calendar?.SelectedDate is not DateTime selectedDate)
        {
            return;
        }

        DisplayDate = selectedDate;

        if (Value != selectedDate)
        {
            Value = selectedDate;
            IsDropDownOpen = false;
        }
    }

    private void UpdateDisplayText()
    {
        if (_textBox is not null)
        {
            _textBox.Text = FormatValue(Value);
        }
    }

    private string FormatValue(DateTime? value)
    {
        return value?.ToString(Format) ?? string.Empty;
    }

    private void RaiseValueChangedEvent()
    {
        RaiseEvent(new RoutedEventArgs(ValueChangedEvent, this));
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
