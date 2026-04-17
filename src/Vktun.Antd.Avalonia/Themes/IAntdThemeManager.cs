using Avalonia;

namespace Vktun.Antd.Avalonia;

/// <summary>
/// Applies generated Ant Design token resources to an Avalonia application.
/// </summary>
public interface IAntdThemeManager
{
    /// <summary>
    /// Applies a theme to the application.
    /// </summary>
    void Apply(Application application, AntdThemeMode theme, AntdSeedToken? seed = null);
}
