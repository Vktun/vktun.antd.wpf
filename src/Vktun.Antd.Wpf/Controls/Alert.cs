using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents an alert component for displaying feedback messages.
/// </summary>
public class Alert : ContentControl
{
    static Alert()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Alert),
            new FrameworkPropertyMetadata(typeof(Alert)));
    }

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdAlertType), typeof(Alert),
            new PropertyMetadata(AntdAlertType.Info));

    /// <summary>
    /// Identifies the <see cref="ShowIcon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowIconProperty =
        DependencyProperty.Register(nameof(ShowIcon), typeof(bool), typeof(Alert),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="Closable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(nameof(Closable), typeof(bool), typeof(Alert),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="CloseText"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CloseTextProperty =
        DependencyProperty.Register(nameof(CloseText), typeof(string), typeof(Alert),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Message"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(object), typeof(Alert),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Description"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(object), typeof(Alert),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Banner"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BannerProperty =
        DependencyProperty.Register(nameof(Banner), typeof(bool), typeof(Alert),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsClosed"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsClosedProperty =
        DependencyProperty.Register(nameof(IsClosed), typeof(bool), typeof(Alert),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the alert type.
    /// </summary>
    public AntdAlertType Type
    {
        get => (AntdAlertType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to show the icon.
    /// </summary>
    public bool ShowIcon
    {
        get => (bool)GetValue(ShowIconProperty);
        set => SetValue(ShowIconProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the alert is closable.
    /// </summary>
    public bool Closable
    {
        get => (bool)GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets the close button text.
    /// </summary>
    public string? CloseText
    {
        get => (string?)GetValue(CloseTextProperty);
        set => SetValue(CloseTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the main message content.
    /// </summary>
    public object? Message
    {
        get => GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>
    /// Gets or sets the description content.
    /// </summary>
    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to display as a banner at the top.
    /// </summary>
    public bool Banner
    {
        get => (bool)GetValue(BannerProperty);
        set => SetValue(BannerProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the alert is closed.
    /// </summary>
    public bool IsClosed
    {
        get => (bool)GetValue(IsClosedProperty);
        set => SetValue(IsClosedProperty, value);
    }

    /// <summary>
    /// Closes the alert.
    /// </summary>
    public void Close()
    {
        IsClosed = true;
    }
}