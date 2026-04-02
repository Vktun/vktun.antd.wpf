using System.Windows.Media;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Defines the seed values used to derive semantic theme resources.
/// </summary>
public sealed class AntdSeedToken
{
    /// <summary>
    /// Gets or sets the primary brand color.
    /// </summary>
    public Color PrimaryColor { get; init; } = (Color)ColorConverter.ConvertFromString("#1677FF");

    /// <summary>
    /// Gets or sets the success color.
    /// </summary>
    public Color SuccessColor { get; init; } = (Color)ColorConverter.ConvertFromString("#52C41A");

    /// <summary>
    /// Gets or sets the warning color.
    /// </summary>
    public Color WarningColor { get; init; } = (Color)ColorConverter.ConvertFromString("#FAAD14");

    /// <summary>
    /// Gets or sets the error color.
    /// </summary>
    public Color ErrorColor { get; init; } = (Color)ColorConverter.ConvertFromString("#FF4D4F");

    /// <summary>
    /// Gets or sets the base font size.
    /// </summary>
    public double FontSizeBase { get; init; } = 14d;

    /// <summary>
    /// Gets or sets the small control height.
    /// </summary>
    public double ControlHeightSmall { get; init; } = 32d;

    /// <summary>
    /// Gets or sets the default control height.
    /// </summary>
    public double ControlHeightMiddle { get; init; } = 40d;

    /// <summary>
    /// Gets or sets the large control height.
    /// </summary>
    public double ControlHeightLarge { get; init; } = 48d;

    /// <summary>
    /// Gets or sets the default radius.
    /// </summary>
    public double BorderRadius { get; init; } = 8d;

    /// <summary>
    /// Gets a default token set.
    /// </summary>
    public static AntdSeedToken Default { get; } = new();
}
