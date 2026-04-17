using System;
using System.Globalization;

namespace Vktun.Antd;

/// <summary>
/// Represents a platform-neutral ARGB color token.
/// </summary>
public readonly record struct AntdColor(byte A, byte R, byte G, byte B)
{
    /// <summary>
    /// Creates an opaque color from red, green, and blue channels.
    /// </summary>
    /// <param name="red">The red channel.</param>
    /// <param name="green">The green channel.</param>
    /// <param name="blue">The blue channel.</param>
    /// <returns>The resulting color.</returns>
    public static AntdColor FromRgb(byte red, byte green, byte blue)
    {
        return new AntdColor(255, red, green, blue);
    }

    /// <summary>
    /// Creates a color from alpha, red, green, and blue channels.
    /// </summary>
    /// <param name="alpha">The alpha channel.</param>
    /// <param name="red">The red channel.</param>
    /// <param name="green">The green channel.</param>
    /// <param name="blue">The blue channel.</param>
    /// <returns>The resulting color.</returns>
    public static AntdColor FromArgb(byte alpha, byte red, byte green, byte blue)
    {
        return new AntdColor(alpha, red, green, blue);
    }

    /// <summary>
    /// Parses a color from #RRGGBB or #AARRGGBB text.
    /// </summary>
    /// <param name="hex">The hexadecimal color text.</param>
    /// <returns>The parsed color.</returns>
    /// <exception cref="ArgumentException">Thrown when the color text is invalid.</exception>
    public static AntdColor Parse(string hex)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(hex);

        var value = hex.StartsWith('#') ? hex[1..] : hex;
        return value.Length switch
        {
            6 => FromRgb(ParseByte(value, 0), ParseByte(value, 2), ParseByte(value, 4)),
            8 => FromArgb(ParseByte(value, 0), ParseByte(value, 2), ParseByte(value, 4), ParseByte(value, 6)),
            _ => throw new ArgumentException("Color text must be #RRGGBB or #AARRGGBB.", nameof(hex)),
        };
    }

    /// <summary>
    /// Returns the color as hexadecimal text.
    /// </summary>
    /// <param name="includeAlpha">Whether to include the alpha channel.</param>
    /// <returns>The hexadecimal color text.</returns>
    public string ToHex(bool includeAlpha = false)
    {
        return includeAlpha
            ? FormattableString.Invariant($"#{A:X2}{R:X2}{G:X2}{B:X2}")
            : FormattableString.Invariant($"#{R:X2}{G:X2}{B:X2}");
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return ToHex(A != 255);
    }

    private static byte ParseByte(string value, int startIndex)
    {
        return byte.Parse(value.AsSpan(startIndex, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    }
}
