using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a title component with Ant Design styling.
/// </summary>
public class TypographyTitle : Control
{
    static TypographyTitle()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TypographyTitle), new FrameworkPropertyMetadata(typeof(TypographyTitle)));
    }

    /// <summary>
    /// Gets or sets the text content.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(TypographyTitle),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the heading level (1-4).
    /// </summary>
    public int Level
    {
        get => (int)GetValue(LevelProperty);
        set => SetValue(LevelProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Level"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LevelProperty =
        DependencyProperty.Register(nameof(Level), typeof(int), typeof(TypographyTitle),
            new PropertyMetadata(1));

    /// <summary>
    /// Gets or sets whether the text can be copied.
    /// </summary>
    public bool IsCopyable
    {
        get => (bool)GetValue(IsCopyableProperty);
        set => SetValue(IsCopyableProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsCopyable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCopyableProperty =
        DependencyProperty.Register(nameof(IsCopyable), typeof(bool), typeof(TypographyTitle),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text has ellipsis.
    /// </summary>
    public bool Ellipsis
    {
        get => (bool)GetValue(EllipsisProperty);
        set => SetValue(EllipsisProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Ellipsis"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EllipsisProperty =
        DependencyProperty.Register(nameof(Ellipsis), typeof(bool), typeof(TypographyTitle),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text is marked/highlighted.
    /// </summary>
    public bool Mark
    {
        get => (bool)GetValue(MarkProperty);
        set => SetValue(MarkProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Mark"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MarkProperty =
        DependencyProperty.Register(nameof(Mark), typeof(bool), typeof(TypographyTitle),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the semantic type of the typography.
    /// </summary>
    public AntdTypographyType Type
    {
        get => (AntdTypographyType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdTypographyType), typeof(TypographyTitle),
            new PropertyMetadata(AntdTypographyType.Default));
}

/// <summary>
/// Represents a text component with Ant Design styling.
/// </summary>
public class TypographyText : Control
{
    static TypographyText()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TypographyText), new FrameworkPropertyMetadata(typeof(TypographyText)));
    }

    /// <summary>
    /// Gets or sets the text content.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(TypographyText),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets whether the text can be copied.
    /// </summary>
    public bool IsCopyable
    {
        get => (bool)GetValue(IsCopyableProperty);
        set => SetValue(IsCopyableProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsCopyable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCopyableProperty =
        DependencyProperty.Register(nameof(IsCopyable), typeof(bool), typeof(TypographyText),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text has ellipsis.
    /// </summary>
    public bool Ellipsis
    {
        get => (bool)GetValue(EllipsisProperty);
        set => SetValue(EllipsisProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Ellipsis"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EllipsisProperty =
        DependencyProperty.Register(nameof(Ellipsis), typeof(bool), typeof(TypographyText),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text is marked/highlighted.
    /// </summary>
    public bool Mark
    {
        get => (bool)GetValue(MarkProperty);
        set => SetValue(MarkProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Mark"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MarkProperty =
        DependencyProperty.Register(nameof(Mark), typeof(bool), typeof(TypographyText),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text has delete style.
    /// </summary>
    public bool Delete
    {
        get => (bool)GetValue(DeleteProperty);
        set => SetValue(DeleteProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Delete"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DeleteProperty =
        DependencyProperty.Register(nameof(Delete), typeof(bool), typeof(TypographyText),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text has underline style.
    /// </summary>
    public bool Underline
    {
        get => (bool)GetValue(UnderlineProperty);
        set => SetValue(UnderlineProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Underline"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UnderlineProperty =
        DependencyProperty.Register(nameof(Underline), typeof(bool), typeof(TypographyText),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text is bold.
    /// </summary>
    public bool Strong
    {
        get => (bool)GetValue(StrongProperty);
        set => SetValue(StrongProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Strong"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StrongProperty =
        DependencyProperty.Register(nameof(Strong), typeof(bool), typeof(TypographyText),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text has code style.
    /// </summary>
    public bool Code
    {
        get => (bool)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Code"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CodeProperty =
        DependencyProperty.Register(nameof(Code), typeof(bool), typeof(TypographyText),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the semantic type of the typography.
    /// </summary>
    public AntdTypographyType Type
    {
        get => (AntdTypographyType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdTypographyType), typeof(TypographyText),
            new PropertyMetadata(AntdTypographyType.Default));
}

/// <summary>
/// Represents a paragraph component with Ant Design styling.
/// </summary>
public class TypographyParagraph : Control
{
    static TypographyParagraph()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TypographyParagraph), new FrameworkPropertyMetadata(typeof(TypographyParagraph)));
    }

    /// <summary>
    /// Gets or sets the text content.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(TypographyParagraph),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets whether the text can be copied.
    /// </summary>
    public bool IsCopyable
    {
        get => (bool)GetValue(IsCopyableProperty);
        set => SetValue(IsCopyableProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsCopyable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCopyableProperty =
        DependencyProperty.Register(nameof(IsCopyable), typeof(bool), typeof(TypographyParagraph),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text has ellipsis.
    /// </summary>
    public bool Ellipsis
    {
        get => (bool)GetValue(EllipsisProperty);
        set => SetValue(EllipsisProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Ellipsis"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty EllipsisProperty =
        DependencyProperty.Register(nameof(Ellipsis), typeof(bool), typeof(TypographyParagraph),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text is marked/highlighted.
    /// </summary>
    public bool Mark
    {
        get => (bool)GetValue(MarkProperty);
        set => SetValue(MarkProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Mark"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MarkProperty =
        DependencyProperty.Register(nameof(Mark), typeof(bool), typeof(TypographyParagraph),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the text is bold.
    /// </summary>
    public bool Strong
    {
        get => (bool)GetValue(StrongProperty);
        set => SetValue(StrongProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Strong"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StrongProperty =
        DependencyProperty.Register(nameof(Strong), typeof(bool), typeof(TypographyParagraph),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the semantic type of the typography.
    /// </summary>
    public AntdTypographyType Type
    {
        get => (AntdTypographyType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdTypographyType), typeof(TypographyParagraph),
            new PropertyMetadata(AntdTypographyType.Default));
}

/// <summary>
/// Helper class for copy functionality.
/// </summary>
public static class TypographyCopyHelper
{
    /// <summary>
    /// Copies the specified text to clipboard.
    /// </summary>
    public static void CopyToClipboard(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            Clipboard.SetText(text);
        }
    }
}