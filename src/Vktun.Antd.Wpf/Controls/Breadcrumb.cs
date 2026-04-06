using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a breadcrumb navigation item.
/// </summary>
public class BreadcrumbItem : HeaderedContentControl
{
    static BreadcrumbItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(BreadcrumbItem),
            new FrameworkPropertyMetadata(typeof(BreadcrumbItem)));
    }

    /// <summary>
    /// Identifies the <see cref="Href"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HrefProperty =
        DependencyProperty.Register(nameof(Href), typeof(string), typeof(BreadcrumbItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(BreadcrumbItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="IsLast"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsLastProperty =
        DependencyProperty.Register(nameof(IsLast), typeof(bool), typeof(BreadcrumbItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the navigation link.
    /// </summary>
    public string? Href
    {
        get => (string?)GetValue(HrefProperty);
        set => SetValue(HrefProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon content.
    /// </summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this is the last item in the breadcrumb.
    /// </summary>
    public bool IsLast
    {
        get => (bool)GetValue(IsLastProperty);
        set => SetValue(IsLastProperty, value);
    }
}

/// <summary>
/// Represents a breadcrumb navigation component.
/// </summary>
public class Breadcrumb : ItemsControl
{
    static Breadcrumb()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Breadcrumb),
            new FrameworkPropertyMetadata(typeof(Breadcrumb)));
    }

    /// <summary>
    /// Identifies the <see cref="Separator"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SeparatorProperty =
        DependencyProperty.Register(nameof(Separator), typeof(object), typeof(Breadcrumb),
            new PropertyMetadata("/"));

    /// <summary>
    /// Identifies the <see cref="ItemTemplate"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(Breadcrumb),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the separator content between items.
    /// </summary>
    public object? Separator
    {
        get => GetValue(SeparatorProperty);
        set => SetValue(SeparatorProperty, value);
    }

    /// <summary>
    /// Gets or sets the item template.
    /// </summary>
    public new DataTemplate? ItemTemplate
    {
        get => (DataTemplate?)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
}