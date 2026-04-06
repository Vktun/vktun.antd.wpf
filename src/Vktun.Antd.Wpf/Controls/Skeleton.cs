using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a skeleton loading placeholder component.
/// </summary>
public class Skeleton : Control
{
    static Skeleton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Skeleton),
            new FrameworkPropertyMetadata(typeof(Skeleton)));
    }

    /// <summary>
    /// Identifies the <see cref="Loading"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LoadingProperty =
        DependencyProperty.Register(nameof(Loading), typeof(bool), typeof(Skeleton),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="Active"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ActiveProperty =
        DependencyProperty.Register(nameof(Active), typeof(bool), typeof(Skeleton),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="Avatar"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvatarProperty =
        DependencyProperty.Register(nameof(Avatar), typeof(bool), typeof(Skeleton),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="AvatarShape"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvatarShapeProperty =
        DependencyProperty.Register(nameof(AvatarShape), typeof(AntdAvatarShape), typeof(Skeleton),
            new PropertyMetadata(AntdAvatarShape.Circle));

    /// <summary>
    /// Identifies the <see cref="AvatarSize"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvatarSizeProperty =
        DependencyProperty.Register(nameof(AvatarSize), typeof(double), typeof(Skeleton),
            new PropertyMetadata(40d));

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(bool), typeof(Skeleton),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="TitleWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleWidthProperty =
        DependencyProperty.Register(nameof(TitleWidth), typeof(double), typeof(Skeleton),
            new PropertyMetadata(double.NaN));

    /// <summary>
    /// Identifies the <see cref="Paragraph"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ParagraphProperty =
        DependencyProperty.Register(nameof(Paragraph), typeof(bool), typeof(Skeleton),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="ParagraphRows"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ParagraphRowsProperty =
        DependencyProperty.Register(nameof(ParagraphRows), typeof(int), typeof(Skeleton),
            new PropertyMetadata(3));

    /// <summary>
    /// Identifies the <see cref="Content"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(Skeleton),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets whether the skeleton is in loading state.
    /// </summary>
    public bool Loading
    {
        get => (bool)GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show animation effect.
    /// </summary>
    public bool Active
    {
        get => (bool)GetValue(ActiveProperty);
        set => SetValue(ActiveProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show avatar placeholder.
    /// </summary>
    public bool Avatar
    {
        get => (bool)GetValue(AvatarProperty);
        set => SetValue(AvatarProperty, value);
    }

    /// <summary>
    /// Gets or sets the avatar shape.
    /// </summary>
    public AntdAvatarShape AvatarShape
    {
        get => (AntdAvatarShape)GetValue(AvatarShapeProperty);
        set => SetValue(AvatarShapeProperty, value);
    }

    /// <summary>
    /// Gets or sets the avatar size.
    /// </summary>
    public double AvatarSize
    {
        get => (double)GetValue(AvatarSizeProperty);
        set => SetValue(AvatarSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show title placeholder.
    /// </summary>
    public bool Title
    {
        get => (bool)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the title width.
    /// </summary>
    public double TitleWidth
    {
        get => (double)GetValue(TitleWidthProperty);
        set => SetValue(TitleWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show paragraph placeholder.
    /// </summary>
    public bool Paragraph
    {
        get => (bool)GetValue(ParagraphProperty);
        set => SetValue(ParagraphProperty, value);
    }

    /// <summary>
    /// Gets or sets the number of paragraph rows.
    /// </summary>
    public int ParagraphRows
    {
        get => (int)GetValue(ParagraphRowsProperty);
        set => SetValue(ParagraphRowsProperty, value);
    }

    /// <summary>
    /// Gets or sets the actual content to display when not loading.
    /// </summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
}