using System;
using System.Windows.Media;

namespace Vktun.Antd.Wpf;

internal static class AntdColorHelper
{
    public static Color Blend(Color from, Color to, double ratio)
    {
        ratio = Math.Clamp(ratio, 0d, 1d);
        return Color.FromArgb(
            (byte)Math.Round(from.A + ((to.A - from.A) * ratio)),
            (byte)Math.Round(from.R + ((to.R - from.R) * ratio)),
            (byte)Math.Round(from.G + ((to.G - from.G) * ratio)),
            (byte)Math.Round(from.B + ((to.B - from.B) * ratio)));
    }

    public static Color WithOpacity(Color color, double opacity)
    {
        return Color.FromArgb((byte)Math.Round(255d * Math.Clamp(opacity, 0d, 1d)), color.R, color.G, color.B);
    }

    public static SolidColorBrush Brush(Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze();
        return brush;
    }
}
