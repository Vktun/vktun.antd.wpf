using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a statistical value display.
/// </summary>
public class Statistic : Control
{
    private static readonly DependencyPropertyKey FormattedValuePropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(FormattedValue), typeof(string), typeof(Statistic),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(Statistic),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Identifies the <see cref="Value"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(object), typeof(Statistic),
            new PropertyMetadata(null, OnDisplayPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="Precision"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PrecisionProperty =
        DependencyProperty.Register(nameof(Precision), typeof(int), typeof(Statistic),
            new PropertyMetadata(-1, OnDisplayPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="Prefix"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PrefixProperty =
        DependencyProperty.Register(nameof(Prefix), typeof(object), typeof(Statistic),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Suffix"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SuffixProperty =
        DependencyProperty.Register(nameof(Suffix), typeof(object), typeof(Statistic),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="ValueFormat"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueFormatProperty =
        DependencyProperty.Register(nameof(ValueFormat), typeof(string), typeof(Statistic),
            new PropertyMetadata(string.Empty, OnDisplayPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="FormattedValue"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FormattedValueProperty = FormattedValuePropertyKey.DependencyProperty;

    static Statistic()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Statistic), new FrameworkPropertyMetadata(typeof(Statistic)));
    }

    /// <summary>
    /// Gets or sets the statistic title.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the value to display.
    /// </summary>
    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the numeric precision used for formatting.
    /// </summary>
    public int Precision
    {
        get => (int)GetValue(PrecisionProperty);
        set => SetValue(PrecisionProperty, value);
    }

    /// <summary>
    /// Gets or sets the prefix content.
    /// </summary>
    public object? Prefix
    {
        get => GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }

    /// <summary>
    /// Gets or sets the suffix content.
    /// </summary>
    public object? Suffix
    {
        get => GetValue(SuffixProperty);
        set => SetValue(SuffixProperty, value);
    }

    /// <summary>
    /// Gets or sets the composite format string used for the value.
    /// </summary>
    public string ValueFormat
    {
        get => (string)GetValue(ValueFormatProperty);
        set => SetValue(ValueFormatProperty, value);
    }

    /// <summary>
    /// Gets the formatted value string.
    /// </summary>
    public string FormattedValue
    {
        get => (string)GetValue(FormattedValueProperty);
        private set => SetValue(FormattedValuePropertyKey, value);
    }

    private static void OnDisplayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Statistic statistic)
        {
            statistic.UpdateFormattedValue();
        }
    }

    private void UpdateFormattedValue()
    {
        if (Value is null)
        {
            FormattedValue = string.Empty;
            return;
        }

        if (!string.IsNullOrWhiteSpace(ValueFormat))
        {
            try
            {
                FormattedValue = string.Format(CultureInfo.InvariantCulture, ValueFormat, Value);
                return;
            }
            catch (FormatException)
            {
            }
        }

        if (Value is IFormattable && Precision >= 0 && TryConvertToDouble(Value, out var numericValue))
        {
            FormattedValue = Math.Round(numericValue, Precision, MidpointRounding.AwayFromZero)
                .ToString($"N{Precision}", CultureInfo.InvariantCulture);
            return;
        }

        FormattedValue = Value switch
        {
            IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
            _ => Value.ToString() ?? string.Empty,
        };
    }

    private static bool TryConvertToDouble(object value, out double numericValue)
    {
        try
        {
            numericValue = Convert.ToDouble(value, CultureInfo.InvariantCulture);
            return true;
        }
        catch (Exception)
        {
            numericValue = 0d;
            return false;
        }
    }
}
