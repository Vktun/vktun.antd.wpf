using System;

namespace Vktun.Antd;

internal static class AntdColorMath
{
    public static AntdColor Blend(AntdColor from, AntdColor to, double ratio)
    {
        ratio = Math.Clamp(ratio, 0d, 1d);
        return AntdColor.FromArgb(
            (byte)Math.Round(from.A + ((to.A - from.A) * ratio)),
            (byte)Math.Round(from.R + ((to.R - from.R) * ratio)),
            (byte)Math.Round(from.G + ((to.G - from.G) * ratio)),
            (byte)Math.Round(from.B + ((to.B - from.B) * ratio)));
    }

    public static AntdColor WithOpacity(AntdColor color, double opacity)
    {
        return AntdColor.FromArgb((byte)Math.Round(255d * Math.Clamp(opacity, 0d, 1d)), color.R, color.G, color.B);
    }
}
