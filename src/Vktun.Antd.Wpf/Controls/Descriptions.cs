using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a single item in a descriptions list.
/// </summary>
public class DescriptionItem : Control
{
    static DescriptionItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DescriptionItem),
            new FrameworkPropertyMetadata(typeof(DescriptionItem)));
    }

    /// <summary>
    /// Identifies the <see cref="Label"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(object), typeof(DescriptionItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Content"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(DescriptionItem),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Span"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SpanProperty =
        DependencyProperty.Register(nameof(Span), typeof(int), typeof(DescriptionItem),
            new PropertyMetadata(1));

    /// <summary>
    /// Gets or sets the label content.
    /// </summary>
    public object? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the content value.
    /// </summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the column span.
    /// </summary>
    public int Span
    {
        get => (int)GetValue(SpanProperty);
        set => SetValue(SpanProperty, value);
    }
}

/// <summary>
/// Represents a descriptions component for displaying read-only data in list format.
/// </summary>
public class Descriptions : ItemsControl
{
    static Descriptions()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Descriptions),
            new FrameworkPropertyMetadata(typeof(Descriptions)));
    }

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(object), typeof(Descriptions),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Extra"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ExtraProperty =
        DependencyProperty.Register(nameof(Extra), typeof(object), typeof(Descriptions),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Column"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColumnProperty =
        DependencyProperty.Register(nameof(Column), typeof(int), typeof(Descriptions),
            new PropertyMetadata(3));

    /// <summary>
    /// Identifies the <see cref="Layout"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LayoutProperty =
        DependencyProperty.Register(nameof(Layout), typeof(AntdDescriptionsLayout), typeof(Descriptions),
            new PropertyMetadata(AntdDescriptionsLayout.Horizontal));

    /// <summary>
    /// Identifies the <see cref="Bordered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderedProperty =
        DependencyProperty.Register(nameof(Bordered), typeof(bool), typeof(Descriptions),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(Descriptions),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Colon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColonProperty =
        DependencyProperty.Register(nameof(Colon), typeof(bool), typeof(Descriptions),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="LabelStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelStyleProperty =
        DependencyProperty.Register(nameof(LabelStyle), typeof(Style), typeof(Descriptions),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="ContentStyle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentStyleProperty =
        DependencyProperty.Register(nameof(ContentStyle), typeof(Style), typeof(Descriptions),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the title content.
    /// </summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets extra content in the header.
    /// </summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>
    /// Gets or sets the number of columns.
    /// </summary>
    public int Column
    {
        get => (int)GetValue(ColumnProperty);
        set => SetValue(ColumnProperty, value);
    }

    /// <summary>
    /// Gets or sets the descriptions layout.
    /// </summary>
    public AntdDescriptionsLayout Layout
    {
        get => (AntdDescriptionsLayout)GetValue(LayoutProperty);
        set => SetValue(LayoutProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the descriptions is bordered.
    /// </summary>
    public bool Bordered
    {
        get => (bool)GetValue(BorderedProperty);
        set => SetValue(BorderedProperty, value);
    }

    /// <summary>
    /// Gets or sets the descriptions size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show colon after labels.
    /// </summary>
    public bool Colon
    {
        get => (bool)GetValue(ColonProperty);
        set => SetValue(ColonProperty, value);
    }

    /// <summary>
    /// Gets or sets the label style.
    /// </summary>
    public Style? LabelStyle
    {
        get => (Style?)GetValue(LabelStyleProperty);
        set => SetValue(LabelStyleProperty, value);
    }

    /// <summary>
    /// Gets or sets the content style.
    /// </summary>
    public Style? ContentStyle
    {
        get => (Style?)GetValue(ContentStyleProperty);
        set => SetValue(ContentStyleProperty, value);
    }
}

/// <summary>
/// Attached properties for Descriptions component.
/// </summary>
public static class DescriptionsAssist
{
    /// <summary>
    /// Identifies the <see cref="GetLabelAlign"/> attached property.
    /// </summary>
    public static readonly DependencyProperty LabelAlignProperty =
        DependencyProperty.RegisterAttached("LabelAlign", typeof(TextAlignment), typeof(DescriptionsAssist),
            new PropertyMetadata(TextAlignment.Right));

    /// <summary>
    /// Gets the label alignment.
    /// </summary>
    public static TextAlignment GetLabelAlign(DependencyObject obj)
    {
        return (TextAlignment)obj.GetValue(LabelAlignProperty);
    }

    /// <summary>
    /// Sets the label alignment.
    /// </summary>
    public static void SetLabelAlign(DependencyObject obj, TextAlignment value)
    {
        obj.SetValue(LabelAlignProperty, value);
    }
}