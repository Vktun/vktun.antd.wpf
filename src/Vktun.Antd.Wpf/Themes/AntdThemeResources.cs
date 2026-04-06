using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// A mergeable resource dictionary that injects Ant Design inspired resources and styles.
/// </summary>
public sealed class AntdThemeResources : ResourceDictionary
{
    private static readonly Uri[] SharedDictionaries =
    {
        new("/Vktun.Antd.Wpf;component/Themes/Generic.xaml", UriKind.Relative),
    };

    private readonly ResourceDictionary _tokenDictionary = new();
    private AntdThemeMode _theme = AntdThemeMode.Light;
    private AntdSeedToken _seed = AntdSeedToken.Default;

    /// <summary>
    /// Initializes a new instance of the <see cref="AntdThemeResources"/> class.
    /// </summary>
    public AntdThemeResources()
    {
        foreach (var dictionary in SharedDictionaries)
        {
            MergedDictionaries.Add(new ResourceDictionary { Source = dictionary });
        }

        MergedDictionaries.Add(_tokenDictionary);
        ReloadTokens();
    }

    /// <summary>
    /// Gets or sets the active theme mode.
    /// </summary>
    public AntdThemeMode Theme
    {
        get => _theme;
        set
        {
            if (_theme == value)
            {
                return;
            }

            _theme = value;
            ReloadTokens();
        }
    }

    /// <summary>
    /// Gets or sets the seed token used to generate runtime resources.
    /// </summary>
    public AntdSeedToken Seed
    {
        get => _seed;
        set
        {
            _seed = value ?? AntdSeedToken.Default;
            ReloadTokens();
        }
    }

    private void ReloadTokens()
    {
        _tokenDictionary.Clear();
        var tokens = AntdTokenFactory.Create(_theme, _seed);
        foreach (System.Collections.DictionaryEntry entry in tokens)
        {
            _tokenDictionary[entry.Key] = entry.Value;
        }
    }
}

