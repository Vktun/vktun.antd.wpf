using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a compact semantic label with Ant Design styling.
/// </summary>
public class Tag : ContentControl
{
    static Tag()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Tag), new FrameworkPropertyMetadata(typeof(Tag)));
    }

    /// <summary>
    /// Gets or sets the color of the tag.
    /// </summary>
    public AntdTagColor Color
    {
        get => (AntdTagColor)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Color"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(AntdTagColor), typeof(Tag),
            new PropertyMetadata(AntdTagColor.Default));

    /// <summary>
    /// Gets or sets whether the tag can be closed.
    /// </summary>
    public bool IsClosable
    {
        get => (bool)GetValue(IsClosableProperty);
        set => SetValue(IsClosableProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsClosable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsClosableProperty =
        DependencyProperty.Register(nameof(IsClosable), typeof(bool), typeof(Tag),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the tag can be checked.
    /// </summary>
    public bool IsCheckable
    {
        get => (bool)GetValue(IsCheckableProperty);
        set => SetValue(IsCheckableProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsCheckable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCheckableProperty =
        DependencyProperty.Register(nameof(IsCheckable), typeof(bool), typeof(Tag),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets whether the tag is checked.
    /// </summary>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsChecked"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(Tag),
            new PropertyMetadata(false, OnIsCheckedChanged));

    private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Tag tag)
        {
            tag.RaiseCheckedChangedEvent();
        }
    }

    /// <summary>
    /// Gets or sets whether the tag has no border.
    /// </summary>
    public bool Borderless
    {
        get => (bool)GetValue(BorderlessProperty);
        set => SetValue(BorderlessProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Borderless"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderlessProperty =
        DependencyProperty.Register(nameof(Borderless), typeof(bool), typeof(Tag),
            new PropertyMetadata(false));

    /// <summary>
    /// Raised when the close button is clicked.
    /// </summary>
    public static readonly RoutedEvent CloseClickEvent =
        EventManager.RegisterRoutedEvent("CloseClick", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(Tag));

    /// <summary>
    /// Raised when the checked state changes.
    /// </summary>
    public static readonly RoutedEvent CheckedChangedEvent =
        EventManager.RegisterRoutedEvent("CheckedChanged", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(Tag));

    public event RoutedEventHandler CloseClick
    {
        add => AddHandler(CloseClickEvent, value);
        remove => RemoveHandler(CloseClickEvent, value);
    }

    public event RoutedEventHandler CheckedChanged
    {
        add => AddHandler(CheckedChangedEvent, value);
        remove => RemoveHandler(CheckedChangedEvent, value);
    }

    /// <summary>
    /// Raises the close click event.
    /// </summary>
    public void RaiseCloseClickEvent()
    {
        RaiseEvent(new RoutedEventArgs(CloseClickEvent, this));
    }

    /// <summary>
    /// Raises the checked changed event.
    /// </summary>
    public void RaiseCheckedChangedEvent()
    {
        RaiseEvent(new RoutedEventArgs(CheckedChangedEvent, this));
    }

    /// <summary>
    /// Called when the close button is clicked.
    /// </summary>
    protected virtual void OnCloseClick()
    {
        RaiseCloseClickEvent();
    }
}
