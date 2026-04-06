using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a single item in a list.
/// </summary>
public class ListItem : Control
{
    static ListItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ListItem),
            new FrameworkPropertyMetadata(typeof(ListItem)));
    }

    /// <summary>
    /// Identifies the <see cref="Content"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(ListItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Extra"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ExtraProperty =
        DependencyProperty.Register(nameof(Extra), typeof(object), typeof(ListItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Actions"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ActionsProperty =
        DependencyProperty.Register(nameof(Actions), typeof(ObservableCollection<object>), typeof(ListItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="IsHoverable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsHoverableProperty =
        DependencyProperty.Register(nameof(IsHoverable), typeof(bool), typeof(ListItem),
            new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>
    /// Gets or sets extra content on the right side.
    /// </summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>
    /// Gets or sets the actions collection.
    /// </summary>
    public ObservableCollection<object>? Actions
    {
        get => (ObservableCollection<object>?)GetValue(ActionsProperty);
        set => SetValue(ActionsProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the item has hover effect.
    /// </summary>
    public bool IsHoverable
    {
        get => (bool)GetValue(IsHoverableProperty);
        set => SetValue(IsHoverableProperty, value);
    }
}

/// <summary>
/// Represents a list component for displaying data in a simple list format.
/// </summary>
public class List : ItemsControl
{
    static List()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(List),
            new FrameworkPropertyMetadata(typeof(List)));
    }

    /// <summary>
    /// Identifies the <see cref="Header"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(List),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Footer"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FooterProperty =
        DependencyProperty.Register(nameof(Footer), typeof(object), typeof(List),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="LoadMore"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LoadMoreProperty =
        DependencyProperty.Register(nameof(LoadMore), typeof(object), typeof(List),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Loading"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LoadingProperty =
        DependencyProperty.Register(nameof(Loading), typeof(bool), typeof(List),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Grid"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty GridProperty =
        DependencyProperty.Register(nameof(Grid), typeof(AntdListGrid), typeof(List),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(List),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Split"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SplitProperty =
        DependencyProperty.Register(nameof(Split), typeof(bool), typeof(List),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="Bordered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderedProperty =
        DependencyProperty.Register(nameof(Bordered), typeof(bool), typeof(List),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Layout"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LayoutProperty =
        DependencyProperty.Register(nameof(Layout), typeof(AntdListLayout), typeof(List),
            new PropertyMetadata(AntdListLayout.Vertical));

    /// <summary>
    /// Gets or sets the header content.
    /// </summary>
    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the footer content.
    /// </summary>
    public object? Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    /// <summary>
    /// Gets or sets the load more content.
    /// </summary>
    public object? LoadMore
    {
        get => GetValue(LoadMoreProperty);
        set => SetValue(LoadMoreProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the list is loading.
    /// </summary>
    public bool Loading
    {
        get => (bool)GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    /// <summary>
    /// Gets or sets the grid configuration.
    /// </summary>
    public AntdListGrid? Grid
    {
        get => (AntdListGrid?)GetValue(GridProperty);
        set => SetValue(GridProperty, value);
    }

    /// <summary>
    /// Gets or sets the list size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether items are separated.
    /// </summary>
    public bool Split
    {
        get => (bool)GetValue(SplitProperty);
        set => SetValue(SplitProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the list has borders.
    /// </summary>
    public bool Bordered
    {
        get => (bool)GetValue(BorderedProperty);
        set => SetValue(BorderedProperty, value);
    }

    /// <summary>
    /// Gets or sets the list layout.
    /// </summary>
    public AntdListLayout Layout
    {
        get => (AntdListLayout)GetValue(LayoutProperty);
        set => SetValue(LayoutProperty, value);
    }
}

/// <summary>
/// Represents grid configuration for List component.
/// </summary>
public class AntdListGrid : DependencyObject
{
    /// <summary>
    /// Identifies the <see cref="Gutter"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty GutterProperty =
        DependencyProperty.Register(nameof(Gutter), typeof(int), typeof(AntdListGrid),
            new PropertyMetadata(16));

    /// <summary>
    /// Identifies the <see cref="Column"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColumnProperty =
        DependencyProperty.Register(nameof(Column), typeof(int), typeof(AntdListGrid),
            new PropertyMetadata(1));

    /// <summary>
    /// Identifies the <see cref="Xs"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XsProperty =
        DependencyProperty.Register(nameof(Xs), typeof(int), typeof(AntdListGrid),
            new PropertyMetadata(1));

    /// <summary>
    /// Identifies the <see cref="Sm"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SmProperty =
        DependencyProperty.Register(nameof(Sm), typeof(int), typeof(AntdListGrid),
            new PropertyMetadata(2));

    /// <summary>
    /// Identifies the <see cref="Md"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MdProperty =
        DependencyProperty.Register(nameof(Md), typeof(int), typeof(AntdListGrid),
            new PropertyMetadata(3));

    /// <summary>
    /// Identifies the <see cref="Lg"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LgProperty =
        DependencyProperty.Register(nameof(Lg), typeof(int), typeof(AntdListGrid),
            new PropertyMetadata(4));

    /// <summary>
    /// Identifies the <see cref="Xl"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XlProperty =
        DependencyProperty.Register(nameof(Xl), typeof(int), typeof(AntdListGrid),
            new PropertyMetadata(6));

    /// <summary>
    /// Identifies the <see cref="Xxl"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty XxlProperty =
        DependencyProperty.Register(nameof(Xxl), typeof(int), typeof(AntdListGrid),
            new PropertyMetadata(8));

    /// <summary>
    /// Gets or sets the gutter size.
    /// </summary>
    public int Gutter
    {
        get => (int)GetValue(GutterProperty);
        set => SetValue(GutterProperty, value);
    }

    /// <summary>
    /// Gets or sets the column count.
    /// </summary>
    public int Column
    {
        get => (int)GetValue(ColumnProperty);
        set => SetValue(ColumnProperty, value);
    }

    /// <summary>
    /// Gets or sets the xs breakpoint columns.
    /// </summary>
    public int Xs
    {
        get => (int)GetValue(XsProperty);
        set => SetValue(XsProperty, value);
    }

    /// <summary>
    /// Gets or sets the sm breakpoint columns.
    /// </summary>
    public int Sm
    {
        get => (int)GetValue(SmProperty);
        set => SetValue(SmProperty, value);
    }

    /// <summary>
    /// Gets or sets the md breakpoint columns.
    /// </summary>
    public int Md
    {
        get => (int)GetValue(MdProperty);
        set => SetValue(MdProperty, value);
    }

    /// <summary>
    /// Gets or sets the lg breakpoint columns.
    /// </summary>
    public int Lg
    {
        get => (int)GetValue(LgProperty);
        set => SetValue(LgProperty, value);
    }

    /// <summary>
    /// Gets or sets the xl breakpoint columns.
    /// </summary>
    public int Xl
    {
        get => (int)GetValue(XlProperty);
        set => SetValue(XlProperty, value);
    }

    /// <summary>
    /// Gets or sets the xxl breakpoint columns.
    /// </summary>
    public int Xxl
    {
        get => (int)GetValue(XxlProperty);
        set => SetValue(XxlProperty, value);
    }
}

/// <summary>
/// Attached properties for List component.
/// </summary>
public static class ListAssist
{
    /// <summary>
    /// Identifies the <see cref="GetItemLayout"/> attached property.
    /// </summary>
    public static readonly DependencyProperty ItemLayoutProperty =
        DependencyProperty.RegisterAttached("ItemLayout", typeof(AntdListLayout), typeof(ListAssist),
            new PropertyMetadata(AntdListLayout.Horizontal));

    /// <summary>
    /// Gets the item layout.
    /// </summary>
    public static AntdListLayout GetItemLayout(DependencyObject obj)
    {
        return (AntdListLayout)obj.GetValue(ItemLayoutProperty);
    }

    /// <summary>
    /// Sets the item layout.
    /// </summary>
    public static void SetItemLayout(DependencyObject obj, AntdListLayout value)
    {
        obj.SetValue(ItemLayoutProperty, value);
    }
}