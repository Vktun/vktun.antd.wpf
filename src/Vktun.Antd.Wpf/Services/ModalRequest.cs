namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a modal dialog payload.
/// </summary>
public sealed class ModalRequest
{
    /// <summary>
    /// Gets or sets the modal title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the body content.
    /// </summary>
    public object? Content { get; init; }

    /// <summary>
    /// Gets or sets the confirmation text.
    /// </summary>
    public string OkText { get; init; } = "OK";

    /// <summary>
    /// Gets or sets the cancel text.
    /// </summary>
    public string CancelText { get; init; } = "Cancel";

    /// <summary>
    /// Gets or sets a value indicating whether the cancel action is available.
    /// </summary>
    public bool ShowCancel { get; init; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether clicking the mask closes the modal.
    /// </summary>
    public bool MaskClosable { get; init; } = true;

    /// <summary>
    /// Gets or sets the preferred modal width.
    /// </summary>
    public double Width { get; init; } = 420d;

    /// <summary>
    /// Gets or sets the preferred modal max width.
    /// </summary>
    public double MaxWidth { get; init; } = 560d;
}
