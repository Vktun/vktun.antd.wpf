using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf.Sample.Controls;

public partial class ComponentDemo : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(ComponentDemo), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(ComponentDemo), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty XamlCodeProperty =
        DependencyProperty.Register(nameof(XamlCode), typeof(string), typeof(ComponentDemo), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty CSharpCodeProperty =
        DependencyProperty.Register(nameof(CSharpCode), typeof(string), typeof(ComponentDemo), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty PreviewContentProperty =
        DependencyProperty.Register(nameof(PreviewContent), typeof(object), typeof(ComponentDemo), new PropertyMetadata(null));

    public static readonly DependencyProperty PreviewMinHeightProperty =
        DependencyProperty.Register(nameof(PreviewMinHeight), typeof(double), typeof(ComponentDemo), new PropertyMetadata(120d));

    public ComponentDemo()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public string XamlCode
    {
        get => (string)GetValue(XamlCodeProperty);
        set => SetValue(XamlCodeProperty, value);
    }

    public string CSharpCode
    {
        get => (string)GetValue(CSharpCodeProperty);
        set => SetValue(CSharpCodeProperty, value);
    }

    public object? PreviewContent
    {
        get => GetValue(PreviewContentProperty);
        set => SetValue(PreviewContentProperty, value);
    }

    public double PreviewMinHeight
    {
        get => (double)GetValue(PreviewMinHeightProperty);
        set => SetValue(PreviewMinHeightProperty, value);
    }
}
