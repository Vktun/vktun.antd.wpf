using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Default <see cref="IModalService"/> implementation.
/// </summary>
public sealed class ModalService : IModalService
{
    /// <inheritdoc />
    public Task<bool?> ShowAsync(Window owner, ModalRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(request);
        return OverlayHost.Attach(owner).ShowModalAsync(owner, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<bool?> ShowWindowAsync(Window owner, ModalRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(request);

        if (owner.Dispatcher.CheckAccess())
        {
            return Task.FromResult(ShowWindowCore(owner, request, cancellationToken));
        }

        return owner.Dispatcher.InvokeAsync(() => ShowWindowCore(owner, request, cancellationToken)).Task;
    }

    private static bool? ShowWindowCore(Window owner, ModalRequest request, CancellationToken cancellationToken)
    {
        var dialog = new ModalDialogWindow(owner, request);
        using var registration = cancellationToken.Register(() => dialog.Dispatcher.BeginInvoke(dialog.Close));
        return dialog.ShowDialog();
    }
}
