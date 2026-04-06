using System;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides global access to Ant Design WPF services.
/// </summary>
public static class AntdServices
{
    /// <summary>
    /// Gets the default message service instance.
    /// </summary>
    public static IMessageService Message { get; } = new MessageService();

    /// <summary>
    /// Gets the default notification service instance.
    /// </summary>
    public static INotificationService Notification { get; } = new NotificationService();

    /// <summary>
    /// Gets the default modal service instance.
    /// </summary>
    public static IModalService Modal { get; } = new ModalService();

    /// <summary>
    /// Shows a success message.
    /// </summary>
    /// <param name="owner">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void Success(System.Windows.Window owner, string content, TimeSpan? duration = null)
    {
        Message.Show(owner, content, MessageKind.Success, duration);
    }

    /// <summary>
    /// Shows an error message.
    /// </summary>
    /// <param name="owner">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void Error(System.Windows.Window owner, string content, TimeSpan? duration = null)
    {
        Message.Show(owner, content, MessageKind.Error, duration);
    }

    /// <summary>
    /// Shows a warning message.
    /// </summary>
    /// <param name="owner">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void Warning(System.Windows.Window owner, string content, TimeSpan? duration = null)
    {
        Message.Show(owner, content, MessageKind.Warning, duration);
    }

    /// <summary>
    /// Shows an info message.
    /// </summary>
    /// <param name="owner">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void Info(System.Windows.Window owner, string content, TimeSpan? duration = null)
    {
        Message.Show(owner, content, MessageKind.Info, duration);
    }
}