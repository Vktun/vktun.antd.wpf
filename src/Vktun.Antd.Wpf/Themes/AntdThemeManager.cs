using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Applies and updates <see cref="AntdThemeResources"/> instances.
/// </summary>
public sealed class AntdThemeManager : IAntdThemeManager
{
    /// <summary>
    /// Gets the shared theme manager instance.
    /// </summary>
    public static AntdThemeManager Current { get; } = new();

    /// <inheritdoc />
    public void Apply(Application application, AntdThemeMode mode, AntdSeedToken? seed = null)
    {
        ArgumentNullException.ThrowIfNull(application);
        var resources = FindOrCreate(application.Resources);
        resources.Theme = mode;
        resources.Seed = seed ?? AntdSeedToken.Default;
    }

    /// <inheritdoc />
    public void Apply(FrameworkElement scope, AntdThemeMode mode, AntdSeedToken? seed = null)
    {
        ArgumentNullException.ThrowIfNull(scope);
        var resources = FindOrCreate(scope.Resources);
        resources.Theme = mode;
        resources.Seed = seed ?? AntdSeedToken.Default;
    }

    private static AntdThemeResources FindOrCreate(ResourceDictionary resources)
    {
        var match = Find(resources);
        if (match is not null)
        {
            return match;
        }

        var created = new AntdThemeResources();
        resources.MergedDictionaries.Add(created);
        return created;
    }

    private static AntdThemeResources? Find(ResourceDictionary resources)
    {
        foreach (var dictionary in resources.MergedDictionaries)
        {
            if (dictionary is AntdThemeResources themeResources)
            {
                return themeResources;
            }

            var nested = Find(dictionary);
            if (nested is not null)
            {
                return nested;
            }
        }

        return null;
    }
}
