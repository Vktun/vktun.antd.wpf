using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WpfCalendar = System.Windows.Controls.Calendar;

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
[TemplatePart(Name = "PART_Calendar", Type = typeof(WpfCalendar))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
public class DatePicker : Control
{
    private TextBox? _textBox;
    private WpfCalendar? _calendar;
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
            new PropertyMetadata("\u8BF7\u9009\u62E9\u65E5\u671F"));

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
        _calendar = GetTemplateChild("PART_Calendar") as WpfCalendar;
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

    /// <inheritdoc />
    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseLeftButtonDown(e);

        if (!IsEnabled || IsDropDownOpen || e.Handled)
        {
            return;
        }

        if (IsSourceWithin(e.OriginalSource as DependencyObject, _toggleButton) ||
            IsSourceWithin(e.OriginalSource as DependencyObject, _clearButton))
        {
            return;
        }

        OpenDropDown();
    }

    /// <inheritdoc />
    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        if (!IsEnabled)
        {
            return;
        }

        if (e.Key == Key.Escape && IsDropDownOpen)
        {
            IsDropDownOpen = false;
            e.Handled = true;
            return;
        }

        if (e.Key == Key.F4 || (e.Key == Key.Down && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt))
        {
            if (IsDropDownOpen)
            {
                IsDropDownOpen = false;
            }
            else
            {
                OpenDropDown();
            }

            e.Handled = true;
        }
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

        if (IsDropDownOpen)
        {
            IsDropDownOpen = false;
            return;
        }

        OpenDropDown();
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

    private void OpenDropDown()
    {
        if (_calendar is not null)
        {
            _calendar.DisplayDate = Value ?? DisplayDate;
            _calendar.SelectedDate = Value;
        }

        IsDropDownOpen = true;
    }

    private static bool IsSourceWithin(DependencyObject? source, DependencyObject? target)
    {
        if (source is null || target is null)
        {
            return false;
        }

        var current = source;
        while (current is not null)
        {
            if (ReferenceEquals(current, target))
            {
                return true;
            }

            current = current switch
            {
                Visual visual => VisualTreeHelper.GetParent(visual),
                Visual3D visual3D => VisualTreeHelper.GetParent(visual3D),
                _ => LogicalTreeHelper.GetParent(current),
            };
        }

        return false;
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
[TemplatePart(Name = "PART_Button", Type = typeof(Button))]
[TemplatePart(Name = "PART_Calendar", Type = typeof(WpfCalendar))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
public class RangePicker : Control
{
    private Popup? _popup;
    private WpfCalendar? _calendar;
    private Button? _toggleButton;
    private bool _isSyncingPopup;
    private bool _isSelectingEnd;

    static RangePicker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RangePicker), new FrameworkPropertyMetadata(typeof(RangePicker)));
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
    /// Identifies the <see cref="IsDropDownOpen"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsDropDownOpenProperty =
        DependencyProperty.Register(nameof(IsDropDownOpen), typeof(bool), typeof(RangePicker),
            new PropertyMetadata(false, OnIsDropDownOpenChanged));

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
            new PropertyMetadata(null, OnStartValueChanged));

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
            new PropertyMetadata(null, OnEndValueChanged));

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
            new PropertyMetadata("\u5F00\u59CB\u65E5\u671F"));

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
            new PropertyMetadata("\u7ED3\u675F\u65E5\u671F"));

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
            new PropertyMetadata(false, OnIsDisabledChanged));

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
        IsDropDownOpen = false;
        _isSelectingEnd = false;
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

        base.OnApplyTemplate();

        _popup = GetTemplateChild("PART_Popup") as Popup;
        _calendar = GetTemplateChild("PART_Calendar") as WpfCalendar;
        _toggleButton = GetTemplateChild("PART_Button") as Button;

        EnsurePopupAndCalendar();

        if (_calendar is not null)
        {
            _calendar.SelectionMode = CalendarSelectionMode.SingleDate;
            _calendar.SelectedDatesChanged += OnCalendarSelectedDatesChanged;
            UpdateCalendarDisplay();
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
    }

    /// <inheritdoc />
    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseLeftButtonDown(e);

        if (!IsEnabled || IsDropDownOpen || e.Handled)
        {
            return;
        }

        if (IsSourceWithin(e.OriginalSource as DependencyObject, _toggleButton))
        {
            return;
        }

        OpenDropDown();
    }

    /// <inheritdoc />
    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        if (!IsEnabled)
        {
            return;
        }

        if (e.Key == Key.Escape && IsDropDownOpen)
        {
            IsDropDownOpen = false;
            e.Handled = true;
            return;
        }

        if (e.Key == Key.F4 || (e.Key == Key.Down && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt))
        {
            if (IsDropDownOpen)
            {
                IsDropDownOpen = false;
            }
            else
            {
                OpenDropDown();
            }

            e.Handled = true;
        }
    }

    private static void OnStartValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not RangePicker picker)
        {
            return;
        }

        if (picker.StartValue.HasValue && picker.EndValue.HasValue && picker.StartValue.Value > picker.EndValue.Value)
        {
            picker.SetCurrentValue(EndValueProperty, picker.StartValue);
        }

        picker.UpdateCalendarDisplay();
    }

    private static void OnEndValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not RangePicker picker)
        {
            return;
        }

        if (picker.StartValue.HasValue && picker.EndValue.HasValue && picker.EndValue.Value < picker.StartValue.Value)
        {
            picker.SetCurrentValue(StartValueProperty, picker.EndValue);
        }

        picker.UpdateCalendarDisplay();
    }

    private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RangePicker picker && picker._popup is not null && !picker._isSyncingPopup)
        {
            picker._popup.IsOpen = picker.IsDropDownOpen;
        }
    }

    private static void OnIsDisabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RangePicker picker)
        {
            picker.IsEnabled = !picker.IsDisabled;
        }
    }

    private void OnToggleButtonClick(object sender, RoutedEventArgs e)
    {
        if (!IsEnabled)
        {
            return;
        }

        if (IsDropDownOpen)
        {
            IsDropDownOpen = false;
            return;
        }

        OpenDropDown();
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

    private void OnCalendarSelectedDatesChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_calendar?.SelectedDate is not DateTime selectedDate)
        {
            return;
        }

        if (!_isSelectingEnd)
        {
            SetCurrentValue(StartValueProperty, selectedDate);
            SetCurrentValue(EndValueProperty, null);
            _isSelectingEnd = true;
            return;
        }

        if (StartValue is null)
        {
            SetCurrentValue(StartValueProperty, selectedDate);
            return;
        }

        if (selectedDate < StartValue.Value)
        {
            SetCurrentValue(EndValueProperty, StartValue);
            SetCurrentValue(StartValueProperty, selectedDate);
        }
        else
        {
            SetCurrentValue(EndValueProperty, selectedDate);
        }

        _isSelectingEnd = false;
        IsDropDownOpen = false;
    }

    private void OpenDropDown()
    {
        _isSelectingEnd = StartValue.HasValue && !EndValue.HasValue;
        UpdateCalendarDisplay();
        IsDropDownOpen = true;
    }

    private void UpdateCalendarDisplay()
    {
        if (_calendar is null)
        {
            return;
        }

        var anchorDate = EndValue ?? StartValue ?? DateTime.Today;
        _calendar.DisplayDate = anchorDate;
        _calendar.SelectedDate = EndValue ?? StartValue;
    }

    private void EnsurePopupAndCalendar()
    {
        _calendar ??= new WpfCalendar
        {
            SelectionMode = CalendarSelectionMode.SingleDate,
            Margin = new Thickness(8),
            Width = 270,
            BorderThickness = new Thickness(0),
        };

        if (_popup is null)
        {
            _popup = new Popup
            {
                PlacementTarget = this,
                Placement = PlacementMode.Bottom,
                StaysOpen = false,
                AllowsTransparency = true,
                PopupAnimation = PopupAnimation.Fade,
                Child = CreatePopupContainer(_calendar),
            };

            return;
        }

        if (_popup.Child is null)
        {
            _popup.Child = CreatePopupContainer(_calendar);
        }
    }

    private static Border CreatePopupContainer(WpfCalendar calendar)
    {
        var border = new Border
        {
            Margin = new Thickness(0, 8, 0, 0),
            Padding = new Thickness(10),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(8),
            Child = calendar,
        };

        border.SetResourceReference(Border.BackgroundProperty, "Antd.Brush.BgElevated");
        border.SetResourceReference(Border.BorderBrushProperty, "Antd.Brush.BorderSecondary");
        border.SetResourceReference(UIElement.EffectProperty, "Antd.Shadow.Popup");
        return border;
    }

    private static bool IsSourceWithin(DependencyObject? source, DependencyObject? target)
    {
        if (source is null || target is null)
        {
            return false;
        }

        var current = source;
        while (current is not null)
        {
            if (ReferenceEquals(current, target))
            {
                return true;
            }

            current = current switch
            {
                Visual visual => VisualTreeHelper.GetParent(visual),
                Visual3D visual3D => VisualTreeHelper.GetParent(visual3D),
                _ => LogicalTreeHelper.GetParent(current),
            };
        }

        return false;
    }
}
