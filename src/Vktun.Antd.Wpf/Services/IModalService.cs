using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Displays modal dialogs in an in-window overlay host.
/// </summary>
public interface IModalService
{
    /// <summary>
    /// Shows a modal dialog and returns the result when it closes.
    /// </summary>
    Task<bool?> ShowAsync(Window owner, ModalRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Shows a modal dialog in a dedicated child window and returns the result when it closes.
    /// </summary>
    /// <param name="owner">The owner window.</param>
    /// <param name="request">The modal payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The dialog result.</returns>
    Task<bool?> ShowWindowAsync(Window owner, ModalRequest request, CancellationToken cancellationToken = default);
}
