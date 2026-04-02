using System;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a notification payload.
/// </summary>
public sealed class NotificationRequest
{
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the semantic kind.
    /// </summary>
    public MessageKind Kind { get; init; } = MessageKind.Info;

    /// <summary>
    /// Gets or sets the placement.
    /// </summary>
    public NotificationPlacement Placement { get; init; } = NotificationPlacement.TopRight;

    /// <summary>
    /// Gets or sets the auto close duration.
    /// </summary>
    public TimeSpan Duration { get; init; } = TimeSpan.FromSeconds(4d);
}
