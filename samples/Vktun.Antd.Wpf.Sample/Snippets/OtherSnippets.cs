namespace Vktun.Antd.Wpf.Sample.Snippets;

public static class OtherSnippets
{
    public static string ConfigProvider => """
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <antd:AntdThemeResources Theme="Light" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
""";

    public static string ThemeManager => """
var seed = new AntdSeedToken
{
    PrimaryColor = AntdColor.Parse("#6495ED"),
    BorderRadius = 12,
};

AntdThemeManager.Current.Apply(
    Application.Current!,
    AntdThemeMode.Dark,
    seed);
""";
}
