using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a numeric input with stepper controls.
/// </summary>
[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
[TemplatePart(Name = "PART_UpButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_DownButton", Type = typeof(Button))]
public class InputNumber : Control
{
    private TextBox? _textBox;
    private Button? _upButton;
    private Button? _downButton;

    /// <summary>
    /// Identifies the <see cref="Value"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double?), typeof(InputNumber),
            new PropertyMetadata(null, OnValueChanged));

    /// <summary>
    /// Identifies the <see cref="Minimum"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(InputNumber),
            new PropertyMetadata(double.MinValue, OnRangePropertyChanged));

    /// <summary>
    /// Identifies the <see cref="Maximum"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(InputNumber),
            new PropertyMetadata(double.MaxValue, OnRangePropertyChanged));

    /// <summary>
    /// Identifies the <see cref="Step"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StepProperty =
        DependencyProperty.Register(nameof(Step), typeof(double), typeof(InputNumber),
            new PropertyMetadata(1d));

    /// <summary>
    /// Identifies the <see cref="Precision"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PrecisionProperty =
        DependencyProperty.Register(nameof(Precision), typeof(int), typeof(InputNumber),
            new PropertyMetadata(-1, OnValueChanged));

    /// <summary>
    /// Identifies the <see cref="Placeholder"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(InputNumber),
            new PropertyMetadata("请输入数字"));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(InputNumber),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Status"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(nameof(Status), typeof(AntdStatus), typeof(InputNumber),
            new PropertyMetadata(AntdStatus.None));

    /// <summary>
    /// Identifies the <see cref="Prefix"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PrefixProperty =
        DependencyProperty.Register(nameof(Prefix), typeof(object), typeof(InputNumber),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Suffix"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SuffixProperty =
        DependencyProperty.Register(nameof(Suffix), typeof(object), typeof(InputNumber),
            new PropertyMetadata(null));

    static InputNumber()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InputNumber), new FrameworkPropertyMetadata(typeof(InputNumber)));
    }

    /// <summary>
    /// Gets or sets the current numeric value.
    /// </summary>
    public double? Value
    {
        get => (double?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the minimum allowed value.
    /// </summary>
    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    /// <summary>
    /// Gets or sets the maximum allowed value.
    /// </summary>
    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    /// <summary>
    /// Gets or sets the step delta used by the up/down controls.
    /// </summary>
    public double Step
    {
        get => (double)GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }

    /// <summary>
    /// Gets or sets the number precision used for formatting and stepping.
    /// </summary>
    public int Precision
    {
        get => (int)GetValue(PrecisionProperty);
        set => SetValue(PrecisionProperty, value);
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
    /// Gets or sets the control size.
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
    /// Gets or sets optional prefix content.
    /// </summary>
    public object? Prefix
    {
        get => GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }

    /// <summary>
    /// Gets or sets optional suffix content.
    /// </summary>
    public object? Suffix
    {
        get => GetValue(SuffixProperty);
        set => SetValue(SuffixProperty, value);
    }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
        if (_textBox is not null)
        {
            _textBox.LostFocus -= OnTextBoxLostFocus;
            _textBox.PreviewKeyDown -= OnTextBoxPreviewKeyDown;
        }

        if (_upButton is not null)
        {
            _upButton.Click -= OnUpButtonClick;
        }

        if (_downButton is not null)
        {
            _downButton.Click -= OnDownButtonClick;
        }

        base.OnApplyTemplate();

        _textBox = GetTemplateChild("PART_TextBox") as TextBox;
        _upButton = GetTemplateChild("PART_UpButton") as Button;
        _downButton = GetTemplateChild("PART_DownButton") as Button;

        if (_textBox is not null)
        {
            _textBox.LostFocus += OnTextBoxLostFocus;
            _textBox.PreviewKeyDown += OnTextBoxPreviewKeyDown;
            UpdateText();
        }

        if (_upButton is not null)
        {
            _upButton.Click += OnUpButtonClick;
        }

        if (_downButton is not null)
        {
            _downButton.Click += OnDownButtonClick;
        }
    }

    /// <summary>
    /// Increments the value by <see cref="Step"/>.
    /// </summary>
    public void Increment()
    {
        StepBy(Math.Abs(Step));
    }

    /// <summary>
    /// Decrements the value by <see cref="Step"/>.
    /// </summary>
    public void Decrement()
    {
        StepBy(-Math.Abs(Step));
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not InputNumber inputNumber)
        {
            return;
        }

        var coerced = inputNumber.Coerce(inputNumber.Value);
        if (coerced != inputNumber.Value)
        {
            inputNumber.SetCurrentValue(ValueProperty, coerced);
            return;
        }

        inputNumber.UpdateText();
    }

    private static void OnRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is InputNumber inputNumber)
        {
            inputNumber.SetCurrentValue(ValueProperty, inputNumber.Coerce(inputNumber.Value));
        }
    }

    private void OnUpButtonClick(object sender, RoutedEventArgs e)
    {
        Increment();
    }

    private void OnDownButtonClick(object sender, RoutedEventArgs e)
    {
        Decrement();
    }

    private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
    {
        CommitText();
    }

    private void OnTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        CommitText();
        e.Handled = true;
    }

    private void CommitText()
    {
        if (_textBox is null)
        {
            return;
        }

        var text = _textBox.Text?.Trim();
        if (string.IsNullOrEmpty(text))
        {
            Value = null;
            return;
        }

        if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed) ||
            double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out parsed))
        {
            Value = Coerce(parsed);
            return;
        }

        UpdateText();
    }

    private void StepBy(double delta)
    {
        var current = Value ?? (Minimum != double.MinValue ? Minimum : 0d);
        Value = Coerce(current + delta);
    }

    private double? Coerce(double? value)
    {
        if (!value.HasValue)
        {
            return null;
        }

        var clamped = Math.Clamp(value.Value, Minimum, Maximum);
        return Precision >= 0 ? Math.Round(clamped, Precision, MidpointRounding.AwayFromZero) : clamped;
    }

    private void UpdateText()
    {
        if (_textBox is null)
        {
            return;
        }

        _textBox.Text = Value.HasValue ? FormatValue(Value.Value) : string.Empty;
    }

    private string FormatValue(double value)
    {
        if (Precision >= 0)
        {
            return value.ToString($"F{Precision}", CultureInfo.InvariantCulture);
        }

        return value.ToString("0.############################", CultureInfo.InvariantCulture);
    }
}
