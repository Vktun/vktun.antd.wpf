using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;

[assembly: InternalsVisibleTo("Vktun.Antd.Wpf.Tests")]
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly)]

// 简化 XAML 命名空间引用
// 使用方式: xmlns:antd="https://vktun.com/antd/wpf"
[assembly: XmlnsDefinition("https://vktun.com/antd/wpf", "Vktun.Antd.Wpf")]
[assembly: XmlnsPrefix("https://vktun.com/antd/wpf", "antd")]
