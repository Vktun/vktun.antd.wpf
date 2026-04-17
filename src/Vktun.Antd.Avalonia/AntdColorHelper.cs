using Avalonia.Media;

namespace Vktun.Antd.Avalonia;

internal static class AntdColorHelper
{
    public static Color ToAvaloniaColor(AntdColor color)
    {
        return Color.FromArgb(color.A, color.R, color.G, color.B);
    }

    public static SolidColorBrush Brush(AntdColor color)
    {
        return new SolidColorBrush(ToAvaloniaColor(color));
    }
}
