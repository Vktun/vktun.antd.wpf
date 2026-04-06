using System;
using System.Threading.Tasks;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides extension methods for Window to simplify Ant Design service usage.
/// </summary>
public static class AntdWindowExtensions
{
    /// <summary>
    /// Shows a success message in the window.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void ShowSuccess(this Window window, string content, TimeSpan? duration = null)
    {
        AntdServices.Success(window, content, duration);
    }

    /// <summary>
    /// Shows an error message in the window.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void ShowError(this Window window, string content, TimeSpan? duration = null)
    {
        AntdServices.Error(window, content, duration);
    }

    /// <summary>
    /// Shows a warning message in the window.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void ShowWarning(this Window window, string content, TimeSpan? duration = null)
    {
        AntdServices.Warning(window, content, duration);
    }

    /// <summary>
    /// Shows an info message in the window.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void ShowInfo(this Window window, string content, TimeSpan? duration = null)
    {
        AntdServices.Info(window, content, duration);
    }

    /// <summary>
    /// Shows a message in the window.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="content">The message content.</param>
    /// <param name="kind">The message kind.</param>
    /// <param name="duration">The display duration. Default is 3 seconds.</param>
    public static void ShowMessage(this Window window, string content, MessageKind kind = MessageKind.Info, TimeSpan? duration = null)
    {
        AntdServices.Message.Show(window, content, kind, duration);
    }

    /// <summary>
    /// Shows a notification in the window.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="request">The notification request.</param>
    public static void ShowNotification(this Window window, NotificationRequest request)
    {
        AntdServices.Notification.Show(window, request);
    }

    /// <summary>
    /// Shows a notification in the window with simplified parameters.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="title">The notification title.</param>
    /// <param name="description">The notification description.</param>
    /// <param name="placement">The notification placement. Default is TopRight.</param>
    public static void ShowNotification(this Window window, string title, string? description = null, NotificationPlacement placement = NotificationPlacement.TopRight)
    {
        var request = new NotificationRequest
        {
            Title = title,
            Description = description,
            Placement = placement
        };
        AntdServices.Notification.Show(window, request);
    }

    /// <summary>
    /// Shows a modal dialog in the window.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="request">The modal request.</param>
    /// <returns>The dialog result.</returns>
    public static Task<bool?> ShowModalAsync(this Window window, ModalRequest request)
    {
        return AntdServices.Modal.ShowAsync(window, request);
    }

    /// <summary>
    /// Shows a modal dialog in the window with simplified parameters.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="title">The modal title.</param>
    /// <param name="content">The modal content.</param>
    /// <param name="showCancel">Whether to show the cancel button.</param>
    /// <returns>The dialog result.</returns>
    public static Task<bool?> ShowModalAsync(this Window window, string title, object content, bool showCancel = true)
    {
        var request = new ModalRequest
        {
            Title = title,
            Content = content,
            ShowCancel = showCancel
        };
        return AntdServices.Modal.ShowAsync(window, request);
    }

    /// <summary>
    /// Shows a confirm modal dialog in the window.
    /// </summary>
    /// <param name="window">The owner window.</param>
    /// <param name="title">The modal title.</param>
    /// <param name="content">The modal content.</param>
    /// <returns>True if confirmed, false if cancelled, null if closed without action.</returns>
    public static Task<bool?> ConfirmAsync(this Window window, string title, string content)
    {
        return window.ShowModalAsync(title, content, true);
    }
}