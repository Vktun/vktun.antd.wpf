using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Displays notification cards.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Shows a notification overlay.
    /// </summary>
    void Show(Window owner, NotificationRequest request);
}
