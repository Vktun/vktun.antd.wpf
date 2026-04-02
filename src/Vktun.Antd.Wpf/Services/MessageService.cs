using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Default <see cref="IMessageService"/> implementation.
/// </summary>
public sealed class MessageService : IMessageService
{
    /// <inheritdoc />
    public void Show(Window owner, string content, MessageKind kind = MessageKind.Info, TimeSpan? duration = null)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        OverlayHost.Attach(owner).ShowMessage(owner, content, kind, duration ?? TimeSpan.FromSeconds(2.5d));
    }
}
