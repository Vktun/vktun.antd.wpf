using Avalonia;

namespace Vktun.Antd.Avalonia;

/// <summary>
/// Default Avalonia theme manager for Ant Design token resources.
/// </summary>
public sealed class AntdThemeManager : IAntdThemeManager
{
    /// <summary>
    /// Gets the shared theme manager instance.
    /// </summary>
    public static AntdThemeManager Current { get; } = new();

    private AntdThemeManager()
    {
    }

    /// <inheritdoc />
    public void Apply(Application application, AntdThemeMode theme, AntdSeedToken? seed = null)
    {
        ArgumentNullException.ThrowIfNull(application);

        var resources = application.Resources.MergedDictionaries
            .OfType<AntdThemeResources>()
            .FirstOrDefault();

        if (resources is null)
        {
            resources = new AntdThemeResources();
            application.Resources.MergedDictionaries.Add(resources);
        }

        resources.Theme = theme;
        resources.Seed = seed ?? AntdSeedToken.Default;
    }
}
