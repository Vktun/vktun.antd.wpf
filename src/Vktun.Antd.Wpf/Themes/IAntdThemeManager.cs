using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides runtime theme application APIs.
/// </summary>
public interface IAntdThemeManager
{
    /// <summary>
    /// Applies the selected theme to an application scope.
    /// </summary>
    void Apply(Application application, AntdThemeMode mode, AntdSeedToken? seed = null);

    /// <summary>
    /// Applies the selected theme to an element scope.
    /// </summary>
    void Apply(FrameworkElement scope, AntdThemeMode mode, AntdSeedToken? seed = null);
}
