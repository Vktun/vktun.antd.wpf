using System.Windows.Media;

namespace Vktun.Antd.Wpf;

internal static class AntdColorHelper
{
    public static Color ToWpfColor(AntdColor color)
    {
        return Color.FromArgb(color.A, color.R, color.G, color.B);
    }

    public static SolidColorBrush Brush(AntdColor color)
    {
        var brush = new SolidColorBrush(ToWpfColor(color));
        brush.Freeze();
        return brush;
    }
}
