using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace Vktun.Antd.Avalonia;

/// <summary>
/// Hosts transient overlay entries for messages, notifications, modals, and drawers.
/// </summary>
public sealed class OverlayHost : ContentControl
{
    private static readonly Dictionary<Panel, OverlayHost> Hosts = [];
    private readonly Stack<TaskCompletionSource<bool>> _modalCompletions = new();

    /// <summary>
    /// Gets overlay entries currently shown by this host.
    /// </summary>
    public ObservableCollection<object> Items { get; } = [];

    /// <summary>
    /// Attaches an overlay host to a panel root.
    /// </summary>
    public static OverlayHost Attach(Panel root)
    {
        ArgumentNullException.ThrowIfNull(root);

        if (Hosts.TryGetValue(root, out var existing))
        {
            return existing;
        }

        var host = new OverlayHost();
        Hosts[root] = host;
        root.Children.Add(host);
        return host;
    }

    /// <summary>
    /// Gets an existing host for the root panel.
    /// </summary>
    public static OverlayHost? Get(Panel root)
    {
        ArgumentNullException.ThrowIfNull(root);
        return Hosts.GetValueOrDefault(root);
    }

    /// <summary>
    /// Adds a message entry.
    /// </summary>
    public void ShowMessage(string content, MessageKind kind, TimeSpan duration)
    {
        Items.Add(new MessageEntry(content, kind, duration));
    }

    /// <summary>
    /// Adds a notification entry.
    /// </summary>
    public void ShowNotification(NotificationRequest request)
    {
        Items.Add(request);
    }

    /// <summary>
    /// Adds a modal entry and returns a completion task.
    /// </summary>
    public Task<bool> ShowModalAsync(ModalRequest request)
    {
        var completion = new TaskCompletionSource<bool>();
        _modalCompletions.Push(completion);
        Items.Add(request);
        return completion.Task;
    }

    /// <summary>
    /// Completes the latest modal request.
    /// </summary>
    public void CloseLatestModal(bool result)
    {
        if (_modalCompletions.TryPop(out var completion))
        {
            completion.TrySetResult(result);
        }
    }
}

/// <summary>
/// Represents a transient message entry.
/// </summary>
public sealed record MessageEntry(string Content, MessageKind Kind, TimeSpan Duration);

/// <summary>
/// Shows transient messages.
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Shows a message on the root panel.
    /// </summary>
    void Show(Panel root, string content, MessageKind kind = MessageKind.Info, TimeSpan? duration = null);
}

/// <summary>
/// Default message service.
/// </summary>
public sealed class MessageService : IMessageService
{
    /// <inheritdoc />
    public void Show(Panel root, string content, MessageKind kind = MessageKind.Info, TimeSpan? duration = null)
    {
        ArgumentNullException.ThrowIfNull(root);
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        OverlayHost.Attach(root).ShowMessage(content, kind, duration ?? TimeSpan.FromSeconds(2.5d));
    }
}

/// <summary>
/// Represents a notification request.
/// </summary>
public sealed class NotificationRequest
{
    /// <summary>
    /// Gets or sets the notification title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the notification description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the notification placement.
    /// </summary>
    public NotificationPlacement Placement { get; set; } = NotificationPlacement.TopRight;
}

/// <summary>
/// Shows notifications.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Shows a notification on the root panel.
    /// </summary>
    void Show(Panel root, NotificationRequest request);
}

/// <summary>
/// Default notification service.
/// </summary>
public sealed class NotificationService : INotificationService
{
    /// <inheritdoc />
    public void Show(Panel root, NotificationRequest request)
    {
        ArgumentNullException.ThrowIfNull(root);
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Title);
        OverlayHost.Attach(root).ShowNotification(request);
    }
}

/// <summary>
/// Represents a modal request.
/// </summary>
public sealed class ModalRequest
{
    /// <summary>
    /// Gets or sets the modal title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the modal content.
    /// </summary>
    public object? Content { get; set; }
}

/// <summary>
/// Shows modal dialogs.
/// </summary>
public interface IModalService
{
    /// <summary>
    /// Shows a modal and returns the result task.
    /// </summary>
    Task<bool> ShowAsync(Panel root, ModalRequest request);
}

/// <summary>
/// Default modal service.
/// </summary>
public sealed class ModalService : IModalService
{
    /// <inheritdoc />
    public Task<bool> ShowAsync(Panel root, ModalRequest request)
    {
        ArgumentNullException.ThrowIfNull(root);
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Title);
        return OverlayHost.Attach(root).ShowModalAsync(request);
    }
}
