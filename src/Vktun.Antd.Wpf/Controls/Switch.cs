using System.Windows;
using System.Windows.Controls.Primitives;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a compact switch control with Ant Design styling.
/// </summary>
public class Switch : ToggleButton
{
    static Switch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Switch), new FrameworkPropertyMetadata(typeof(Switch)));
    }

    /// <summary>
    /// Gets or sets the size of the switch.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(Switch),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Gets or sets whether the switch is in loading state.
    /// </summary>
    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsLoading"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsLoadingProperty =
        DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(Switch),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the text displayed when switch is checked.
    /// </summary>
    public string CheckedChildren
    {
        get => (string?)GetValue(CheckedChildrenProperty) ?? string.Empty;
        set => SetValue(CheckedChildrenProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="CheckedChildren"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CheckedChildrenProperty =
        DependencyProperty.Register(nameof(CheckedChildren), typeof(string), typeof(Switch),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the text displayed when switch is unchecked.
    /// </summary>
    public string UnCheckedChildren
    {
        get => (string?)GetValue(UnCheckedChildrenProperty) ?? string.Empty;
        set => SetValue(UnCheckedChildrenProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="UnCheckedChildren"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UnCheckedChildrenProperty =
        DependencyProperty.Register(nameof(UnCheckedChildren), typeof(string), typeof(Switch),
            new PropertyMetadata(string.Empty));
}
