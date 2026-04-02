using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Displays transient message overlays.
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Shows a transient message overlay.
    /// </summary>
    void Show(Window owner, string content, MessageKind kind = MessageKind.Info, TimeSpan? duration = null);
}
