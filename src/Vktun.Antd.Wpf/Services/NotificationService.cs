using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Default <see cref="INotificationService"/> implementation.
/// </summary>
public sealed class NotificationService : INotificationService
{
    /// <inheritdoc />
    public void Show(Window owner, NotificationRequest request)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(request);
        OverlayHost.Attach(owner).ShowNotification(owner, request);
    }
}
