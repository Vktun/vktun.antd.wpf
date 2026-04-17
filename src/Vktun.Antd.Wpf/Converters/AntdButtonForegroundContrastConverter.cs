using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Vktun.Antd.Wpf;

public sealed class AntdButtonForegroundContrastConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is null || values.Length < 4)
        {
            return DependencyProperty.UnsetValue;
        }

        if (values[0] is not Brush backgroundBrush || values[1] is not Brush foregroundBrush)
        {
            return values.Length > 1 && values[1] is Brush fallbackBrush ? fallbackBrush : Brushes.Black;
        }

        if (values[2] is not AntdButtonType type || values[3] is not AntdStatus status)
        {
            return foregroundBrush;
        }

        // Primary / status states are already explicit semantic colors in style triggers.
        if (type == AntdButtonType.Primary || status != AntdStatus.None)
        {
            return foregroundBrush;
        }

        if (!TryGetColor(backgroundBrush, out var backgroundColor) || !TryGetColor(foregroundBrush, out var foregroundColor))
        {
            return foregroundBrush;
        }

        // 0.5: transparent/near transparent backgrounds keep explicit foreground.
        if (backgroundColor.A == 0)
        {
            return foregroundBrush;
        }

        if (GetContrastRatio(foregroundColor, backgroundColor) >= 4.5)
        {
            return foregroundBrush;
        }

        var whiteContrast = GetContrastRatio(Colors.White, backgroundColor);
        var blackContrast = GetContrastRatio(Colors.Black, backgroundColor);
        return whiteContrast >= blackContrast ? Brushes.White : Brushes.Black;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private static bool TryGetColor(Brush brush, out Color color)
    {
        if (brush is SolidColorBrush solidColorBrush)
        {
            color = solidColorBrush.Color;
            return true;
        }

        color = default;
        return false;
    }

    private static double GetContrastRatio(Color foreground, Color background)
    {
        var foreLum = GetLuminance(foreground);
        var backLum = GetLuminance(background);
        var lighter = Math.Max(foreLum, backLum);
        var darker = Math.Min(foreLum, backLum);
        return (lighter + 0.05) / (darker + 0.05);
    }

    private static double GetLuminance(Color color)
    {
        return 0.2126 * ConvertToLinear(color.R / 255d)
            + 0.7152 * ConvertToLinear(color.G / 255d)
            + 0.0722 * ConvertToLinear(color.B / 255d);
    }

    private static double ConvertToLinear(double value)
    {
        return value <= 0.03928 ? value / 12.92 : Math.Pow((value + 0.055) / 1.055, 2.4);
    }
}
