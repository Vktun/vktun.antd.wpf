using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Vktun.Antd.Wpf;

/// <summary>
/// A control that displays different content based on its checked state,
/// similar to HandyControl's ToggleBlock for implementing toggle UI patterns.
/// </summary>
public class ToggleBlock : ContentControl
{
    static ToggleBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleBlock),
            new FrameworkPropertyMetadata(typeof(ToggleBlock)));
    }

    /// <summary>
    /// Identifies the <see cref="IsChecked"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(ToggleBlock),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="UnCheckedContent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UnCheckedContentProperty =
        DependencyProperty.Register(nameof(UnCheckedContent), typeof(object), typeof(ToggleBlock),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="CheckedContent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CheckedContentProperty =
        DependencyProperty.Register(nameof(CheckedContent), typeof(object), typeof(ToggleBlock),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="ToggleGesture"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ToggleGestureProperty =
        DependencyProperty.Register(nameof(ToggleGesture), typeof(MouseGesture), typeof(ToggleBlock),
            new PropertyMetadata(new MouseGesture(MouseAction.LeftClick)));

    /// <summary>
    /// Gets or sets whether the toggle block is checked.
    /// </summary>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /// <summary>
    /// Gets or sets the content displayed when unchecked.
    /// </summary>
    public object? UnCheckedContent
    {
        get => GetValue(UnCheckedContentProperty);
        set => SetValue(UnCheckedContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the content displayed when checked.
    /// </summary>
    public object? CheckedContent
    {
        get => GetValue(CheckedContentProperty);
        set => SetValue(CheckedContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the mouse gesture that toggles the state.
    /// </summary>
    public MouseGesture ToggleGesture
    {
        get => (MouseGesture)GetValue(ToggleGestureProperty);
        set => SetValue(ToggleGestureProperty, value);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (ToggleGesture.MouseAction == MouseAction.LeftClick)
        {
            IsChecked = !IsChecked;
            e.Handled = true;
        }
        base.OnMouseLeftButtonDown(e);
    }

    protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
    {
        if (ToggleGesture.MouseAction == MouseAction.RightClick)
        {
            IsChecked = !IsChecked;
            e.Handled = true;
        }
        base.OnMouseRightButtonDown(e);
    }
}